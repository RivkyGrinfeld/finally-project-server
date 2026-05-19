using Bl.Models.DTOs;
using Bl.Services;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
//using QuestPDF.Fluent.Document;
using QuestPDF.Infrastructure;
using System;
using System.Reflection.Metadata;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

[ApiController]
[Route("api/cv")]
public class CvController : ControllerBase
{
    private readonly OpenAiService _ai;

    public CvController(OpenAiService ai)
    {
        _ai = ai;
    }




    [HttpPost("next")]
    public async Task<IActionResult> GetNextQuestion([FromBody] CandidateCvDto cv)
    {
        var nextQuestion = CvValidator.GetNextQuestion(cv);

        if (nextQuestion == null)
        {
            try
            {
                string prompt;
                
                if (cv.Conversation != null && cv.Conversation.Any())
                {
                    prompt = string.Join("\n", cv.Conversation.Select(c => $"{c.Role}: {c.Content}"));
                }
                else
                {
                    prompt = $"שם: {cv.FullName}, אימייל: {cv.Email}, טלפון: {cv.Phone}, " +
                             $"כישורים: {string.Join(", ", cv.Skills)}, " +
                             $"ניסיון: {string.Join(", ", cv.Experience.Select(e => $"{e.Role} ב-{e.Company}"))}, " +
                             $"השכלה: {string.Join(", ", cv.Education.Select(e => $"{e.Degree} ב-{e.Institution}"))}";
                    cv.Conversation.Add(new ConversationMessage { Role = "user", Content = prompt });
                }

                string txt = await _ai.AskAsync(prompt);
                
                // Clean potential markdown code fences from AI response
                txt = txt.Trim();
                if (txt.StartsWith("```json")) txt = txt.Substring(7);
                if (txt.StartsWith("```")) txt = txt.Substring(3);
                if (txt.EndsWith("```")) txt = txt.Substring(0, txt.Length - 3);
                txt = txt.Trim();

                AiCvResponse aiResponse = JsonSerializer.Deserialize<AiCvResponse>(txt, new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true 
                })!;
                
                return await f(aiResponse, cv);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(502, new { error = "שגיאה בתקשורת עם שירות ה-AI", details = ex.Message });
            }
            catch (JsonException ex)
            {
                return StatusCode(500, new { error = "שגיאה בפענוח תשובת ה-AI", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "שגיאה כללית", details = ex.Message });
            }
        }

        return Ok(new { question = nextQuestion, Role = "c" });
    }

    private async Task<IActionResult> f(AiCvResponse aiResponse, CandidateCvDto cv)
    {
        if (aiResponse.IsComplete && aiResponse.PolishedCv != null)
        {
            try
            {
                var pdfBytes = GeneratePdf(aiResponse.PolishedCv);
                return File(pdfBytes, "application/pdf", "cv.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "שגיאה ביצירת PDF", details = ex.Message });
            }
        }
        else
        {
            cv.Conversation.Add(new ConversationMessage { Role = "assistant", Content = aiResponse.Question });
            return Ok(new { question = aiResponse.Question, Role = "ai" });
        }
    }

    [HttpPost("aiq")]
    public async Task<IActionResult> Conversation([FromBody] CvDto cv)
    {
        try
        {
            cv.candidate.Conversation.Add(new ConversationMessage { Role = "user", Content = cv.ans });
            string prompt = string.Join("\n", cv.candidate.Conversation.Select(c => $"{c.Role}: {c.Content}"));
            string txt = await _ai.AskAsync(prompt);

            // Clean potential markdown code fences from AI response
            txt = txt.Trim();
            if (txt.StartsWith("```json")) txt = txt.Substring(7);
            if (txt.StartsWith("```")) txt = txt.Substring(3);
            if (txt.EndsWith("```")) txt = txt.Substring(0, txt.Length - 3);
            txt = txt.Trim();

            AiCvResponse aiResponse = JsonSerializer.Deserialize<AiCvResponse>(txt, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            })!;
            
            return await f(aiResponse, cv.candidate);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new { error = "שגיאה בתקשורת עם שירות ה-AI", details = ex.Message });
        }
        catch (JsonException ex)
        {
            return StatusCode(500, new { error = "שגיאה בפענוח תשובת ה-AI", details = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "שגיאה כללית", details = ex.Message });
        }
    }

    //public string Quest(string a)
    //{
    //    return a;
    //}

    [HttpPost("analyze-match")]
    public async Task<IActionResult> AnalyzeCvMatch([FromBody] CvMatchRequest request)
    {
        try
        {
            // Read the CV file from Uploads folder
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            var filePath = Path.Combine(uploadsFolder, request.FileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound(new { error = "קובץ קו\"ח לא נמצא" });

            // Read file content (for text-based files like PDF text extraction would be needed)
            // For simplicity, we pass the filename and let AI work with what we have
            var cvContent = $"שם הקובץ: {request.FileName}";
            
            // If it's a text file, read it
            var ext = Path.GetExtension(filePath).ToLower();
            if (ext == ".txt" || ext == ".csv")
            {
                cvContent = await System.IO.File.ReadAllTextAsync(filePath);
            }
            else
            {
                // For PDF/DOC files, pass the candidate info we have
                cvContent = request.CandidateInfo ?? $"מועמד: {request.FileName}";
            }

            // Build job requirements string
            var requirements = string.Join(", ", request.Requirements ?? new List<string>());

            var score = await _ai.AnalyzeCvMatchAsync(cvContent, requirements);

            return Ok(new { score });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "שגיאה בניתוח", details = ex.Message });
        }
    }

    private byte[] GeneratePdf(CandidateCvDto cv)
    {
        return QuestPDF.Fluent.Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(QuestPDF.Helpers.PageSizes.A4);
                page.MarginVertical(40);
                page.MarginHorizontal(50);
                page.DefaultTextStyle(x => x.FontSize(10).FontColor("#333333"));

                page.Header().Column(header =>
                {
                    // Name
                    header.Item().Text(cv.FullName)
                        .FontSize(28)
                        .Bold()
                        .FontColor("#1a1a2e");

                    header.Item().PaddingTop(6).Row(row =>
                    {
                        if (!string.IsNullOrEmpty(cv.Email))
                        {
                            row.AutoItem().Text(cv.Email).FontSize(10).FontColor("#4f46e5");
                            row.AutoItem().PaddingHorizontal(8).Text("|").FontSize(10).FontColor("#cbd5e1");
                        }
                        if (!string.IsNullOrEmpty(cv.Phone))
                        {
                            row.AutoItem().Text(cv.Phone).FontSize(10).FontColor("#4f46e5");
                        }
                    });

                    // Divider line
                    header.Item().PaddingTop(12).LineHorizontal(1).LineColor("#e2e8f0");
                });

                page.Content().PaddingTop(20).Column(col =>
                {
                    // Skills Section
                    if (cv.Skills != null && cv.Skills.Any())
                    {
                        col.Item().PaddingBottom(16).Column(section =>
                        {
                            section.Item().Text("כישורים מקצועיים")
                                .FontSize(13)
                                .Bold()
                                .FontColor("#1a1a2e");
                            section.Item().PaddingTop(4).LineHorizontal(0.5f).LineColor("#4f46e5");
                            section.Item().PaddingTop(8).Row(row =>
                            {
                                foreach (var skill in cv.Skills)
                                {
                                    row.AutoItem().PaddingLeft(4).PaddingBottom(4)
                                        .Background("#f1f5f9")
                                        .Padding(4)
                                        .Text(skill)
                                        .FontSize(9)
                                        .FontColor("#334155");
                                }
                            });
                        });
                    }

                    // Experience Section
                    if (cv.Experience != null && cv.Experience.Any())
                    {
                        col.Item().PaddingBottom(16).Column(section =>
                        {
                            section.Item().Text("ניסיון תעסוקתי")
                                .FontSize(13)
                                .Bold()
                                .FontColor("#1a1a2e");
                            section.Item().PaddingTop(4).LineHorizontal(0.5f).LineColor("#4f46e5");

                            foreach (var exp in cv.Experience)
                            {
                                section.Item().PaddingTop(10).Column(expItem =>
                                {
                                    expItem.Item().Row(row =>
                                    {
                                        row.RelativeItem().Text(exp.Role)
                                            .FontSize(11)
                                            .Bold()
                                            .FontColor("#1e293b");
                                        if (!string.IsNullOrEmpty(exp.StartDate) || !string.IsNullOrEmpty(exp.EndDate))
                                        {
                                            row.AutoItem().Text($"{exp.StartDate} - {exp.EndDate}")
                                                .FontSize(9)
                                                .FontColor("#94a3b8");
                                        }
                                    });
                                    if (!string.IsNullOrEmpty(exp.Company))
                                    {
                                        expItem.Item().PaddingTop(2).Text(exp.Company)
                                            .FontSize(10)
                                            .FontColor("#64748b");
                                    }
                                });
                            }
                        });
                    }

                    // Education Section
                    if (cv.Education != null && cv.Education.Any())
                    {
                        col.Item().PaddingBottom(16).Column(section =>
                        {
                            section.Item().Text("השכלה")
                                .FontSize(13)
                                .Bold()
                                .FontColor("#1a1a2e");
                            section.Item().PaddingTop(4).LineHorizontal(0.5f).LineColor("#4f46e5");

                            foreach (var edu in cv.Education)
                            {
                                section.Item().PaddingTop(10).Column(eduItem =>
                                {
                                    eduItem.Item().Text(edu.Degree)
                                        .FontSize(11)
                                        .Bold()
                                        .FontColor("#1e293b");
                                    if (!string.IsNullOrEmpty(edu.Institution))
                                    {
                                        eduItem.Item().PaddingTop(2).Text(edu.Institution)
                                            .FontSize(10)
                                            .FontColor("#64748b");
                                    }
                                });
                            }
                        });
                    }
                });

                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("נוצר באמצעות JobMatch AI")
                        .FontSize(8)
                        .FontColor("#94a3b8");
                });
            });
        }).GeneratePdf();
    }
}
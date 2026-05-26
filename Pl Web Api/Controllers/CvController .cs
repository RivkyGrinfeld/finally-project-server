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
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Linq;

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

                // Use options with converter that accepts string or object for Experience items
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                options.Converters.Add(new ExperienceDtoJsonConverter());

                AiCvResponse aiResponse = JsonSerializer.Deserialize<AiCvResponse>(txt, options)!;

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

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new ExperienceDtoJsonConverter());

            AiCvResponse aiResponse = JsonSerializer.Deserialize<AiCvResponse>(txt, options)!;

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

    // Converter that accepts either string or object for ExperienceDto
    private class ExperienceDtoJsonConverter : JsonConverter<ExperienceDto>
    {
        public override ExperienceDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var txt = reader.GetString();
                return new ExperienceDto { Description = txt ?? string.Empty };
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                using var doc = JsonDocument.ParseValue(ref reader);
                var el = doc.RootElement;
                var exp = new ExperienceDto();

                if (el.TryGetProperty("Company", out var p) && p.ValueKind == JsonValueKind.String) exp.Company = p.GetString();
                if (el.TryGetProperty("Role", out var r) && r.ValueKind == JsonValueKind.String) exp.Role = r.GetString();
                if (el.TryGetProperty("StartDate", out var sd) && sd.ValueKind == JsonValueKind.String) exp.StartDate = sd.GetString();
                if (el.TryGetProperty("EndDate", out var ed) && ed.ValueKind == JsonValueKind.String) exp.EndDate = ed.GetString();
                if (el.TryGetProperty("Description", out var d) && d.ValueKind == JsonValueKind.String) exp.Description = d.GetString();

                return exp;
            }

            // fallback
            return new ExperienceDto();
        }

        public override void Write(Utf8JsonWriter writer, ExperienceDto value, JsonSerializerOptions options)
        {
            // default serialization
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}

//using Azure;
//using Bl.Api;
//using Dal.Api;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Wordprocessing;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using UglyToad.PdfPig;
//using UglyToad.PdfPig.Content;
//using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
//using UglyToad.PdfPig.Graphics;
//using System.Text.RegularExpressions;
//using Microsoft.AspNetCore.Mvc;

//namespace Bl.Services
//{
//    public class OpenAiService
//    {
//        private readonly IConfiguration _config;

//        public OpenAiService(IConfiguration config)
//        {
//            _config = config;
//        }

//        public async Task<string> AskAsync(string prompt)
//        {
//            using var httpClient = new HttpClient();
//            httpClient.BaseAddress = new Uri("https://safeai613.com/v1/");
//            httpClient.DefaultRequestHeaders.Authorization =
//                new AuthenticationHeaderValue("Bearer", "sk-safeai-dee4e1a30e6cc9ed21fc068ac5de55029c5f9725eec64cec");

//            var payload = new
//            {
//                model = "gpt-4o-mini",
//                messages = new[]
//                {
//            new
//            {
//                role = "system",
//                content = @"
//אתה מומחה ביצירת קורות חיים בעברית.
//אתה מקבל שרשור של השיחה עד עכשיו, הכולל חלק מהפרטים הנחוצים.

//מטרה:
//- לבדוק אילו פרטים חסרים
//- לשאול שאלות קצרות עד שכל השדות מלאים
//- בסיום להחזיר קורות חיים מסודרים

//החזר JSON בלבד:
//{
//  ""Question"": ""string"",
//  ""IsComplete"": false,
//  ""PolishedCv"": {
//    ""Skills"": [],
//    ""Experience"": [],
//    ""Education"": []
//  }
//}

//כל השדות מערכים גם אם יש פריט אחד.
//אין טקסט מחוץ ל-JSON."
//            },
//            new { role = "user", content = prompt }
//        },
//                temperature = 0.3,
//                max_tokens = 1200,
//                response_format = new
//                {
//                    type = "json_object"
//                }

//            };
//            var json = JsonSerializer.Serialize(payload);
//            var content = new StringContent(json, Encoding.UTF8, "application/json");

//            try
//            {
//                var response = await httpClient.PostAsync("chat/completions", content);

//                var result = await response.Content.ReadAsStringAsync();

//                if (!response.IsSuccessStatusCode)
//                {
//                    Console.WriteLine($"OpenAI API Error - Status: {response.StatusCode}, Body: {result}");
//                    throw new HttpRequestException(
//                        $"OpenAI API call failed. StatusCode: {response.StatusCode}, Body: {result}");
//                }

//                using var doc = JsonDocument.Parse(result);

//                var rawContent = doc.RootElement.GetProperty("choices")[0]
//                          .GetProperty("message")
//                          .GetProperty("content")
//                          .GetString() ?? string.Empty;

//                // Log raw content for debugging
//                Console.WriteLine("[OpenAiService] Raw AI content:");
//                Console.WriteLine(rawContent);

//                // Try to clean / extract valid JSON before returning
//                var cleaned = CleanAndValidateJson(rawContent, out bool valid);

//                if (valid)
//                {
//                    Console.WriteLine("[OpenAiService] Cleaned JSON successfully.");
//                    return cleaned;
//                }
//                else
//                {
//                    Console.WriteLine("[OpenAiService] Could not fully validate JSON after cleaning. Returning best-effort string.");
//                    return cleaned; // still return cleaned or raw; controller will attempt deserialize and handle failure
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Exception in AskAsync: {ex.Message}");
//                throw;
//            }
//        }

//        // Attempts to extract/clean/repair JSON returned by the AI.
//        // Returns a string (preferably valid JSON). 'success' indicates if parsing succeeded.
//        private string CleanAndValidateJson(string input, out bool success)
//        {
//            success = false;
//            if (string.IsNullOrWhiteSpace(input)) return input ?? string.Empty;

//            string s = input.Trim();

//            // Remove triple-backtick fences (``` or ```json)
//            s = Regex.Replace(s, @"^```(?:json)?\s*", "", RegexOptions.IgnoreCase);
//            s = Regex.Replace(s, @"\s*```$", "", RegexOptions.IgnoreCase);
//            s = s.Trim();

//            // Quick parse attempt
//            if (TryParseJson(s))
//            {
//                success = true;
//                return s;
//            }

//            // Try to find the first JSON object {...}
//            int firstObj = s.IndexOf('{');
//            int lastObj = s.LastIndexOf('}');
//            if (firstObj >= 0 && lastObj > firstObj)
//            {
//                var candidate = s.Substring(firstObj, lastObj - firstObj + 1);
//                if (TryParseJson(candidate))
//                {
//                    success = true;
//                    return candidate;
//                }
//                s = candidate; // continue trying fixes on candidate
//            }
//            else
//            {
//                // Try to find JSON array [...]
//                int firstArr = s.IndexOf('[');
//                int lastArr = s.LastIndexOf(']');
//                if (firstArr >= 0 && lastArr > firstArr)
//                {
//                    var candidate = s.Substring(firstArr, lastArr - firstArr + 1);
//                    if (TryParseJson(candidate))
//                    {
//                        success = true;
//                        return candidate;
//                    }
//                    s = candidate;
//                }
//            }

//            // Remove leading non-json lines (like "Here's the JSON:" or extraneous text)
//            var match = Regex.Match(s, @"(\{[\s\S]*\}|\[[\s\S]*\])");
//            if (match.Success)
//            {
//                var candidate = match.Groups[1].Value;
//                if (TryParseJson(candidate))
//                {
//                    success = true;
//                    return candidate;
//                }
//                s = candidate;
//            }

//            // Common fixes:
//            // - remove trailing commas before } or ]
//            s = Regex.Replace(s, @",\s*(?=[}\]])", "");

//            // - convert single quotes to double quotes when no double quotes exist (naive but useful for some outputs)
//            if (!s.Contains("\"") && s.Contains("'"))
//            {
//                s = s.Replace('\'', '"');
//            }

//            // - replace Windows-style newlines with \n
//            s = s.Replace("\r\n", "\n");

//            // Try parse again
//            if (TryParseJson(s))
//            {
//                success = true;
//                return s;
//            }

//            // As a last resort: try to wrap plain key: value lines into an object (very aggressive)
//            // Only do this if it looks like multiple lines with colon and no surrounding braces.
//            var lines = s.Split('\n').Select(l => l.Trim()).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
//            if (lines.Length > 0 && !s.StartsWith("{") && lines.Any(l => l.Contains(":")))
//            {
//                var sb = new StringBuilder();
//                sb.Append("{");
//                for (int i = 0; i < lines.Length; i++)
//                {
//                    var line = lines[i];
//                    var idx = line.IndexOf(':');
//                    if (idx > 0)
//                    {
//                        var key = line.Substring(0, idx).Trim().Trim('"', '\'');
//                        var val = line.Substring(idx + 1).Trim().Trim('"', '\'');
//                        sb.Append($"\"{EscapeString(key)}\":\"{EscapeString(val)}\"");
//                        if (i < lines.Length - 1) sb.Append(",");
//                    }
//                }
//                sb.Append("}");
//                var candidate = sb.ToString();
//                if (TryParseJson(candidate))
//                {
//                    success = true;
//                    return candidate;
//                }
//                s = candidate;
//            }

//            // Couldn't fully validate: return the best attempt (so controller can log / inspect)
//            success = false;
//            return s;
//        }

//        private static bool TryParseJson(string s)
//        {
//            try
//            {
//                using var _ = JsonDocument.Parse(s);
//                return true;
//            }
//            catch
//            {
//                return false;
//            }
//        }

//        private static string EscapeString(string s)
//        {
//            if (s == null) return string.Empty;
//            return s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r");
//        }

//        static string ExtractText(string filePath)
//        {
//            string extension =
//                Path.GetExtension(filePath).ToLower();

//            return extension switch
//            {
//                ".pdf" => ConvertPdfToText(filePath),
//                ".docx" => ConvertWordToText(filePath),
//                _ => throw new Exception("Unsupported file type")
//            };
//        }
//        static string ConvertPdfToText(string filePath)
//        {
//            var sb = new StringBuilder();

//            using (PdfDocument document = PdfDocument.Open(filePath))
//            {
//                foreach (Page page in document.GetPages())
//                {
//                    string text =
//                        ContentOrderTextExtractor.GetText(page);

//                    text = FixHebrew(text);

//                    sb.AppendLine(text);
//                }
//            }

//            return sb.ToString();
//        }

//        static string ConvertWordToText(string filePath)
//        {
//            var sb = new StringBuilder();

//            using (WordprocessingDocument doc =
//                   WordprocessingDocument.Open(filePath, false))
//            {
//                Body body =
//                    doc.MainDocumentPart.Document.Body;

//                foreach (Text text in body.Descendants<Text>())
//                {
//                    sb.Append(text.Text + " ");
//                }
//            }

//            return FixHebrew(sb.ToString());
//        }

//        static string FixHebrew(string text)
//        {
//            //var lines = text.Split('\n');

//            //for (int i = 0; i < lines.Length; i++)
//            //{
//            //    string line = lines[i];

//            //    bool containsHebrew =
//            //        line.Any(c => c >= 0x0590 && c <= 0x05FF);

//            //    if (containsHebrew)
//            //    {
//            //        char[] chars = line.ToCharArray();
//            //        Array.Reverse(chars);

//            //        lines[i] = new string(chars);
//            //    }
//            //}

//            //return string.Join("\n", lines);
//            return text;
//        }
//        [HttpPost("analyze-match")]
//        public async Task<string> AnalyzeCvMatchAsync(string cvText, string jobRequirements)
//        {

//            try
//            {
//                // קריאת PDF
//                //string resumeText = ConvertPdfToText(cvText);
//                string text = ExtractText(cvText);

//                Console.OutputEncoding = Encoding.UTF8;
//                // בניית הפרומפט
//                string inputText =
//    $@"
//You are a recruitment matching engine.

//Analyze the candidate resume against the job description.

//Return ONLY a number between 0 and 100 representing the match percentage.

//Do not explain.
//Do not add text.
//Do not add symbols.
//Do not write percent sign.
//Only the numeric value.

//Candidate Resume:
//{text}

//Job Description:
//{jobRequirements}
//";

//                inputText = $@"Candidate Resume:\n{text}\n\nJob Description:\n{jobRequirements}\n\nAnalyze and indicate if the candidate is suitable for this position";

//                // שליחה ל-AI
//                string result = await SendToOpenAI(inputText);

//                Console.WriteLine("Match Percentage:");
//                Console.WriteLine(result);
//                return result;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Error:");
//                Console.WriteLine(ex.Message);
//            }
//            return "0";
//        }
//        static async Task<string> SendToOpenAI(string prompt)
//        {
//            string apiKey = "sk-safeai-dee4e1a30e6cc9ed21fc068ac5de55029c5f9725eec64cec";

//            using var client = new HttpClient();

//            client.BaseAddress = new Uri("https://safeai613.com/v1/");

//            client.DefaultRequestHeaders.Authorization =
//                new AuthenticationHeaderValue("Bearer", apiKey);

//            var requestBody = new
//            {
//                model = "gpt-4o-mini",
//                temperature = 0,
//                messages = new object[]
//                {
//                new
//                {
//                    role = "system",
//                    content = "Return only a numeric percentage between 0 and 100."
//                },
//                new
//                {
//                    role = "user",
//                    content = prompt
//                }
//                }
//            };

//            string json =
//                JsonSerializer.Serialize(requestBody);

//            using var content =
//                new StringContent(json, Encoding.UTF8, "application/json");

//            HttpResponseMessage response =
//                await client.PostAsync("chat/completions", content);

//            string responseString =
//                await response.Content.ReadAsStringAsync();

//            if (!response.IsSuccessStatusCode)
//            {
//                throw new Exception(
//                    $"API Error: {response.StatusCode}\n{responseString}");
//            }

//            using JsonDocument doc =
//                JsonDocument.Parse(responseString);

//            JsonElement root = doc.RootElement;

//            if (root.TryGetProperty("choices", out JsonElement choices) &&
//                choices.GetArrayLength() > 0)
//            {
//                JsonElement message =
//                    choices[0].GetProperty("message");

//                if (message.TryGetProperty("content", out JsonElement contentElement))
//                {
//                    return contentElement.GetString()?.Trim()
//                           ?? "0";
//                }
//            }

//            return "0";
//        }
//    }
//}


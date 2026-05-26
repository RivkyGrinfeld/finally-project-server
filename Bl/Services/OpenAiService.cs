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

//namespace Bl.Services
//{
//    public class OpenAiService
//    {
//        private readonly IConfiguration _config;

//        public OpenAiService(IConfiguration config)
//        {
//            _config = config;
//        }


//        //    public async Task<string> AskAsync(string prompt)
//        //    {
//        //        using var httpClient = new HttpClient();
//        //        httpClient.BaseAddress = new Uri("https://safeai613.com/v1/");
//        //        httpClient.DefaultRequestHeaders.Authorization =
//        //            new AuthenticationHeaderValue("Bearer", "sk-safeai-dee4e1a30e6cc9ed21fc068ac5de55029c5f9725eec64cec");

//        //        var payload = new
//        //        {
//        //            model = "gpt-4o-mini",
//        //            messages = new[]
//        //            {
//        //                    new
//        //                    {
//        //                        role = "system",
//        //                        content = @"

//        //אתה מומחה ביצירת קורות חיים בעברית.  
//        //אתה מקבל שרשור של השיחה עד עכשיו, הכולל חלק מהפרטים הנחוצים.  
//        //המטרה שלך היא לבדוק אילו פרטים חסרים ולשאול את המשתמש שאלות קצרות עד שכל השדות הדרושים מלאים.  
//        //לאחר שכל הפרטים מלאים, כתוב את קורות החיים בצורה יפה ומסודרת.  

//        //החזר **JSON בלבד** עם שלושה שדות:
//        //1. ""Question"" – השאלה הבאה למשתמש, או ריק אם אין שאלות.
//        //2. ""IsComplete"" – true אם כל השדות מלאים וניתן ליצור PDF, אחרת false.
//        //3. ""PolishedCv"" – אובייקט CV משופר.

//        //כל השדות ""Skills"", ""Experience"", ""Education"" חייבים להיות מערכים (Array), גם אם יש פריט אחד בלבד.  
//        //השתמש בהיסטוריית השיחה כדי להבין מה חסר ב-CV של המשתמש ומה השאלה הבאה.  
//        //אין להוסיף טקסט חופשי מחוץ ל-JSON.
//        //"},
//        //                    new { role = "user", content = prompt }
//        //                },
//        //            temperature = 0.3
//        //        };

//        //        var json = JsonSerializer.Serialize(payload);
//        //        var content = new StringContent(json, Encoding.UTF8, "application/json");

//        //        try
//        //        {
//        //            var response = await httpClient.PostAsync("chat/completions", content);

//        //            if (!response.IsSuccessStatusCode)
//        //            {
//        //                var errorBody = await response.Content.ReadAsStringAsync();
//        //                Console.WriteLine($"OpenAI API Error - Status: {response.StatusCode}, Body: {errorBody}");
//        //                throw new HttpRequestException(
//        //                    $"OpenAI API call failed. StatusCode: {response.StatusCode}, Body: {errorBody}");
//        //            }

//        //            var result = await response.Content.ReadAsStringAsync();
//        //            using var doc = JsonDocument.Parse(result);

//        //            return doc.RootElement.GetProperty("choices")[0]
//        //                      .GetProperty("message")
//        //                      .GetProperty("content")
//        //                      .GetString()!;
//        //        }
//        //        catch (Exception ex)
//        //        {
//        //            Console.WriteLine($"Exception in AskAsync: {ex.Message}");
//        //            throw;
//        //        }
//        //    }
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

//                return doc.RootElement.GetProperty("choices")[0]
//                          .GetProperty("message")
//                          .GetProperty("content")
//                          .GetString()!;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Exception in AskAsync: {ex.Message}");
//                throw;
//            }
//        }
//        //        public async Task<int> AnalyzeCvMatchAsync(string cvText, string jobRequirements)
//        //        {
//        //            using var httpClient = new HttpClient();
//        //            httpClient.BaseAddress = new Uri("https://safeai613.com/v1/");
//        //            httpClient.DefaultRequestHeaders.Authorization =
//        //                new AuthenticationHeaderValue("Bearer", "sk-safeai-dee4e1a30e6cc9ed21fc068ac5de55029c5f9725eec64cec");

//        //            var payload = new
//        //            {
//        //                model = "gpt-4o-mini",
//        //                messages = new[]
//        //                {
//        //                    new
//        //                    {
//        //                        role = "system",
//        //                        content = @"אתה מומחה בגיוס עובדים. אתה מקבל תוכן של קורות חיים ודרישות של משרה.
//        //עליך לנתח את ההתאמה בין המועמד למשרה ולהחזיר מספר בלבד בין 0 ל-100 שמייצג את אחוז ההתאמה.
//        //אל תחזיר שום טקסט נוסף - רק מספר!"
//        //                    },
//        //                    new { role = "user", content = $"קורות חיים:\n{cvText}\n\nדרישות המשרה:\n{jobRequirements}" }
//        //                },
//        //                temperature = 0.2
//        //            };

//        //            var json = JsonSerializer.Serialize(payload);
//        //            var content = new StringContent(json, Encoding.UTF8, "application/json");

//        //            try
//        //            {
//        //                var response = await httpClient.PostAsync("chat/completions", content);
//        //                if (!response.IsSuccessStatusCode) return 0;

//        //                var result = await response.Content.ReadAsStringAsync();
//        //                using var doc = JsonDocument.Parse(result);

//        //                var scoreText = doc.RootElement.GetProperty("choices")[0]
//        //                    .GetProperty("message")
//        //                    .GetProperty("content")
//        //                    .GetString()!.Trim();

//        //                if (int.TryParse(scoreText, out int score))
//        //                    return Math.Clamp(score, 0, 100);

//        //                return 0;
//        //            }
//        //            catch
//        //            {
//        //                return 0;
//        //            }
//        //        }

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



using Azure;
using Bl.Api;
using Dal.Api;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
using UglyToad.PdfPig.Graphics;
using System.Text.RegularExpressions;

namespace Bl.Services
{
    public class OpenAiService
    {
        private readonly IConfiguration _config;

        public OpenAiService(IConfiguration config)
        {
            _config = config;
        }

        // AskAsync accepts an optional HttpClient for testability; callers can continue to call AskAsync(prompt).
        public async Task<string> AskAsync(string prompt, HttpClient? httpClient = null)
        {
            bool disposeClient = false;
            HttpClient client = httpClient ?? new HttpClient();
            if (httpClient == null) disposeClient = true;

            try
            {
                client.BaseAddress = new Uri("https://safeai613.com/v1/");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", " sk-safeai-b9f1da666cd7bbf6048bcc3cc562266e0e6620cee11fe7aa");

                var payload = new
                {
                    model = "gpt-4o-mini",
                    messages = new[]
                    {
                        new
                        {
                            role = "system",
                            content = @"אתה מומחה ליצירת קורות חיים.

קלט:
אתה מקבל שרשור של מידע שהוזן עד כה על ידי המשתמש, הכולל חלק מהפרטים הנדרשים ליצירת קורות חיים.

מטרה:
- לזהות אילו פרטים חסרים ליצירת קורות חיים מלאים
- אם חסרים פרטים → לשאול שאלה אחת קצרה בלבד
- אם כל הפרטים קיימים → להחזיר קורות חיים מלאים, מסודרים ומקצועיים

פורמט פלט (חובה JSON בלבד):
{
  ""IsComplete"": true,
  ""Question"": null,
  ""CvText"": ""...""
}

כללים:
- אין להחזיר שום טקסט מחוץ ל-JSON
- אין להשתמש ב-markdown
- חובה להחזיר JSON תקין בלבד

לוגיקה:
כאשר חסרים פרטים:
- IsComplete = false
- Question = שאלה אחת קצרה בלבד
- CvText = null

כאשר כל הפרטים קיימים:
- IsComplete = true
- Question = null
- CvText = קורות חיים מלאים בעברית בפורמט מקצועי

התנהגות:
- לשאול בכל פעם שאלה אחת בלבד
- לא להוסיף הסברים
- לא להמציא מידע שלא סופק
- לא לחזור על מידע שכבר התקבל
"
                        },
                        new { role = "user", content = prompt }
                    },
                    temperature = 0.3,
                    max_tokens = 1200,
                    response_format = new
                    {
                        type = "json_object"
                    }
                };

                var json = JsonSerializer.Serialize(payload);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("chat/completions", content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"OpenAI API Error - Status: {response.StatusCode}, Body: {result}");
                    throw new HttpRequestException($"OpenAI API call failed. StatusCode: {response.StatusCode}, Body: {result}");
                }

                using var doc = JsonDocument.Parse(result);
                var rawContent = doc.RootElement.GetProperty("choices")[0]
                          .GetProperty("message")
                          .GetProperty("content")
                          .GetString() ?? string.Empty;

                // Log raw content for debugging
                Console.WriteLine("[OpenAiService] Raw AI content:");
                Console.WriteLine(rawContent);

                // Try to clean / extract valid JSON before returning
                var cleaned = CleanAndValidateJson(rawContent, out bool valid);

                if (valid)
                {
                    Console.WriteLine("[OpenAiService] Cleaned JSON successfully.");
                    return cleaned;
                }
                else
                {
                    Console.WriteLine("[OpenAiService] Could not fully validate JSON after cleaning. Returning best-effort string.");
                    return cleaned;
                }
            }
            finally
            {
                if (disposeClient)
                {
                    client.Dispose();
                }
            }
        }

        // Attempts to extract/clean/repair JSON returned by the AI.
        // Returns a string (preferably valid JSON). 'success' indicates if parsing succeeded.
        private string CleanAndValidateJson(string input, out bool success)
        {
            success = false;
            if (string.IsNullOrWhiteSpace(input)) return input ?? string.Empty;

            string s = input.Trim();

            // Remove triple-backtick fences (``` or ```json)
            s = Regex.Replace(s, @"^```(?:json)?\s*", "", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"\s*```$", "", RegexOptions.IgnoreCase);
            s = s.Trim();

            // Quick parse attempt
            if (TryParseJson(s))
            {
                success = true;
                return s;
            }

            // Try to find the first JSON object {...}
            int firstObj = s.IndexOf('{');
            int lastObj = s.LastIndexOf('}');
            if (firstObj >= 0 && lastObj > firstObj)
            {
                var candidate = s.Substring(firstObj, lastObj - firstObj + 1);
                if (TryParseJson(candidate))
                {
                    success = true;
                    return candidate;
                }
                s = candidate; // continue trying fixes on candidate
            }
            else
            {
                // Try to find JSON array [...]
                int firstArr = s.IndexOf('[');
                int lastArr = s.LastIndexOf(']');
                if (firstArr >= 0 && lastArr > firstArr)
                {
                    var candidate = s.Substring(firstArr, lastArr - firstArr + 1);
                    if (TryParseJson(candidate))
                    {
                        success = true;
                        return candidate;
                    }
                    s = candidate;
                }
            }

            // Remove leading non-json lines (like "Here's the JSON:" or extraneous text)
            var match = Regex.Match(s, @"(\{[\s\S]*\}|\[[\s\S]*\])");
            if (match.Success)
            {
                var candidate = match.Groups[1].Value;
                if (TryParseJson(candidate))
                {
                    success = true;
                    return candidate;
                }
                s = candidate;
            }

            // Common fixes:
            // - remove trailing commas before } or ]
            s = Regex.Replace(s, @",\s*(?=[}\]])", "");

            // - convert single quotes to double quotes when no double quotes exist (naive but useful for some outputs)
            if (!s.Contains("\"") && s.Contains("'"))
            {
                s = s.Replace('\'', '"');
            }

            // - replace Windows-style newlines with \n
            s = s.Replace("\r\n", "\n");

            // Try parse again
            if (TryParseJson(s))
            {
                success = true;
                return s;
            }

            // As a last resort: try to wrap plain key: value lines into an object (very aggressive)
            var lines = s.Split('\n').Select(l => l.Trim()).Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
            if (lines.Length > 0 && !s.StartsWith("{") && lines.Any(l => l.Contains(":")))
            {
                var sb = new StringBuilder();
                sb.Append("{");
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];
                    var idx = line.IndexOf(':');
                    if (idx > 0)
                    {
                        var key = line.Substring(0, idx).Trim().Trim('"', '\'');
                        var val = line.Substring(idx + 1).Trim().Trim('"', '\'');
                        sb.Append($"\"{EscapeString(key)}\":\"{EscapeString(val)}\"");
                        if (i < lines.Length - 1) sb.Append(",");
                    }
                }
                sb.Append("}");
                var candidate = sb.ToString();
                if (TryParseJson(candidate))
                {
                    success = true;
                    return candidate;
                }
                s = candidate;
            }

            // Couldn't fully validate: return the best attempt (so controller can log / inspect)
            success = false;
            return s;
        }

        private static bool TryParseJson(string s)
        {
            try
            {
                using var _ = JsonDocument.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string EscapeString(string s)
        {
            if (s == null) return string.Empty;
            return s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r");
        }

        // שאר המתודות (ExtractText, ConvertPdfToText וכו') נשארו ללא שינוי
        static string ExtractText(string filePath)
        {
            string extension =
                Path.GetExtension(filePath).ToLower();

            return extension switch
            {
                ".pdf" => ConvertPdfToText(filePath),
                ".docx" => ConvertWordToText(filePath),
                _ => throw new Exception("Unsupported file type")
            };
        }
        static string ConvertPdfToText(string filePath)
        {
            var sb = new StringBuilder();

            using (PdfDocument document = PdfDocument.Open(filePath))
            {
                foreach (Page page in document.GetPages())
                {
                    string text =
                        ContentOrderTextExtractor.GetText(page);

                    text = FixHebrew(text);

                    sb.AppendLine(text);
                }
            }

            return sb.ToString();
        }

        static string ConvertWordToText(string filePath)
        {
            var sb = new StringBuilder();

            using (WordprocessingDocument doc =
                   WordprocessingDocument.Open(filePath, false))
            {
                Body body =
                    doc.MainDocumentPart.Document.Body;

                foreach (Text text in body.Descendants<Text>())
                {
                    sb.Append(text.Text + " ");
                }
            }

            return FixHebrew(sb.ToString());
        }

        static string FixHebrew(string text)
        {
            return text;
        }

        public async Task<string> AnalyzeCvMatchAsync(string cvText, string jobRequirements)
        {

            try
            {
                string text = ExtractText(cvText);

                Console.OutputEncoding = Encoding.UTF8;
                string inputText =
    $@"
You are a recruitment matching engine.

Analyze the candidate resume against the job description.

Return ONLY a number between 0 and 100 representing the match percentage.

Do not explain.
Do not add text.
Do not add symbols.
Do not write percent sign.
Only the numeric value.

Candidate Resume:
{text}

Job Description:
{jobRequirements}
";

                inputText = $@"Candidate Resume:\n{text}\n\nJob Description:\n{jobRequirements}\n\nAnalyze and indicate if the candidate is suitable for this position";

                string result = await SendToOpenAI(inputText);

                Console.WriteLine("Match Percentage:");
                Console.WriteLine(result);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:");
                Console.WriteLine(ex.Message);
            }
            return "0";
        }
        static async Task<string> SendToOpenAI(string prompt)
        {
            string apiKey = "sk-safeai-dee4e1a30e6cc9ed21fc068ac5de55029c5f9725eec64cec";

            using var client = new HttpClient();

            client.BaseAddress = new Uri("https://safeai613.com/v1/");

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "gpt-4o-mini",
                temperature = 0,
                messages = new object[]
                {
                new
                {
                    role = "system",
                    content = "Return only a numeric percentage between 0 and 100."
                },
                new
                {
                    role = "user",
                    content = prompt
                }
                }
            };

            string json =
                JsonSerializer.Serialize(requestBody);

            using var content =
                new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                await client.PostAsync("chat/completions", content);

            string responseString =
                await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"API Error: {response.StatusCode}\n{responseString}");
            }

            using JsonDocument doc =
                JsonDocument.Parse(responseString);

            JsonElement root = doc.RootElement;

            if (root.TryGetProperty("choices", out JsonElement choices) &&
                choices.GetArrayLength() > 0)
            {
                JsonElement message =
                    choices[0].GetProperty("message");

                if (message.TryGetProperty("content", out JsonElement contentElement))
                {
                    return contentElement.GetString()?.Trim()
                           ?? "0";
                }
            }


            return "0";
        }
    }
}

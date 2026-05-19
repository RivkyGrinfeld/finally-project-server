using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bl.Services
{
    public class OpenAiService
    {
        private readonly IConfiguration _config;

        public OpenAiService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> AskAsync(string prompt)
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://safeai613.com/v1/");
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "sk-safeai-dee4e1a30e6cc9ed21fc068ac5de55029c5f9725eec64cec");

            var payload = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = @"החזר JSON עם שלושה שדות:
1. Question – השאלה הבאה למשתמש, או ריק אם אין שאלות.
2. IsComplete – true אם כל השדות מלאים וניתן ליצור PDF, אחרת false.
3. PolishedCv – אובייקט CV משופר.
כל שדות Skills, Experience, Education חייבים להיות מערכים (Array), גם אם יש פריט אחד בלבד.
אין טקסט חופשי מחוץ ל-JSON.
אתה מומחה ביצירת קורות חיים.
שאל שאלות קצרות או שפר טקסט.
השתמש בהיסטוריית השיחה כדי להבין מה חסר ב-CV של המשתמש ומה השאלה הבאה."
                    },
                    new { role = "user", content = prompt }
                },
                temperature = 0.3
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync("chat/completions", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"OpenAI API Error - Status: {response.StatusCode}, Body: {errorBody}");
                    throw new HttpRequestException(
                        $"OpenAI API call failed. StatusCode: {response.StatusCode}, Body: {errorBody}");
                }

                var result = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(result);

                return doc.RootElement.GetProperty("choices")[0]
                          .GetProperty("message")
                          .GetProperty("content")
                          .GetString()!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in AskAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<int> AnalyzeCvMatchAsync(string cvText, string jobRequirements)
        {
            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://safeai613.com/v1/");
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "sk-safeai-dee4e1a30e6cc9ed21fc068ac5de55029c5f9725eec64cec");

            var payload = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = @"אתה מומחה בגיוס עובדים. אתה מקבל תוכן של קורות חיים ודרישות של משרה.
עליך לנתח את ההתאמה בין המועמד למשרה ולהחזיר מספר בלבד בין 0 ל-100 שמייצג את אחוז ההתאמה.
אל תחזיר שום טקסט נוסף - רק מספר!"
                    },
                    new { role = "user", content = $"קורות חיים:\n{cvText}\n\nדרישות המשרה:\n{jobRequirements}" }
                },
                temperature = 0.2
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync("chat/completions", content);
                if (!response.IsSuccessStatusCode) return 0;

                var result = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(result);

                var scoreText = doc.RootElement.GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString()!.Trim();

                if (int.TryParse(scoreText, out int score))
                    return Math.Clamp(score, 0, 100);

                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}





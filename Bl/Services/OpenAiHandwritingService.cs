using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class OpenAiHandwritingService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public OpenAiHandwritingService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    //    public async Task<string> AnalyzeHandwriting(Stream imageStream)
    //    {
    //        using var memoryStream = new MemoryStream();
    //        await imageStream.CopyToAsync(memoryStream);

    //        var imageBytes = memoryStream.ToArray();
    //        var base64Image = Convert.ToBase64String(imageBytes);

    //        var apiKey = _configuration["OpenAI:ApiKey"];

    //        _httpClient.DefaultRequestHeaders.Authorization =
    //            new AuthenticationHeaderValue("Bearer", apiKey);

    //        var requestBody = new
    //        {
    //            model = "gpt-4.1-mini",
    //            input = new object[]
    //            {
    //                new
    //                {
    //                    role = "user",
    //                    content = new object[]
    //                    {
    //                        new
    //                        {
    //                            type = "input_text",
    //                            text = " @\"\r\nAnalyze the handwriting graphology characteristics.\r\n\r\nReturn ONLY valid JSON.\r\n\r\n{\r\n  \"\"writing_style\"\": \"\"\"\",\r\n  \"\"pressure\"\": \"\"\"\",\r\n  \"\"spacing\"\": \"\"\"\",\r\n  \"\"slant\"\": \"\"\"\",\r\n  \"\"letter_size\"\": \"\"\"\",\r\n  \"\"baseline\"\": \"\"\"\",\r\n  \"\"personality_traits\"\": [],\r\n  \"\"confidence\"\": \"\"\"\"\r\n}\r\n\r\nAnalyze:\r\n- slant\r\n- pressure\r\n- spacing\r\n- margins\r\n- baseline stability\r\n- letter size\r\n- writing rhythm\r\n- emotional indicators\r\n- organization style\r\n\r\nDo not explain outside JSON.\r\n\";"
    //                        },
    //                        new
    //                        {
    //                            type = "input_image",
    //                            image_url = $"data:image/jpeg;base64,{base64Image}"
    //                        }
    //                    }
    //                }
    //            }
    //        };

    //        var json = JsonSerializer.Serialize(requestBody);

    //        var content = new StringContent(
    //            json,
    //            Encoding.UTF8,
    //            "application/json");

    //        var response = await _httpClient.PostAsync(
    //            "https://safeai613.com/v1/",
    //            content);

    //        response.EnsureSuccessStatusCode();

    //        var responseJson = await response.Content.ReadAsStringAsync();

    //        using var doc = JsonDocument.Parse(responseJson);

    //        var text = doc
    //            .RootElement
    //            .GetProperty("output")[0]
    //            .GetProperty("content")[0]
    //            .GetProperty("text")
    //            .GetString();

    //        return text ?? "";
    //    }
    //}


    public async Task<string> AnalyzeHandwriting(Stream imageStream)
    {
        using var memoryStream = new MemoryStream();

        await imageStream.CopyToAsync(memoryStream);

        byte[] imageBytes = memoryStream.ToArray();

        string base64Image =
            Convert.ToBase64String(imageBytes);

        string apiKey = "sk-safeai-b9f1da666cd7bbf6048bcc3cc562266e0e6620cee11fe7aa";



        using var client = new HttpClient();

        client.BaseAddress =
            new Uri("https://safeai613.com/v1/");

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);

        string prompt = @"
Analyze the handwriting graphology characteristics.

Return ONLY valid JSON.

{
  ""writing_style"": """",
  ""pressure"": """",
  ""spacing"": """",
  ""slant"": """",
  ""letter_size"": """",
  ""baseline"": """",
  ""personality_traits"": [],
  ""confidence"": """"
}

Analyze:
- slant
- pressure
- spacing
- margins
- baseline stability
- letter size
- writing rhythm
- emotional indicators
- organization style

Do not explain outside JSON.
";

        var requestBody = new
        {
            model = "gpt-4o-mini",
            temperature = 0,
            messages = new object[]
            {
            new
            {
                role = "system",
                content = "Return only valid JSON."
            },
            new
            {
                role = "user",
                content = new object[]
                {
                    new
                    {
                        type = "text",
                        text = prompt
                    },
                    new
                    {
                        type = "image_url",
                        image_url = new
                        {
                            url = $"data:image/jpeg;base64,{base64Image}"
                        }
                    }
                }
            }
            }
        };

        string json =
            JsonSerializer.Serialize(requestBody);

        using var content =
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

        HttpResponseMessage response =
            await client.PostAsync(
                "chat/completions",
                content);

        string responseString =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(
                $"API Error: {response.StatusCode}\n{responseString}");
        }

        using JsonDocument doc =
            JsonDocument.Parse(responseString);

        JsonElement root =
            doc.RootElement;

        if (root.TryGetProperty("choices", out JsonElement choices) &&
            choices.GetArrayLength() > 0)
        {
            JsonElement message =
                choices[0].GetProperty("message");

            if (message.TryGetProperty("content", out JsonElement contentElement))
            {
                return contentElement.GetString()?.Trim()
                       ?? "";
            }
        }

        return "";
    }
}
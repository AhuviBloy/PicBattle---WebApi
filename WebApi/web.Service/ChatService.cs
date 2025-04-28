using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using web.Core.Services;

namespace web.Service
{
    public class ChatService : IChatService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ChatService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<string> GetResponseAsync(string prompt)
        {
            var apiKey = _configuration["OpenAI:ApiKey"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[] {
                    new { role = "user", content = prompt }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);

            // בדיקה אם קוד ה-HTTP בתשובה הוא תקין
            if (!response.IsSuccessStatusCode)
            {
                return $"שגיאה מה-API: {response.StatusCode} - {response.ReasonPhrase}";
            }

            var json = await response.Content.ReadAsStringAsync();

            // טיפול בשגיאה אם אין את המפתחות הצפויים
            try
            {
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("choices", out var choices) &&
                    choices[0].TryGetProperty("message", out var message) &&
                    message.TryGetProperty("content", out var contentElement))
                {
                    return contentElement.GetString();
                }
                else
                {
                    return "שגיאה: התגובה מה-API אינה מכילה את המידע הצפוי.";
                }
            }
            catch (JsonException ex)
            {
                return $"שגיאה בקריאת התגובה: {ex.Message}";
            }
        }
    }
}

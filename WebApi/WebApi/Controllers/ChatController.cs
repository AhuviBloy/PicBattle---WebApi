

//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using web.Core.models;
//using Microsoft.Extensions.Configuration;

//[ApiController]
//[Route("api/[controller]")]
//public class ChatController : ControllerBase
//{
//    private readonly HttpClient _client = new();
//    private readonly string _apiKey;

//    public ChatController(IConfiguration configuration)
//    {
//        _apiKey = configuration["OpenAI:ApiKey"];
//    }

//    [HttpPost]
//    public async Task<IActionResult> Get([FromBody] GptRequest gptRequest)
//    {
//        try
//        {
//            var prompt = new
//            {
//                model = "gpt-4o-mini",
//                messages = new[] {
//                    new { role = "system", content = gptRequest.Prompt },
//                    new { role = "user", content = gptRequest.Question }
//                }
//            };

//            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
//            {
//                Content = JsonContent.Create(prompt)
//            };
//            request.Headers.Add("Authorization", $"Bearer {_apiKey}");

//            var response = await _client.SendAsync(request);
//            var content = await response.Content.ReadAsStringAsync();

//            if (!response.IsSuccessStatusCode)
//                return StatusCode((int)response.StatusCode, content);

//            return Ok(content);
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, $"שגיאה בשרת: {ex.Message}");
//        }
//    }
//}




using Microsoft.AspNetCore.Mvc;
using web.Core.models;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly HttpClient _client = new();
    private readonly string _apiKey;

    public ChatController(IConfiguration configuration)
    {
        _apiKey = configuration["OpenAI:ApiKey"];
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GptRequest gptRequest)
    {
        try
        {
            var requestBody = new
            {
                model = "gpt-4o", // או gpt-4o-mini, לפי מה שבפועל אתה רוצה
                messages = gptRequest.Messages.Select(m => new
                {
                    role = m.Role,
                    content = m.Content
                }).ToArray()
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
            {
                Content = JsonContent.Create(requestBody)
            };
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, content);
            }

            var json = System.Text.Json.JsonDocument.Parse(content);
            var reply = json.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            return Ok(new { reply });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

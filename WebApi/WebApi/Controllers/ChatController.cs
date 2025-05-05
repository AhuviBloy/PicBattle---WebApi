

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




//using Microsoft.AspNetCore.Mvc;
//using web.Core.models;

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
//    public async Task<IActionResult> Post([FromBody] GptRequest gptRequest)
//    {
//        try
//        {
//            var requestBody = new
//            {
//                model = "gpt-4o", // או gpt-4o-mini, לפי מה שבפועל אתה רוצה
//                messages = gptRequest.Messages.Select(m => new
//                {
//                    role = m.Role,
//                    content = m.Content
//                }).ToArray()
//            };

//            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
//            {
//                Content = JsonContent.Create(requestBody)
//            };
//            request.Headers.Add("Authorization", $"Bearer {_apiKey}");

//            var response = await _client.SendAsync(request);
//            var content = await response.Content.ReadAsStringAsync();

//            if (!response.IsSuccessStatusCode)
//            {
//                return StatusCode((int)response.StatusCode, content);
//            }

//            var json = System.Text.Json.JsonDocument.Parse(content);
//            var reply = json.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

//            return Ok(new { reply });
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, ex.Message);
//        }
//    }
//}








//using Microsoft.AspNetCore.Mvc;
//using System.Text.Json;

//namespace Web.Api.Controllers
//{
//    [Route("api/chat")]
//    [ApiController]
//    public class ChatController : ControllerBase
//    {

//        private readonly HttpClient _client = new();
//        private readonly string _apiKey;

//        public ChatController(IConfiguration configuration)
//        {
//            _apiKey = configuration["OpenAI:ApiKey"];
//        }


//        [HttpPost]
//        public async Task<IActionResult> Post([FromBody] ChallengePromptRequest gptRequest)
//        {
//            try
//            {
//                var content = $"User question: {gptRequest.UserQuestion}\nGive me a suitable response.";

//                var prompt = new
//                {
//                    model = "gpt-4o-mini",
//                    messages = new[] {
//                 new { role = "user", content = content }
//             }
//                };

//                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
//                {
//                    Content = JsonContent.Create(prompt)
//                };
//                request.Headers.Add("Authorization", $"Bearer {_apiKey}");

//                var response = await _client.SendAsync(request);
//                if (!response.IsSuccessStatusCode)
//                {
//                    var responseContent = await response.Content.ReadAsStringAsync();
//                    throw new Exception($"Error in-OpenAI. status: {response.StatusCode}. response: {responseContent}");
//                    throw new Exception($"Error in OpenAI. status: {response.StatusCode}. response: {responseContent}");
//                }

//                var responseJson = await response.Content.ReadAsStringAsync();
//                var jsonDoc = JsonDocument.Parse(responseJson);
//                var contentText = jsonDoc.RootElement
//                    .GetProperty("choices")[0]
//                    .GetProperty("message")
//                    .GetProperty("content")
//                    .GetString();

//                var suggestions = contentText
//                    .Split('\n')
//                    .Where(line => !string.IsNullOrWhiteSpace(line))
//                    .Select(line => line.Trim().TrimStart('-', '*', '•').Trim())
//                    .ToList();

//                return Ok(new { prompts = suggestions });
//                return Ok(new { prompts = new List<string> { contentText } });
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error: {ex.Message}");
//                //return StatusCode(500, "שגיאה כלשהי במהלך הפעולה.");
//                return StatusCode(500, "An error occurred during the operation.");
//            }
//        }
//    }
//}

//public class ChallengePromptRequest
//{
//    public string Topic { get; set; }
//    public string Description { get; set; }
//    //public string Topic { get; set; }
//    //public string Description { get; set; }
//    public string UserQuestion { get; set; }
//}


//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Text.Json;

//namespace Web.Api.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class ChatController : ControllerBase
//{
//    private readonly IConfiguration _config;
//    private readonly IHttpClientFactory _httpClientFactory;

//    public ChatController(IConfiguration config, IHttpClientFactory httpClientFactory)
//    {
//        _config = config;
//        _httpClientFactory = httpClientFactory;
//    }

//    [HttpPost]
//    public async Task<IActionResult> Post([FromBody] ChatRequest request)
//    {
//        var apiKey = _config["OpenAI:ApiKey"];
//        var client = _httpClientFactory.CreateClient();

//        client.DefaultRequestHeaders.Authorization =
//            new AuthenticationHeaderValue("Bearer", apiKey);

//        var openAiPayload = new
//        {
//            model = "gpt-3.5-turbo",
//            messages = request.Messages
//        };

//        var json = JsonSerializer.Serialize(openAiPayload);
//        var content = new StringContent(json, Encoding.UTF8, "application/json");

//        var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

//        if (!response.IsSuccessStatusCode)
//            return StatusCode((int)response.StatusCode, new { reply = "Error contacting AI." });

//        var responseStream = await response.Content.ReadAsStreamAsync();
//        var responseData = await JsonDocument.ParseAsync(responseStream);

//        var reply = responseData.RootElement
//            .GetProperty("choices")[0]
//            .GetProperty("message")
//            .GetProperty("content")
//            .GetString();

//        return Ok(new { reply });
//    }
//}





using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comp.API.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public ChatController(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChallengePromptRequest request)
        {
            var apiKey = _config["OpenAI:ApiKey"];

            try
            {
                //var content = $"Title: {request.Topic}\nDescription: {request.Description}\nUser question: {request.UserQuestion}\nPlease provide a suitable response.";
                var payload = new
                {
                    model = "gpt-4o-mini",
                    messages = request.Messages
                    //    messages = new[]
                    //    {
                    //    new { role = "user", content }
                    //}
                };

                var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")
                {
                    Content = JsonContent.Create(payload)
                };

                // Add authorization header
                httpRequest.Headers.Add("Authorization", $"Bearer {apiKey}");

                // Send the request to OpenAI API
                var response = await client.SendAsync(httpRequest);
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"OpenAI Error: {response.StatusCode} - {error}");
                }

                // Parse the response from OpenAI
                var json = await response.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                var replyContent = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                //return Ok(new { prompts = new List<string> { replyContent } });
                return Ok(new { reply = replyContent });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred during the operation.");
            }
        }
    }
    public class ChallengePromptRequest
    {
        [JsonPropertyName("messages")]
        public List<ChatMessage> Messages { get; set; }
    }

    public class ChatMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
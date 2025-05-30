using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

[Route("api/ai")]
[ApiController]
public class AiController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public AiController(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _config = config;
    }

    [HttpPost("review")]
    public async Task<IActionResult> Review([FromBody] AiReviewRequest request)
    {
        Console.WriteLine("התקבלה בקשה");
        Console.WriteLine(request.challengeDescription);

        var prompt = $"אתגר בנושא: {request.challengeDescription}\n" +
                     $"תיאור היצירה: {request.creationDescription}\n" +
                     $"אתה מבקר אומנות AI.ואל תכנס לענינים רוחניים בכלל אתה צריך לתת חוות דעת ותן גם ציון עד כמה זה קשור לנושא האתגר עד כמה היצירה מתאימה לנושא האתגר אל תזכיר רוחניות בתשובה!!! תן חוות דעת עניינית ותמציתית  עם קצת הומור על היצירה לפי התיאור והנושא. אל תכלול המלצות, רעיונות לשיפור, או הערות נוספות.\r\n.\r\n";

        var client = _httpClientFactory.CreateClient();
        var openAiKey = _config["OpenAI:ApiKey"];

        var body = new
        {
            model = "gpt-4o-mini",
            messages = new[] {
                new { role = "system", content = "אתה מבקר אומנות AI." },
                new { role = "user", content = prompt }
            }
        };

        var json = JsonSerializer.Serialize(body);
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
        httpRequest.Headers.Add("Authorization", $"Bearer {openAiKey}");
        httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.SendAsync(httpRequest);
        if (!response.IsSuccessStatusCode)
        {
            var errorText = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"שגיאה מה-AI: {response.StatusCode} - {errorText}");
            return BadRequest("שגיאה מה-AI");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(jsonResponse);
        var aiReply = doc.RootElement
                         .GetProperty("choices")[0]
                         .GetProperty("message")
                         .GetProperty("content")
                         .GetString();

        return Ok(new { response = aiReply });
    }
}

public class AiReviewRequest
{
    public string challengeDescription { get; set; }
    public string creationDescription { get; set; }
}






//using Google.Cloud.Vision.V1;
//using Microsoft.AspNetCore.Mvc;
//using System.Net.Http;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using static System.Net.Mime.MediaTypeNames;
//using Image = Google.Cloud.Vision.V1.Image;

//[Route("api/ai")]
//[ApiController]
//public class AiController : ControllerBase
//{
//    private readonly IHttpClientFactory _httpClientFactory;
//    private readonly IConfiguration _config;

//    public AiController(IHttpClientFactory httpClientFactory, IConfiguration config)
//    {
//        _httpClientFactory = httpClientFactory;
//        _config = config;
//    }

//    [HttpPost("review")]
//    public async Task<IActionResult> Review([FromBody] AiReviewRequest request)
//    {
//        Console.WriteLine("התקבלה בקשה");
//        Console.WriteLine(request.ChallengeDescription);

//        // קוראים ל-Google Vision כדי לקבל תיאור התמונה
//        var imageDescription = await AnalyzeImageAsync(request.creationUrl);

//        var prompt = $"אתגר בנושא: {request.ChallengeDescription}\n" +
//                     $"תיאור היצירה: {request.CreationDescription}\n" +
//                     $"תיאור התמונה (ניתוח AI): {imageDescription}\n" +
//                     $"אתה מבקר אומנות AI. תן חוות דעת עניינית ותמציתית בלבד על היצירה לפי התיאור, הנושא וניתוח התמונה. אנא אל תכלול המלצות, רעיונות לשיפור, או הערות נוספות.\r\n.\r\n";

//        var client = _httpClientFactory.CreateClient();
//        var openAiKey = _config["OpenAI:ApiKey"];

//        var body = new
//        {
//            model = "gpt-4o-mini",
//            messages = new[] {
//                new { role = "system", content = "אתה מבקר אומנות AI." },
//                new { role = "user", content = prompt }
//            }
//        };

//        var json = JsonSerializer.Serialize(body);
//        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
//        httpRequest.Headers.Add("Authorization", $"Bearer {openAiKey}");
//        httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");

//        var response = await client.SendAsync(httpRequest);
//        if (!response.IsSuccessStatusCode)
//            return BadRequest("שגיאה מה-AI");

//        var jsonResponse = await response.Content.ReadAsStringAsync();
//        using var doc = JsonDocument.Parse(jsonResponse);
//        var aiReply = doc.RootElement
//                         .GetProperty("choices")[0]
//                         .GetProperty("message")
//                         .GetProperty("content")
//                         .GetString();

//        return Ok(new { response = aiReply });
//    }

//    private async Task<string> AnalyzeImageAsync(string imageUrl)
//    {
//        // יוצרים client של Google Vision
//        var client = await ImageAnnotatorClient.CreateAsync();

//        // יוצרים Image מהכתובת
//        var image = Image.FromUri(imageUrl);

//        // מבקשים תגיות על התמונה (Label Detection)
//        var labels = await client.DetectLabelsAsync(image);

//        if (labels == null || labels.Count == 0)
//            return "לא נמצא תיאור לתמונה.";

//        // מחברים את התגיות לטקסט פשוט
//        var description = string.Join(", ", labels.Select(label => label.Description));

//        return description;
//    }
//}

//public class AiReviewRequest
//{
//    public string ChallengeDescription { get; set; }
//    public string CreationDescription { get; set; }
//    public string creationUrl { get; set; }  // כתובת התמונה הציבורית ב-AWS
//}






//using Google.Cloud.Vision.V1;
//using Google.Apis.Auth.OAuth2;
//using Microsoft.AspNetCore.Mvc;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//[Route("api/ai")]
//[ApiController]
//public class AiController : ControllerBase
//{
//    private readonly IHttpClientFactory _httpClientFactory;
//    private readonly IConfiguration _config;

//    public AiController(IHttpClientFactory httpClientFactory, IConfiguration config)
//    {
//        _httpClientFactory = httpClientFactory;
//        _config = config;
//    }

//    [HttpPost("review")]
//    public async Task<IActionResult> Review([FromBody] AiReviewRequest request)
//    {
//        var imageDescription = await AnalyzeImageAsync(request.creationUrl);

//        var prompt = $"אתגר בנושא: {request.ChallengeDescription}\n" +
//                     $"תיאור היצירה: {request.CreationDescription}\n" +
//                     $"תיאור התמונה (ניתוח AI): {imageDescription}\n" +
//                     $"אתה מבקר אומנות AI. תן חוות דעת עניינית ותמציתית בלבד על היצירה לפי התיאור, הנושא וניתוח התמונה. אנא אל תכלול המלצות, רעיונות לשיפור, או הערות נוספות.\r\n.\r\n";

//        var client = _httpClientFactory.CreateClient();
//        var openAiKey = _config["OpenAI:ApiKey"];

//        var body = new
//        {
//            model = "gpt-4o-mini",
//            messages = new[] {
//                new { role = "system", content = "אתה מבקר אומנות AI." },
//                new { role = "user", content = prompt }
//            }
//        };

//        var json = JsonSerializer.Serialize(body);
//        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
//        httpRequest.Headers.Add("Authorization", $"Bearer {openAiKey}");
//        httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");

//        var response = await client.SendAsync(httpRequest);
//        if (!response.IsSuccessStatusCode)
//            return BadRequest("שגיאה מה-AI");

//        var jsonResponse = await response.Content.ReadAsStringAsync();
//        using var doc = JsonDocument.Parse(jsonResponse);
//        var aiReply = doc.RootElement
//                         .GetProperty("choices")[0]
//                         .GetProperty("message")
//                         .GetProperty("content")
//                         .GetString();

//        return Ok(new { response = aiReply });
//    }

//    private async Task<string> AnalyzeImageAsync(string imageUrl)
//    {
//        var credentialsPath = _config["GoogleCloud:CredentialsPath"];

//        GoogleCredential credential;
//        using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
//        {
//            credential = GoogleCredential.FromStream(stream);
//        }

//        var builder = new ImageAnnotatorClientBuilder
//        {
//            Credential = credential
//        };

//        var client = await builder.BuildAsync();

//        var image = Image.FromUri(imageUrl);

//        var labels = await client.DetectLabelsAsync(image);

//        if (labels == null || labels.Count == 0)
//            return "לא נמצא תיאור לתמונה.";

//        var description = string.Join(", ", labels.Select(label => label.Description));
//        return description;
//    }
//}

//public class AiReviewRequest
//{
//    public string ChallengeDescription { get; set; }
//    public string CreationDescription { get; set; }
//    public string creationUrl { get; set; }
//}


using System.ClientModel;
using Azure.AI.OpenAI;
using ChatBotProject.Extensions;
using ChatBotProject.Models;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;

namespace ChatBotProject.Controllers
{ 
    public class Mth404BotController : Controller
    {
        private readonly AzureOpenAIClient _aiClient;
        private readonly string? _deploymentName;
        private readonly ChatCompletionOptions _options;
        private readonly string _systemMessage;
        
        public Mth404BotController(IConfiguration configuration)
        {
            _systemMessage = configuration["systemMessage"] ?? "";
            _options = new ChatCompletionOptions()
            {
                MaxOutputTokenCount = int.Parse(configuration["MaxOutputTokenCount"] ?? "100")
            };
            var apiKey = configuration["OpenAIAPIKey"];
            var endpoint = configuration["OpenAIEndPoint"];
            _deploymentName = configuration["OpenAIDeploymentName"];

            _aiClient = new AzureOpenAIClient(new Uri(endpoint ?? ""), new ApiKeyCredential(apiKey ?? ""));
        }
        
        
        public IActionResult Index()
        {
            var chatHistory = HttpContext.Session.Get<List<string>>("ChatHistory") ?? [];

            if (chatHistory.Count == 0)
            {
                chatHistory.Add("MTH404 Bot: Merhabalar, size nasıl yardımcı olabilirim?");
                HttpContext.Session.Set("ChatHistory", chatHistory);
            }

            var model = new ChatViewModel
            {
                ChatHistory = chatHistory
            };
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Index(ChatViewModel model)
        {
            var chatHistory = HttpContext.Session.Get<List<string>>("ChatHistory") ?? [];

            if (!string.IsNullOrEmpty(model.UserInput))
            {
                var chatClient = _aiClient.GetChatClient(_deploymentName);

                ChatCompletion completion = chatClient.CompleteChat([
                    new SystemChatMessage(_systemMessage),
                    new UserChatMessage(model.UserInput)
                ], _options);

                chatHistory.Add($"You: {model.UserInput}");
                chatHistory.Add($"MTH404 Bot: {completion.Content[0].Text}");

                HttpContext.Session.Set("ChatHistory", chatHistory);
                model.UserInput = string.Empty;
            }
            model.ChatHistory = chatHistory;

            return View(model);
        }
    }
}

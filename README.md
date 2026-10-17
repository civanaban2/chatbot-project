# Azure Chatbot Integration

An ASP.NET Core MVC chatbot integration built around Azure OpenAI. The intended
use case is a customer assistant inside a pizza application: system instructions
define the assistant's role and scope, while the web interface sends customer
questions to a configured model deployment.

This repository contains the standalone chatbot interface and integration
layer, not a complete pizza ordering platform. It does not include a restaurant
menu database, payment flow, or order-management API.

## Features

- Azure OpenAI chat-completion integration.
- Configurable endpoint, deployment, system instructions, and output-token limit.
- A Razor chat interface with a session-scoped conversation transcript.
- MVC separation between controllers, view models, views, and session helpers.

## Requirements

- .NET 8 SDK. `global.json` selects the .NET 8 SDK family.
- An Azure OpenAI resource with an available chat model deployment.
- The resource endpoint, API key, and deployment name.

The project references `Azure.AI.OpenAI` 2.1.0 and `Newtonsoft.Json` 13.0.3.
`dotnet restore` installs those dependencies; do not add arbitrary newer package
versions just to run the application.

## Setup

```sh
git clone https://github.com/civanaban2/chatbot-project.git
cd chatbot-project
dotnet restore ChatBotProject.sln
dotnet build ChatBotProject.sln --no-restore
```

Set configuration using environment variables in the same terminal before
starting the application. Replace placeholders with your resource values:

```sh
export OpenAIAPIKey='YOUR_AZURE_OPENAI_API_KEY'
export OpenAIEndPoint='https://YOUR_RESOURCE.openai.azure.com/'
export OpenAIDeploymentName='YOUR_DEPLOYMENT_NAME'
export MaxOutputTokenCount='100'
```

Use the **deployment name** configured in Azure, which may differ from the
underlying model name. These environment variables override the placeholders
in `ChatBotProject/appsettings.json`. Keep real credentials outside tracked
files. ASP.NET Core reads environment variables directly; this project does
not automatically load `.env` files.

The checked-in `SystemMessage` provides a pizza-assistant example. Override it
with an environment variable if needed:

```sh
export SystemMessage='You are a customer assistant for a pizza application. Answer within the supplied application context and do not invent menu or order information.'
```

The example does not contain actual restaurant data. Prompts define intended
behavior; they are not a substitute for an authoritative menu or order API.

## Run

```sh
dotnet run --project ChatBotProject/ChatBotProject.csproj --launch-profile http
```

Open `http://localhost:5288/Mth404Bot/Index`. The root URL opens the template
home page; the chat screen is a separate route. The `http` launch profile sets
the Development environment. For an HTTPS launch, configure a development
certificate and use the `https` profile instead.

## Request flow

1. The browser submits `UserInput` to `Mth404BotController.Index`.
2. The controller loads the endpoint, credentials, deployment, system message,
   and token limit from ASP.NET Core configuration.
3. Each model call contains the system message and the **current user message**.
4. The reply is appended to the displayed transcript, serialized to JSON in
   HTTP session state, and rendered by the Razor view.

The stored transcript is **not sent back to the model** on subsequent turns.
The interface displays previous messages, but the model does not have
multi-turn conversational memory. Session data is held in process memory and
is lost when the application restarts; it is not a durable conversation store.

## Configuration keys

| Key | Purpose |
| --- | --- |
| `OpenAIAPIKey` | Azure OpenAI resource API key |
| `OpenAIEndPoint` | Azure OpenAI resource endpoint |
| `OpenAIDeploymentName` | Azure model deployment name |
| `SystemMessage` | Assistant instructions and application scope |
| `MaxOutputTokenCount` | Positive integer output-token limit; default example is 100 |

Configuration keys are case-insensitive. The controller reads `systemMessage`,
which resolves to the checked-in `SystemMessage` setting.

## Structure

```text
ChatBotProject.sln
global.json
ChatBotProject/
  Program.cs
  appsettings.json
  Controllers/MTH404BotController.cs
  Extensions/SessionExtensions.cs
  Models/ChatViewModel.cs
  Views/MTH404Bot/Index.cshtml
  Properties/launchSettings.json
```

The historical `MTH404` names remain in the source and UI. They are project
identifiers, not a separate AI model or Azure service.

## Scope and limitations

This is an educational integration prototype. It does not implement RAG,
menu retrieval, tools/agents, order placement, or persistent chat storage.
API calls are synchronous, and API-failure handling, rate limiting,
authentication, input limits, and production deployment controls require
further work. Customer messages are sent to the configured Azure service;
avoid using real customer data while evaluating the demo.

There is no automated test suite in the repository. Building checks local
compilation; an end-to-end response also requires valid Azure configuration
and a live model deployment.

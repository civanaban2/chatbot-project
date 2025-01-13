# ChatBot Project

## Table of Contents
1. [Project Description / Proje Açıklaması](#project-description--proje-açıklaması)
2. [Requirements / Gereksinimler](#requirements--gereksinimler)
3. [Installation / Kurulum](#installation--kurulum)
4. [Usage / Kullanım](#usage--kullanım)
---
## Project Description / Proje Açıklaması
- This project is a **WebApp** template written using the **MVC** template and containing a **chatbot** infrastructure using **Azure** services. You can personalize the **chatbot** as you wish and bring it to a format suitable for your application.  
- Bu proje, **MVC** şablonu kullanılarak yazılan ve **Azure** hizmetlerinden faydalanarak **chatbot** altyapısı bulunduran bir **WebApp** şablonudur. İsteğinize göre **chatbotu** kişiselleştirebilir ve uygulamanıza uygun bir biçime getirebilirsiniz.

---
## Requirements / Gereksinimler
- [**.NET SDK 8.0+**](https://dotnet.microsoft.com/)
- [**VS Code**](https://code.visualstudio.com) or [**Visual Studio**](https://visualstudio.microsoft.com) or [**Rider**](https://www.jetbrains.com/rider/)
- [**Azure**](https://azure.microsoft.com/) account with OpenAI services enabled / Etkin bir OpenAI servisinin bulunduğu bir [**Azure**](https://azure.microsoft.com/) hesabı
- Nuget Package Manager / Nuget Paket Yöneticisi
  1. Newtonsoft.Json
  2. Azure.AI.OpenAI
---
## Installation / Kurulum

1. Open terminal and clone the repository:   
   Terminali açın ve repoyu klonlayın: 
   ```bash
   git clone https://github.com/zaferemre/CartApp-MTH404.git [directory name]
2. Navigate to project directory:  
   Proje dizinine ilerleyin:
   ```bash
   cd [directory name]/chatbot-project
3. Manage nugets on IDE or Run this commands on terminal:  
   IDE üzerinden veya direkt terminalden gerekli nuget paketlerini kurun:
   ```bash
   dotnet add package Azure.AI.OpenAI
   dotnet add package Newtonsoft.Json
---
## Usage / Kullanım

1. You need to configure appsettings.json file for chatbot services.  
   appsettings.json dosyasını kendi servis bilgilerinize göre yapılandırın.
   
3. Build project:  
   Projeyi derleyin:
   ```bash
   dotnet build
4. Run project:  
   Projeyi çalıştırın:
   ```bash
   dotnet run
5. Once the server is running, it will generate a localhost URL (e.g., `http://localhost:3000`). Open your browser and navigate to given port:  
   Sunucu açıldığında bir link oluşturulacak (örneğin `http://localhost:3000`). Tarayıcınızı açın ve atanan adrese gidin:
   ```
   http://localhost:[Your Port]
   ```

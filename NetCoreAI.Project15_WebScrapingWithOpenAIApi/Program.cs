using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Text;

class Program
{
    private static readonly string apiKey = "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";

    static async Task Main(string[] args)
    {
        Console.Write("Lütfen analiz yapmak istediğiniz web sayfa URL'ini giriniz: ");
        string inputUrl;
        inputUrl = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine("Web sayfası içeriği: ");
        string webContent = ExtractTextFromWeb(inputUrl);
        await AnalyzeWithAI(webContent, "Web Sayfası İçeriği");

        static string ExtractTextFromWeb(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var bodyText = doc.DocumentNode.SelectSingleNode("//body")?.InnerText;
            return bodyText ?? "Sayfa içeriği okunamadı.";
        }

        static async Task AnalyzeWithAI(string text, string sourceType)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "system", content = "Sen bir yapay zeka asistanısın. Kullanıcının gönderdiği metni analiz eder ve Türkçe olarak özetlersin. Yanıtlarını sadece Türkçe ver!" },
                        new {role="user",content=$"Analyze and summarize the following {sourceType}:\n\n {text}"}
                    }
                };

                string json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    Console.WriteLine($"\n AI Analizi ({sourceType}): \n {result.choices[0].message.content}");
                }
                else
                {
                    Console.WriteLine("Hata: " + responseJson);
                }
            }
        }
    }
}
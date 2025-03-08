using Newtonsoft.Json;
using System.Text;

class Program
{
    private static readonly string apiKey = "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";

    static async Task Main(string[] args)
    {
        Console.Write("Bir metin giriniz: ");
        string input;
        input = Console.ReadLine();
        Console.WriteLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Gelişmiş duygu analizi yapılıyor...");
            string sentiment = await AdvancedSentimentalAnalysis(input);
            Console.WriteLine();
            Console.WriteLine($"\n Sonuç: \n{sentiment}");
        }


        static async Task<string> AdvancedSentimentalAnalysis(string text)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new {role="system",content="You are an advanced AI that analyzes emotions in text. Your response must be in JSON format. Identiyf the sentiment scores (0-100%) for the following emotions: Joy, Sadness, Anger, Fear, Surprise and Neutral."},
                        new {role="user", content=$"Analyze this text: \"{text}\" and return a JSON object with percentages for each emotions."}
                    }
                };

                string json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    string analyzsis = result.choices[0].message.content.ToString();
                    return analyzsis;
                }
                else
                {
                    Console.WriteLine($"Bir Hata Oluştur {responseJson}");
                    return "Hata!";
                }
            }
        }
    }
}
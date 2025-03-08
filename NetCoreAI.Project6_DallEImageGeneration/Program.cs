using Newtonsoft.Json;
using System.Text;

class Program
{
    public static async Task Main(string[] args)
    {
        string apiKey = "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";
        Console.Write("Çizilmesini istediğiniz içerik (example prompts): ");
        string prompt;
        prompt = Console.ReadLine();
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var requestBody = new
            {
                prompt = prompt,
                n = 1,
                size = "1024x1024"
            };

            string jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/images/generations", content);
            string responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
        }
    }
}
//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
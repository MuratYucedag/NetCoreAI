using System.Text;
using System.Text.Json;

class Program
{
    private static readonly string apiKey = "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";

    static async Task Main()
    {
        Console.Write("Hikaye Türünü Seçiniz (Macera, Korku, Bilim Kurgu, Fantastik, Komedi): ");
        string genre = Console.ReadLine();

        Console.Write("Ana karakteriniz kim: ");
        string character = Console.ReadLine();

        Console.Write("Hikaye nerede geçiyor: ");
        string setting = Console.ReadLine();

        Console.Write("Hikayenin uzunluğu (kısa/orta/uzun): ");
        string length = Console.ReadLine();

        string prompt = $"{genre} türünde bir hikaye yaz. Baş karakterin adı {character}. Hikaye {setting} bölgesinde geçiyor. {length} bir hikaye olsun. Giriş, gelişme ve sonuç içermeli.";

        string story = await GenerateStory(prompt);
        Console.WriteLine();
        Console.WriteLine("--- AI Tarafında Oluşturulan Hikaye ---\n");
        Console.WriteLine(story);
    }

    static async Task<string> GenerateStory(string prompt)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        var requestBody = new
        {
            model = "gpt-4-turbo",
            messages = new[]
            {
                new {role="system", content="You are a creative story writer."},
                new {role="user",content=prompt}
            },
            max_tokens = 1000
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", jsonContent);

        string responseContent = await response.Content.ReadAsStringAsync();
        JsonDocument doc = JsonDocument.Parse(responseContent);
        return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
    }
}
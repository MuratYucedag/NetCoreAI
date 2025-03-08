using Newtonsoft.Json;
using System.Text;

class Program
{
    private static readonly string apiKey = "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";

    static async Task Main(string[] args)
    {
        Console.Write("Metni Giriniz: ");
        string input;
        input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Ses dosyası oluşuturuluyor....");
            await GenerateSpeech(input);
            Console.Write("Ses dosyası 'output mp3' olarak kaydedildi!");
            System.Diagnostics.Process.Start("explorer.exe", "output.mp3");

        }

        static async Task GenerateSpeech(string text)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "tts-1",
                    input = text,
                    voice = "onyx"
                };

                string json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/audio/speech", content);

                if (response.IsSuccessStatusCode)
                {
                    byte[] audioBytes = await response.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync("output.mp3", audioBytes);
                }
                else
                {
                    Console.WriteLine("Bir hata oluştu");
                }

            }
        }
    }
}
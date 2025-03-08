using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;

class Program
{
    private static readonly string apiKey = "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";
    static async Task Main(string[] args)
    {
        Console.Write("Uzun metninizi veya makalenizi giriniz: ");
        string input;
        input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine();
            Console.WriteLine("Girmiş olduğunuz metin AI tarafından özetleniyor...");
            Console.WriteLine();

            string shortSummary = await SummarizeText(input, "short");
            string mediumSummary = await SummarizeText(input, "medium");
            string detailedSummary = await SummarizeText(input, "detailed");

            Console.WriteLine("Özetler");
            Console.WriteLine("------------------------");
            Console.WriteLine($" ** Kısa Özet: ** {shortSummary}");
            Console.WriteLine("------------------------");
            Console.WriteLine($" ** Orta Uzunlukta Özet: ** {mediumSummary}");
            Console.WriteLine("------------------------");
            Console.WriteLine($" ** Detaylı Özet: ** {detailedSummary}");
        }



        async Task<string> SummarizeText(string text, string level)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                string instruction = level switch
                {
                    "short" => "Summarize this text in 1-2 sentences.",
                    "medium" => "Summarize this text in 3-5 sentences.",
                    "detailed" => "Summarize this text in a detailed but concise manner.",
                    _ => "Summerize this text."
                };

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                    new {role="system",content="You are an AI that summarize text info different leves: short, medium and detailed."},
                    new {role="user",content=$"{instruction}\n\n{text}"}
                }
                };

                string json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    return result.choices[0].message.content.ToString();
                }
                else
                {
                    Console.WriteLine($"Hata: {responseJson}");
                    return "Hata!";
                }
            }
        }
    }
}
/*
 Kışkırtıcı mevsim, zorunlu hava ve şimdi dinlenme incenistan incenuş dönemi bir. Yağmur, kar ve don mevsim buinins ovaları. Dikişsel özellikler. Bulaca, Amal havası kalıcı sebzeler kışı manyik dabiry. Kış sebzeleri, düşük sıcaklık, dayanıklı, sazağı cücek hadâhı halhal dahalorder. Bu sebze, bağışıklık güçlendirici vitamin sistemi ve mineraller açısından zengin. Kışın en sebzeli vazıklar nel Lahana, ömrün zengini-orama sistemi. İyi kireci olun. Ispanak ve Paz devam ediyor, demir ve C vitaminii) virüsün içeriğine bağlıyoroz. Ve bu da, vauç ve Zülgede kök sebzeci, minik uz-aç havadan havadan çarpık havadan dahalü. PırasaAntikratikten, Cilt Direksiyonu, Karnabahar ve brokoli sebzeci hayvanatsız hayvanamcık benzeri değiştirilebilir değiştirilebilir. Kış mevsimi Sonsuza Dekkey Engelberiyerler up süzme, marul, roka, mün" der brsel lahana, maylyn Davlumbazlık. Bu sebzeleyici, yar-ikonik biriki beslenme mayası sağlıklıyekü. Özetle, kış ayları ayları hava ve-mil, hemanars herar ve luminalz, aynı doğa spazar sebzeli de bek bir zamanda. Bergar Üreli terkâk taze ve sebze geyiği beslenmek ürkütücü sürdürülebilir.
 */
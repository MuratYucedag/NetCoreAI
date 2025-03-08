using Tesseract;

class Program

{
    static void Main(string[] args)
    {
        Console.Write("Karakter Okuması Yapılacak Resim Yolu:");
        string imagePath = Console.ReadLine();
        Console.WriteLine();

        string tessDataPath = @"C:\tessdata";

        try
        {
            using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        string text = page.GetText();
                        Console.WriteLine("Resimden Okunan Metin...");
                        Console.WriteLine(text);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bir hata oluştu: {ex.Message}");
        }
        Console.ReadLine();
    }
}
using ImageTextReader.Domain.Interfaces;
using Tesseract;

namespace ImageTextReader.Infrastructure.Services
{
    public class TesseractOcrService : IOcrService
    {
        private readonly string _tessDataPath;

        public TesseractOcrService(string tessDataPath)
        {
            _tessDataPath = tessDataPath;
        }

        public Task<string> ExtractTextAsync(byte[] imageData)
        {
            return Task.Run(() =>
            {
                using var engine = new TesseractEngine(_tessDataPath, "eng", EngineMode.Default);
                using var img = Pix.LoadFromMemory(imageData);
                using var page = engine.Process(img);
                return page.GetText();
            });
        }
    }
}

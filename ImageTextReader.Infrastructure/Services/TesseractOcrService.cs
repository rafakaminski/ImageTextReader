using ImageTextReader.Domain.Interfaces;
using Tesseract;

namespace ImageTextReader.Infrastructure.Services
{
    public class TesseractOcrService : IOcrService
    {
        private readonly string _tessDataPath;
        private readonly string _imagesFolder = @"C:\ImageTextReader\images";

        public TesseractOcrService(string tessDataPath)
        {
            if (!Directory.Exists(tessDataPath))
                throw new DirectoryNotFoundException($"A pasta tessdata não foi encontrada em: {tessDataPath}");

            _tessDataPath = tessDataPath;

            if (!Directory.Exists(_imagesFolder))
                Directory.CreateDirectory(_imagesFolder);
        }

        public Task<string> ExtractTextAsync(byte[] imageData)
        {
            return Task.Run(() =>
            {
                SaveImage(imageData);
                using var engine = new TesseractEngine(_tessDataPath, "eng", EngineMode.Default);

                using var img = Pix.LoadFromMemory(imageData);
                using var page = engine.Process(img);

                return page.GetText()?.Trim() ?? string.Empty;
            });
        }

        private void SaveImage(byte[] imageData)
        {
            using var ms = new MemoryStream(imageData);
            using var img = System.Drawing.Image.FromStream(ms);

            string fileName = Path.Combine(_imagesFolder, $"{Guid.NewGuid()}.png");
            img.Save(fileName);
        }
    }
}

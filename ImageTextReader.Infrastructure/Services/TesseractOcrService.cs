using ImageTextReader.Domain.Interfaces;
using Tesseract;

namespace ImageTextReader.Infrastructure.Services
{
    public class TesseractOcrService : IOcrService
    {
        private readonly string _tessDataPath;

        public TesseractOcrService(string tessDataPath)
        {
            if (!Directory.Exists(tessDataPath))
                throw new DirectoryNotFoundException($"A pasta tessdata não foi encontrada em: {tessDataPath}");

            _tessDataPath = tessDataPath;
        }

        public Task<string> ExtractTextAsync(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                throw new ArgumentException("O array de bytes da imagem está vazio.", nameof(imageData));

            return Task.Run(() =>
            {
                using var engine = new TesseractEngine(_tessDataPath, "eng", EngineMode.Default);

                using var img = Pix.LoadFromMemory(imageData);
                using var page = engine.Process(img);

                var text = page.GetText()?.Trim() ?? string.Empty;
                return text;
            });
        }
    }
}

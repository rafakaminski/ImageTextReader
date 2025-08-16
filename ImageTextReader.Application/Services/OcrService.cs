using ImageTextReader.Domain.Interfaces;

namespace ImageTextReader.Application.Services
{
    public class OcrService : IOcrService
    {
        private readonly IOcrService _ocrInfra;

        public OcrService(IOcrService ocrInfra)
        {
            _ocrInfra = ocrInfra;
        }

        public async Task<string> ExtractTextAsync(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
            {
                throw new ArgumentException("Image data cannot be null or empty.", nameof(imageData));
            }
            return await _ocrInfra.ExtractTextAsync(imageData);
        }
    }
}

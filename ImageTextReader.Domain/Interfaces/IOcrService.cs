namespace ImageTextReader.Domain.Interfaces
{
    public interface IOcrService
    {
        Task<string> ExtractTextAsync(byte[] imageData);
    }
}

namespace ImageTextReader.Domain.Interfaces
{
    public class IOcrService
    {
        Task<string> ExtractTextAsync(byte[] imageData);
    }
}

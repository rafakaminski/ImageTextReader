using ImageTextReader.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageTextReader.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OcrController : ControllerBase
    {
        private readonly OcrService _ocrService;

        public OcrController(OcrService ocrService)
        {
            _ocrService = ocrService;
        }

        [HttpPost(Name = "extract-text")]
        public async Task<IActionResult> ExtractText([FromBody] ImageTextReader request)
        {
            try
            {
                string base64 = request.ImageBase64;
                if (base64.Contains(","))
                    base64 = base64.Split(',')[1];

                base64 = base64.Replace("\r", "").Replace("\n", "").Trim();

                byte[] imageBytes = Convert.FromBase64String(base64);

                if (imageBytes.Length == 0)
                    return BadRequest("Imagem inválida");

                var text = await _ocrService.ExtractTextAsync(imageBytes);
                return Ok(new { Text = text });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        public class ImageTextReader
        {
            public string ImageBase64 { get; set; }
        }
    }
}
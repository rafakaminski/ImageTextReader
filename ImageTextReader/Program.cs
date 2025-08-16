using ImageTextReader.Application.Services;
using ImageTextReader.Domain.Interfaces;
using ImageTextReader.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var tessDataPath = Path.Combine(builder.Environment.ContentRootPath, "tessdata");

builder.Services.AddSingleton<IOcrService>(provider =>
    new TesseractOcrService(tessDataPath));

builder.Services.AddScoped<OcrService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

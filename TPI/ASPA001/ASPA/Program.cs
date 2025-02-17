var builder = WebApplication.CreateBuilder(args);

// Добавление службы HTTP Logging
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096; // Максимальный размер логируемого тела запроса
    logging.ResponseBodyLogLimit = 4096; // Максимальный размер логируемого тела ответа
    logging.MediaTypeOptions.AddText("application/json");
    logging.MediaTypeOptions.AddText("text/plain");
});

var app = builder.Build();

// Использование HTTP Logging в пайплайне обработки запросов
app.UseHttpLogging();

app.MapGet("/", () => "Hello World!");

app.Run();

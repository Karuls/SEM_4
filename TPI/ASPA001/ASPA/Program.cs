var builder = WebApplication.CreateBuilder(args);

// ���������� ������ HTTP Logging
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096; // ������������ ������ ����������� ���� �������
    logging.ResponseBodyLogLimit = 4096; // ������������ ������ ����������� ���� ������
    logging.MediaTypeOptions.AddText("application/json");
    logging.MediaTypeOptions.AddText("text/plain");
});

var app = builder.Build();

// ������������� HTTP Logging � ��������� ��������� ��������
app.UseHttpLogging();

app.MapGet("/", () => "Hello World!");

app.Run();

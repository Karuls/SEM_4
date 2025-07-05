using DAL004;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Logging.AddConsole();
/*app.MapGet("/", () => "Hello World!");

app.Run();
*/



Repository.JSONFileName = "Celebrities.json";
using (IRepository repository = Repository.Create("C:\\Users\\fedor\\OneDrive\\Desktop\\ASPA\\Celebrities"))
{
    string jsonFilePath = Path.Combine(repository.BasePath, Repository.JSONFileName);
    app.UseExceptionHandler("/Celebrities/Error");

    app.MapGet("/Celebrities", ()=> repository.getAllCelebrities());
    app.MapGet("/Celebrities/{id:int}", (int id) =>
    {
        Celebrity? celebrity = repository.getCelebrityById(id);
        if (celebrity == null)
            throw new FoundByIdException($"Celebrity Id = {id}");
        return celebrity;
    });
    app.MapPost("/Celebrities", (Celebrity celebrity) =>
    {
      
        int? id = repository.addCelebrity(celebrity);
        if (id == null) throw new AddCelebrityException("/Celebrities error, id == null");
        if (repository.SaveChange() <= 0) throw new SaveException("/Celebrities error, SaveChange() <= 0");
   
       if (!File.Exists(jsonFilePath))
        {
            throw new FileNotFoundException($"Cannot find the JSON file at {jsonFilePath}.");
        }
        return new Celebrity((int)id, celebrity.Firstname, celebrity.Surname, celebrity.PhotoPath);

    });
    app.MapFallback((HttpContext ctx) => Results.NotFound(new { error = $"path {ctx.Request.Path} not supported" }));
    app.Map("/Celebrities/Error", (HttpContext ctx) =>
    {
        Exception? ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
        IResult rc = Results.Problem(detail: $"Could not find file {jsonFilePath}", instance: app.Environment.EnvironmentName, title: "ASPA004", statusCode: 500);
        if(ex != null)
        {
            if (ex is FileNotFoundException) rc = Results.Problem(title: "ASPA004", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
            if (ex is FoundByIdException) rc = Results.Problem(title: "ASPA004/FoundByIdException", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
            if (ex is BadHttpRequestException) rc = Results.Problem(title: "ASPA004/BadHttpRequestException", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
            if (ex is SaveException) rc= Results.Problem(title: "ASPA004/SaveException", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
            if (ex is AddCelebrityException) rc = Results.Problem(title: "ASPA004/AddCelebrityException", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
        }
        return rc;
    });
    app.Run();
}

public class FoundByIdException : Exception
{
    public FoundByIdException(string message) : base($"Found by Id: {message}") { }
}
public class AddCelebrityException : Exception
{
    public AddCelebrityException(string message) : base($"SaveChanges error: {message}") { }
}

public class SaveException : Exception
{
    public SaveException(string message) : base($"AddCelebrityException error: {message}") { }
}


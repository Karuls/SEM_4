using DAL004;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
builder.Logging.AddConsole();
/*app.MapGet("/", () => "Hello World!");

app.Run();
*/



Repository.JSONFileName = "Celebrities.json";
using (IRepository repository = Repository.Create("C:\\Users\\fedor\\OneDrive\\Desktop\\ASPA\\Celebrities"))
{
    app.UseExceptionHandler("/Celebrities/Error");

    app.MapGet("/Celebrities", () => repository.getAllCelebrities());
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
        return new Celebrity((int)id, celebrity.Firstname, celebrity.Surname, celebrity.PhotoPath);
    });
    app.MapDelete("/Celebrities/{id:int}", (int id) =>
    {
        bool deleted = repository.delCelebrityById(id);
        if (!deleted)
            throw new FoundByIdException($"DELETE /Celebrities error,  Id = {id}");

        if (repository.SaveChange() <= 0)
            throw new SaveException("/Celebrities error during deletion: SaveChange() <= 0");

        return Results.Ok(new {message = $"Celebrity with Id = {id} deleted" });

    });
    app.MapFallback((HttpContext ctx) => Results.NotFound(new { error = $"path {ctx.Request.Path} not supported" }));
    app.Map("/Celebrities/Error", (HttpContext ctx) =>
    {
        Exception? ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
        IResult rc = Results.Problem(detail: "Panic", instance: app.Environment.EnvironmentName, title: "ASPA004", statusCode: 500);
        if (ex != null)
        {
            if(ex is DelbyIdException) rc = Results.NotFound(ex.Message);
            if (ex is FileNotFoundException) rc = Results.Problem(title: "ASPA004", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
            if (ex is FoundByIdException) rc = Results.NotFound(ex.Message);
            if (ex is BadHttpRequestException) rc = Results.BadRequest(ex.Message);
            if (ex is SaveException) rc = Results.Problem(title: "ASPA004/SaveChanges", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
            if (ex is AddCelebrityException) rc = Results.Problem(title: "ASPA004/addCelebrity", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
        }
        return rc;
    });
    app.Run();
}

public class DelbyIdException : Exception
{
    public DelbyIdException(string message) : base($"Delete by Id: {message}") { }
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


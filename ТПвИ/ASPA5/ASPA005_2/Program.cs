using DAL004;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRepository>(sp => Repository.Create("C:\\Users\\fedor\\OneDrive\\Desktop\\ASPA\\Celebrities"));
builder.Services.AddScoped<Validation.SurnameFilter>();
builder.Services.AddScoped<Validation.PhotoExistFilter>();
builder.Services.AddScoped<Validation.ValidatePutFilter>();
builder.Services.AddScoped<Validation.ValidateDeleteFilter>();
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
    RouteGroupBuilder api = app.MapGroup("/Celebrities");
    app.MapGet("/Celebrities", () => repository.getAllCelebrities());
    app.MapGet("/Celebrities/{id:int}", (int id) =>
    {
        Celebrity? celebrity = repository.getCelebrityById(id);
        if (celebrity == null)
            throw new FoundByIdException($"Celebrity Id = {id}");
        return celebrity;
    });

/*    Validation.SurnameFilter.repository = Validation.PhotoExistFilter.repository = repository;*/
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

    })
    .AddEndpointFilter<Validation.SurnameFilter>()
    .AddEndpointFilter<Validation.PhotoExistFilter>();


    app.MapDelete("/Celebrities/{id:int}", (int id) =>
    {
        bool deleted = repository.delCelebrityById(id);
        /*        if (!deleted)
                    throw new FoundByIdException($"DELETE /Celebrities error,  Id = {id}");
        */
        if (repository.SaveChange() <= 0)
            throw new SaveException("/Celebrities error during deletion: SaveChange() <= 0");

        return Results.Ok(new { message = $"Celebrity with Id = {id} deleted" });

    })
    .AddEndpointFilter<Validation.ValidateDeleteFilter>();

    app.MapPut("/Celebrities/{id:int}", (int id, Celebrity celebrity) =>
    {
        celebrity = celebrity with { Id = id };
        int? updatedId = repository.upCelebrityById(id, celebrity);
        if (updatedId == null)
        {
            throw new FoundByIdException($"Celebrity with Id = {id} not found for update.");
        }

        if (repository.SaveChange() <= 0)
        {
            throw new SaveException("/Celebrities error during update: SaveChange() <= 0");
        }

        return Results.Ok(new Celebrity((int)updatedId, celebrity.Firstname, celebrity.Surname, celebrity.PhotoPath));
    });
/*    .AddEndpointFilter<Validation.ValidatePutFilter>();*/

    app.MapFallback((HttpContext ctx) => Results.NotFound(new { error = $"path {ctx.Request.Path} not supported" }));
    app.Map("/Celebrities/Error", (HttpContext ctx) =>
    {
        Exception? ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;
        IResult rc = Results.Problem(detail: $"Could not find file {jsonFilePath}", instance: app.Environment.EnvironmentName, title: "ASPA004", statusCode: 500);
        if (ex != null)
        {
            if (ex is FoundSameSurnameException) rc = Results.Problem(title: "ASPA005/FoundSameSurnameException", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 409);
            if (ex is FoundWrongSurnameException) rc = Results.Problem(title: "ASPA005/FoundWrongSurnameException", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 409);
            if (ex is FoundNullCelebrityException) rc = Results.Problem(title: "ASPA005/FoundNullCelebrityException", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
            if (ex is FileNotFoundException) rc = Results.Problem(title: "ASPA004", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
            if (ex is FoundByIdException) rc = Results.Problem(title: "ASPA004/FoundByIdException", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
            if (ex is BadHttpRequestException) rc = Results.Problem(title: "ASPA004/BadHttpRequestException", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
            if (ex is SaveException) rc = Results.Problem(title: "ASPA004/SaveException", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
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

public class FoundNullCelebrityException : Exception
{
    public FoundNullCelebrityException(string message) : base($"Exception error: {message}") { }
}

public class FoundWrongSurnameException : Exception
{
    public FoundWrongSurnameException(string message) : base($"Value:POST {message}") { }
}

public class FoundSameSurnameException : Exception
{
    public FoundSameSurnameException(string message) : base($"Value:POST {message}") { }
}

namespace Validation
{
    public class SurnameFilter : IEndpointFilter
    {
        private readonly IRepository repository;

        public SurnameFilter(IRepository repository)
        {
            this.repository = repository;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var celebrity = context.GetArgument<Celebrity>(0);

            if (celebrity == null)
                throw new FoundNullCelebrityException("/Celebrities error, celebrity == null");

            if (string.IsNullOrEmpty(celebrity.Surname) || celebrity.Surname.Length < 2)
                throw new FoundWrongSurnameException("/Celebrities error, Surname is wrong");

            if (repository.DoesSurnameExist(celebrity.Surname))
                throw new FoundSameSurnameException("/Celebrities error, Surname is doubled");

            return await next(context);
        }
    }

    public class PhotoExistFilter : IEndpointFilter
    {
        private readonly string basePath = "D:\\labs\\4semestr\\ProgrammingTechnologyInTheInternet\\labs\\lab4\\ASPA\\Celebrities";

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var celebrity = context.GetArgument<Celebrity>(0);

            if (celebrity == null)
                throw new FoundNullCelebrityException("/Celebrities error, celebrity == null");

            var fileName = Path.GetFileName(celebrity.PhotoPath);

            if (!File.Exists(Path.Combine(basePath, fileName)))
            {
                context.HttpContext.Response.Headers.Add("X-Celebrity", $"NotFound = {fileName}");
            }

            return await next(context);
        }
    }
    public class ValidatePutFilter : IEndpointFilter
    {
        private readonly IRepository repository;

        public ValidatePutFilter(IRepository repository)
        {
            this.repository = repository;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var celebrity = context.GetArgument<Celebrity>(0);

            if (celebrity == null)
                throw new ArgumentNullException(nameof(celebrity), "The provided object is null.");
     
            if (!repository.DoesCelebrityExist(celebrity.Id))
                throw new FoundByIdException($"Celebrity with ID {celebrity.Id} does not exist.");
          
            return await next(context);
        }
    }

    public class ValidateDeleteFilter : IEndpointFilter
    {
        private readonly IRepository repository;

        public ValidateDeleteFilter(IRepository repository)
        {
            this.repository = repository;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var id = context.GetArgument<int>(0);

            
            if (!repository.DoesCelebrityExist(id))
            {
                throw new FoundByIdException($"DELETE /Celebrities error,  Id = {id}");
            }
    
            return await next(context);
        }
    }
}






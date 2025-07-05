using DAL_Celebrity_MSSQL;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using Exceptions;
using System.Data.Common;
using ASPA007_1;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.AddCelebritiesConfiguration();          // конфигурация Celebrities
        builder.AddCelebritiesServices();               // сервисы Celebrities


        builder.Services.AddRazorPages();
        builder.Services.AddRazorPages(
            o =>
            {
                o.Conventions.AddPageRoute("/Celebrities", "/");                            // все фотографии
                o.Conventions.AddPageRoute("/NewCelebrity", "/0");                          // добавление новой знаменитости
                o.Conventions.AddPageRoute("/Celebrity", "/Celebrities/{id:int:min(1)}");   // отображение знаменитости по id
                o.Conventions.AddPageRoute("/Celebrity", "/{id:int:min(1)}");               // отображение знаменитости с id
            }
        );

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.MapCelebrities();
        app.MapLifeevents();
        app.MapPhotoCelebrities();

        app.Run();

    }
}

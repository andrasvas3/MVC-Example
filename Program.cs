using System.IO.Compression;
using MVC_Example.Models;
using Microsoft.AspNetCore.ResponseCompression;

namespace MVC_Example;

public class Program
{
    private readonly static DatabaseContext databaseContext = new();
    public static void Main(string[] args)
    {
        databaseContext.Database.EnsureDeleted();
        databaseContext.Database.EnsureCreated();

        databaseContext.Employees.Add(new Employee { Name = "Bill", Salary = 19200 });
        databaseContext.Employees.Add(new Employee { Name = "Jane", Salary = 17600 });
        databaseContext.Employees.Add(new Employee { Name = "John", Salary = 11900 });
        databaseContext.Employees.Add(new Employee { Name = "Aaron", Salary = 20400 });
        databaseContext.Employees.Add(new Employee { Name = "Catherine", Salary = 21500 });
        databaseContext.SaveChanges();

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        builder.Services.AddResponseCompression();
        builder.Services.Configure<GzipCompressionProviderOptions>(o => o.Level = CompressionLevel.Fastest);

        var app = builder.Build();
        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllerRoute(name: "default", pattern: "{controller=Site}/{action=Home}");
        app.Run();
    }
}

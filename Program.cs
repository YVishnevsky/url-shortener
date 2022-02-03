using Microsoft.AspNetCore.Builder;
using Nix.Tasks.UrlShortener.Infrastructure;

//seed data
var idProvider = new SimpleUniqueStringProvider(110000, new[] { "index", "privacy" });
new InMemoryUrlStorage().Add(new Url(id: idProvider.GetNewId(), value: "https://google.com"));

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IUniqueStringProvider>(idProvider);
builder.Services.AddSingleton<IUrlStorage>(sp => new InMemoryUrlStorage());
builder.Services.AddSingleton<IRequestLogger>(sp => new SimpleRequestLogger($"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location)}/requests_{DateTime.Today:yyyyMMdd}.txt"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.Use((context, next) =>
    {
        var storage = ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IUrlStorage>();
        var url = storage.GetOne(context.Request.Path.ToString()[1..]);
        if (url is not null)
        {
            var logger = ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IRequestLogger>();
            logger.Write($"[{DateTime.Now:G}.{DateTime.Now:fff}] {context.Connection.RemoteIpAddress} {context.Request.Method} {context.Request.Path} redirect to '{url.Value}'");
            context.Response.Redirect(url.Value);
            return Task.CompletedTask;
        }
        else
        {
            return next(context);
        }
    });

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();


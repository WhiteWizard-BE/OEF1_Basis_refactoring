using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VivesBlog.Core;
using VivesBlog.Cyb.Ui.Mvc.Controllers;
using VivesBlog.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VivesBlogDbContext>(ConfigureDbContext);

builder.Services.AddScoped<BlogService>();
builder.Services.AddScoped<PeopleService>();

var app = builder.Build();

ConfigureApplication(app);

app.Run();

void ConfigureDbContext(DbContextOptionsBuilder options)
{
    options.UseInMemoryDatabase(nameof(VivesBlogDbContext));
}

void ConfigureApplication(WebApplication app)
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }
    else
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<VivesBlogDbContext>();
        if (dbContext.Database.IsInMemory())
        {
            dbContext.Seed();
        }
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}

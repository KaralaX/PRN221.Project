using PRN221.Project.Application;
using PRN221.Project.Infrastructure;
using PRN221.Project.Infrastructure.Persistence;
using PRN221.WebUi;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplicationServices()
        .AddInfrastructureServices(builder.Configuration)
        .AddWebUiServices();
}

var app = builder.Build();
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }
    else
    {
        await app.InitializeDatabaseAsync();
    }
            
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();
    app.UseAuthentication();

    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
}
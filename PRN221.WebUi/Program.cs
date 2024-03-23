using PRN221.Project.Application;
using PRN221.Project.Infrastructure;
using PRN221.Project.Infrastructure.Persistence;
using PRN221.Project.Infrastructure.VnPay.Services;
using PRN221.WebUi;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplicationServices()
        .AddInfrastructureServices(builder.Configuration)
        .AddWebUiServices();
}
builder.Services.AddScoped<IVnPayService, VnPayService>();

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

    app.UseSession();
    
    app.UseAuthentication();

    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
}
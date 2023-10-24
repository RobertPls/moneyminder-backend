using Infrastructure.Security;
using Web;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            IWebHostEnvironment env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            SecurityInitializer securityInitializer = scope.ServiceProvider.GetRequiredService<SecurityInitializer>();


            string contentRootPath = env.ContentRootPath;
            var permissionJsonFilePath = contentRootPath + "/DataFiles/permissions.json";
            var securityInitializationJsonFilePath = contentRootPath + "/DataFiles/initializer.json";

            await securityInitializer.Initialize(permissionJsonFilePath, securityInitializationJsonFilePath);

        }

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .UseContentRoot(Directory.GetCurrentDirectory())

           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>();
           });

}


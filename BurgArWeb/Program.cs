public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // builder.Services.AddRazorPages();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        var staticFileOptions = GetStaticFileOptions(app);
        app.UseStaticFiles(staticFileOptions);

        app.UseRouting();

        // app.UseAuthorization();
        // app.MapRazorPages();

        app.MapFallbackToFile("{*path}", "/index.html", staticFileOptions);

        app.Run();
    }

    private static StaticFileOptions GetStaticFileOptions(WebApplication app)
    {
        StaticFileOptions staticFileOptions;
        var wwwRootPath = app.Environment.IsProduction()
            ? app.Environment.WebRootPath
            : Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, @"..\BurgArClient\dist\burg-ar-client"))
            ;
        var fileProvider = new PhysicalFileProvider(wwwRootPath);
        staticFileOptions = new StaticFileOptions
        {
            RequestPath = new PathString(""),
            FileProvider = fileProvider
        };
        return staticFileOptions;
    }
}
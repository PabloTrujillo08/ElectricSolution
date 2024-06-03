using ElectricSolution.Server.Infrastructure.dbContext;
using Microsoft.EntityFrameworkCore;

namespace ElectricSolution.Server.Settings
{
    public static class AppProgram
    {
        public static void LoadConfiguration(WebApplication app)
        {
            RunMigrations(app);
            LoadAppSettings(app);

            app.Run();
        }

        private static async void RunMigrations(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dbContext = services.GetRequiredService<ElectricDbContext>();
                    dbContext.Database.Migrate(); // Asegúrate de que las migraciones de Identity y otras estén aplicadas

                    // Inicializa los datos de roles y características asincrónicamente
                    await RolesDataSeed.Initialize(services);
                    await FeaturesDataSeed.Initialize(services);
                    await TariffDataSeed.Initialize(services);
                    await ProvincesDataSeed.Initialize(services);

                    // Aquí puedes añadir cualquier lógica adicional de inicialización de la base de datos,
                    // como la creación de roles iniciales, usuarios administradores, etc.
                }
                catch (Exception ex)
                {
                    // Considera añadir algún manejo de errores aquí, como loguear el error
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Un error ocurrió mientras se aplicaban las migraciones.");
                }
            }
        }

        private static void LoadAppSettings(WebApplication app)
        {
            // Configura el middleware para servir archivos predeterminados y estáticos
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            // Configura CORS según las políticas definidas en BuilderProgram.cs
            app.UseCors("MyPolicy");

            // Configura el middleware de autenticación y autorización
            app.UseAuthentication();
            app.UseAuthorization();

            // Configura Swagger/OpenAPI si está en desarrollo
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Configura el mapeo de controladores y la página de fallback para SPA
            app.MapControllers();
            app.MapFallbackToFile("/index.html");
        }
    }
}

using ElectricSolution.Server.Settings;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Call to the method that configures the builder
WebApplication app = BuilderProgram.ConfigureBuilder(builder);

AppProgram.LoadConfiguration(app);



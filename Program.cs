using ControlIDMvc;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});
var Startup = new Startup(builder.Configuration);
Startup.ConfigureServices(builder);

var app = builder.Build();
//add automapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configure the HTTP request pipeline.
Startup.Configure(app, app.Environment);

app.Run();

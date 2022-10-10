using ControlIDMvc;

var builder = WebApplication.CreateBuilder(args);

var Startup = new Startup(builder.Configuration);
Startup.ConfigureServices(builder);

var app = builder.Build();
//add automapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configure the HTTP request pipeline.
Startup.Configure(app, app.Environment);

app.Run("http://localhost:6000");

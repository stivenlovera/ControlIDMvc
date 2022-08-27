using ControlIDMvc.Querys;
using ControlIDMvc.Services;
using ControlIDMvc.Services.QueryControlId;
using Microsoft.EntityFrameworkCore;

namespace ControlIDMvc
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(WebApplicationBuilder Services)
        {
            // Add services to the container.
            Services.Services.AddControllersWithViews();
            //conect database
            var connectionString = Services.Configuration.GetConnectionString("DefaultConnection");
            Services.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
            Services.Services.AddDbContext<DBContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            Services.Services.AddHttpContextAccessor();
            Services.Services.AddHttpClient();
            Services.Services.AddTransient<HttpClientService>();
            Services.Services.AddTransient<PersonaQuery>();
            
            Services.Services.AddTransient<LoginControlIdQuery>();
            Services.Services.AddTransient<UsuarioControlIdQuery>();

        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

        }
    }
}
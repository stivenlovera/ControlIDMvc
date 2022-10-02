using ControlIDMvc.Entities;
using ControlIDMvc.Querys;
using ControlIDMvc.ServicesCI;
using ControlIDMvc.ServicesCI.QueryCI;
using Microsoft.AspNetCore.Identity;
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
            /*sistema*/
            Services.Services.AddTransient<PersonaQuery>();
            Services.Services.AddTransient<TarjetaQuery>();
            Services.Services.AddTransient<HorarioQuery>();
            Services.Services.AddTransient<AreaQuery>();
            Services.Services.AddTransient<AreaReglasAccesoQuery>();
            Services.Services.AddTransient<ReglaAccesoQuery>();
            Services.Services.AddTransient<PersonaReglaAccesoQuery>();
            Services.Services.AddTransient<AccionQuery>();
            Services.Services.AddTransient<PortalAccionQuery>();
            Services.Services.AddTransient<PortalQuery>();
            Services.Services.AddTransient<PortalReglasAccesoQuery>();
            Services.Services.AddTransient<HorarioReglaAccesoQuery>();
            Services.Services.AddTransient<DispositivoQuery>();
            Services.Services.AddTransient<PaqueteQuery>();
            Services.Services.AddTransient<InscripcionQuery>();

            /*Controlador*/
            Services.Services.AddTransient<LoginControlIdQuery>();
            Services.Services.AddTransient<UsuarioControlIdQuery>();
            Services.Services.AddTransient<CardControlIdQuery>();
            Services.Services.AddTransient<HorarioControlIdQuery>();
            Services.Services.AddTransient<UsuarioRulesAccessControlIdQuery>();
            Services.Services.AddTransient<AccessRulesControlIdQuery>();
            Services.Services.AddTransient<HorarioAccessRulesControlIdQuery>();
            Services.Services.AddTransient<PortalsAccessRulesControlIdQuery>();
            Services.Services.AddTransient<PortalsControlIdQuery>();
            Services.Services.AddTransient<ActionsControlIdQuery>();
            Services.Services.AddTransient<PortalsActionsControlIdQuery>();
            Services.Services.AddTransient<AreaControlIdQuery>();

            Services.Services.AddAutoMapper(typeof(Startup));
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
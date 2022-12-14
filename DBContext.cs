using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using ControlIDMvc.Entities;
namespace ControlIDMvc
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Tarjeta> Tarjeta { get; set; }
        public DbSet<Horario> Horario { get; set; }
        public DbSet<Dia> Dia { get; set; }
        public DbSet<Proyecto> Proyecto { get; set; }
        public DbSet<PersonaReglasAcceso> PersonaReglasAcceso { get; set; }
        public DbSet<ReglaAcceso> ReglaAcceso { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<AreaReglaAcceso> AreaReglaAcceso { get; set; }
        public DbSet<PortalReglaAcceso> PortalReglaAcceso { get; set; }
        public DbSet<Portal> Portal { get; set; }
        public DbSet<AccionPortal> AccionPortal { get; set; }
        public DbSet<Accion> Accion { get; set; }
        public DbSet<HorarioReglaAcceso> HorarioReglaAcceso { get; set; }
        public DbSet<Dispositivo> Dispositivo { get; set; }
        public DbSet<Inscripcion> Inscripcion { get; set; }
        public DbSet<Paquete> Paquete { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<RolUsuario> RolUsuario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Caja> Caja { get; set; }
        public DbSet<Egreso> Egreso { get; set; }
        public DbSet<PlanCuentaGrupo> PlanCuentaGrupo { get; set; }
        public DbSet<PlanCuentaRubro> PlanCuentaRubro { get; set; }
        public DbSet<PlanCuentaTitulo> PlanCuentaTitulo { get; set; }
        public DbSet<PlanCuentaCompuesta> PlanCuentaCompuesta { get; set; }
        public DbSet<PlanCuentaSubCuenta> PlanCuentaSubCuenta { get; set; }
        public DbSet<MovimientosAsiento> MovimientosAsiento { get; set; }
        public DbSet<Asiento> Asiento { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<TipoMovimiento> TipoMovimiento { get; set; }
        public DbSet<ImagenPerfil> ImagenPerfil { get; set; }
        public DbSet<PlanAsiento> PlanAsiento { get; set; }
        public DbSet<MetodoPago> MetodoPago { get; set; }
    }
}


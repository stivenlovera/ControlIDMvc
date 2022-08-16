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
        public DbSet<Proyecto> Proyecto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
    }
}


using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.BD.DATA.Entity;

namespace Z.BD.DATA
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options){ }
        
        
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Presupuesto> Presupuestos { get; set; }
        public DbSet<Obra> Obras { get; set; }
        public DbSet<ItemObra> ItemObra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔥 Guardar enum como string en SQL Server
            modelBuilder.Entity<Presupuesto>()
                .Property(p => p.Rubro)
                .HasConversion<string>();

            modelBuilder.Entity<Obra>()
                .Property(o => o.Rubro)
                .HasConversion<string>();

            // Configuración de decimales
            modelBuilder.Entity<ItemObra>()
                .Property(i => i.PrecioUnitario)
                .HasPrecision(18, 2);   // 18 dígitos, 2 decimales

            modelBuilder.Entity<Presupuesto>()
                .Property(p => p.Total)
                .HasPrecision(18, 2);

            // Evitar borrado en cascada
            var cascadeFKs = modelBuilder.Model.G­etEntityTypes()
                                         .SelectMany(t => t.GetForeignKeys())
                                         .Where(fk => !fk.IsOwnership
                                                      && fk.DeleteBehavior == DeleteBehavior.Casca­de);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restr­ict;
            }

            base.OnModelCreating(modelBuilder);
        }

        
    }
}

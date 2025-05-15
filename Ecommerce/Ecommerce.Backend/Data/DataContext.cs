using Ecommerce.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Backend.Data
{
    // Clase para hacer la conexión a la base datos. (para agregar tablas)
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        // Con esto se construye la tablas  en sql server
        public DbSet<Categoria> Categorias { get; set; } = null!;

        // Se sobre escribe el metodo  OnModelCreating.

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Significa que no se puede tener 2 categorias con el mismo nombre.
            modelBuilder.Entity<Categoria>().HasIndex(c => c.Nombre).IsUnique();

                
        }

    }
}

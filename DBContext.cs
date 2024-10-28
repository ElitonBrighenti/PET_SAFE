using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSafe
{
    public class AppDbContext : DbContext
    {
        // Tabelas no banco de dados
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pet> Pets { get; set; }

        // Configuração da string de conexão para PostgreSQL
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure a string de conexão para o banco de dados PostgreSQL
            optionsBuilder.UseNpgsql("server=localhost;username=postgres;database=petsafe;Password=2024");
        }

        // Configuração do modelo de entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Garantir que as chaves primárias estejam configuradas
            modelBuilder.Entity<Usuario>().HasKey(u => u.IDUsuario);
            modelBuilder.Entity<Pet>().HasKey(p => p.IDPet);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Pets)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.IDUsuario);
        }
    }
}

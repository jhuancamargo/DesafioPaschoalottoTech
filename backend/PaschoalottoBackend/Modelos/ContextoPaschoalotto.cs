using Microsoft.EntityFrameworkCore;

namespace PaschoalottoBackend.Modelos
{
public class ContextoPaschoalotto : DbContext
{
    public ContextoPaschoalotto(DbContextOptions<ContextoPaschoalotto> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().ToTable("Usuarios"); // conecxão com a tabela Usuarios lá no postgres
    }
}}
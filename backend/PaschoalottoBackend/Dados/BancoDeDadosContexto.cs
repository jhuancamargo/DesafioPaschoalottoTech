using Microsoft.EntityFrameworkCore;
using PaschoalottoBackend.Modelos;

namespace PaschoalottoBackend.Dados
{
    public class BancoDeDadosContexto : DbContext
    {
        public BancoDeDadosContexto(DbContextOptions<BancoDeDadosContexto> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
using DesafioViceri.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioViceri.Repositories
{
    public class Repository: DbContext
    {
        public Repository(DbContextOptions<Repository> options): base(options)
        {

        }
        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}

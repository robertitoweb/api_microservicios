using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Interfaces
{
    public interface IAppDBContext
    {
        DbSet<Categorias> Categorias { get; set; }
        DbSet<Post> Post { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
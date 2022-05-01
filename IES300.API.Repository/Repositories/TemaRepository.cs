using IES300.API.Domain.Entities;
using IES300.API.Domain.Interfaces.Repositories;

namespace IES300.API.Repository.Repositories
{
    public class TemaRepository : RepositoryBase<Tema>, ITemaRepository
    {
        public TemaRepository(ApiDbContext context) : base(context) { }
    }
}

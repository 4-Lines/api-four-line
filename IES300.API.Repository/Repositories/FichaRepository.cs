using IES300.API.Domain.Entities;
using IES300.API.Domain.Interfaces.Repositories;

namespace IES300.API.Repository.Repositories
{
    public class FichaRepository : RepositoryBase<Ficha>, IFichaRepository
    {
        public FichaRepository(ApiDbContext context) : base(context) { }
    }
}

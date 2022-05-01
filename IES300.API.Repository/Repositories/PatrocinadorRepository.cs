using IES300.API.Domain.Entities;
using IES300.API.Domain.Interfaces.Repositories;

namespace IES300.API.Repository.Repositories
{
    public class PatrocinadorRepository : RepositoryBase<Patrocinador>, IPatrocinadorRepository
    {
        public PatrocinadorRepository(ApiDbContext context) : base(context) { }
    }
}

using IES300.API.Domain.Entities;
using IES300.API.Domain.Interfaces.Repositories;
using System.Linq;

namespace IES300.API.Repository.Repositories
{
    public class PatrocinadorRepository : RepositoryBase<Patrocinador>, IPatrocinadorRepository
    {
        public PatrocinadorRepository(ApiDbContext context) : base(context) { }

        public bool EmailExistenteDePatrocinador(string email, int id)
        {
            return _dbSet.Where(x => x.Id != id).Any(x => x.Email.ToUpper() == email.ToUpper());
        }
    }
}

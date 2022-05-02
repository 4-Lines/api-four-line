using IES300.API.Domain.Entities;

namespace IES300.API.Domain.Interfaces.Repositories
{
    public interface IPatrocinadorRepository : IRepositoryBase<Patrocinador>
    {
        bool EmailExistenteDePatrocinador(string email, int id);
    }
}

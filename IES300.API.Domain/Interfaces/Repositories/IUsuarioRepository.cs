using IES300.API.Domain.DTOs.Usuario;
using IES300.API.Domain.Entities;
using System.Collections.Generic;

namespace IES300.API.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        bool EmailExistenteDeUsuario(string email, int id);

        Usuario UsuarioExistente(string nomeUsuario, string senha);

        string ObterSenhaEncriptadaPeloId(int id);
    }
}

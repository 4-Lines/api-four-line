using IES300.API.Domain.DTOs.Usuario;
using System.Collections.Generic;

namespace IES300.API.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        UsuarioOutputDTO InserirUsuario(UsuarioInsertDTO UsuarioInput);
        List<UsuarioOutputDTO> ObterTodosUsuarios(bool ativado = true);
        UsuarioOutputDTO ObterUsuarioPorId(int id);
        void DeletarUsuario(int id);
        UsuarioOutputDTO AlterarUsuario(UsuarioUpdateDTO usuarioInput);
        void ValidarUsuario(UsuarioValidateDTO usuarioInput);
    }
}

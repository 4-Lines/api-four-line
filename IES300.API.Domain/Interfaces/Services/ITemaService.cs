using IES300.API.Domain.DTOs.Tema;
using System.Collections.Generic;

namespace IES300.API.Domain.Interfaces.Services
{
    public interface ITemaService
    {
        TemaOutputDTO InserirTema(TemaInputDTO TemaInput);
        List<TemaOutputDTO> ObterTodosTemas();
        TemaOutputDTO ObterTemaPorId(int id);
        void DeletarTema(int id);
        TemaOutputDTO AlterarTema(TemaInputDTO temaInput);

    }
}

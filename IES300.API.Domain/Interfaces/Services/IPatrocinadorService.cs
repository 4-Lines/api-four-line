using IES300.API.Domain.DTOs.Patrocinador;
using System.Collections.Generic;

namespace IES300.API.Domain.Interfaces.Services
{
    public interface IPatrocinadorService
    {
        List<PatrocinadorOutputDTO> ObterTodosPatrocinadores();
        PatrocinadorOutputDTO ObterPatrocinadorPorId(int id);
        PatrocinadorOutputDTO InserirPatrocinador(PatrocinadorInputDTO patrocinadorInput);
        PatrocinadorOutputDTO AlterarPatrocinador(PatrocinadorOutputDTO patrocinadorOutput);
        PatrocinadorInputDTO DeletarPatrocinador(int id);
    }
}

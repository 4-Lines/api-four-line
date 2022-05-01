using IES300.API.Domain.DTOs.Patrocinador;
using System.Collections.Generic;

namespace IES300.API.Domain.Interfaces.Services
{
    public interface IPatrocinadorService
    {
        PatrocinadorOutputDTO InserirPatrocinador(PatrocinadorInputDTO patrocinadorInput);
        List<PatrocinadorOutputDTO> ObterTodosPatrocinadores();
        PatrocinadorOutputDTO ObterPatrocinadorPorId(int id);
    }
}

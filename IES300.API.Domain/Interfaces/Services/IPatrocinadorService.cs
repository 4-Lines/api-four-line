using IES300.API.Domain.DTOs.Patrocinador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES300.API.Domain.Interfaces.Services
{
    public interface IPatrocinadorService
    {
        PatrocinadorOutputDTO InserirPatrocinador(PatrocinadorInputDTO patrocinadorInput);
    }
}

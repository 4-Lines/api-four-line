using IES300.API.Domain.DTOs.Tema;
using IES300.API.Domain.Entities;
using IES300.API.Domain.Interfaces.Repositories;
using IES300.API.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IES300.API.Services.Services
{
    public class TemaService : ITemaService
    {
        private readonly ITemaRepository _temaRepository;
        public TemaService(ITemaRepository temaRepository)
        {
            _temaRepository = temaRepository;
        }
        public TemaOutputDTO AlterarTema(TemaInputDTO temaInput)
        {
            throw new NotImplementedException(); //Função aqui
        }

        public void DeletarTema(TemaInputDTO TemaInput)
        {
            throw new NotImplementedException(); //Função aqui
        }

        public TemaOutputDTO InserirTema(TemaInputDTO TemaInput)
        {
            var oTema = new Tema()
            {
                Ativado = true,
                IdPatrocinador = TemaInput.IdPatrocinador,
                Nome = TemaInput.Nome,
                UrlTabuleiro = TemaInput.UrlTabuleiro                
            };

            _temaRepository.Inserir(oTema);

            return new TemaOutputDTO()
            {
                Nome = TemaInput.Nome,
                IdPatrocinador = TemaInput.IdPatrocinador,
                UrlTabuleiro = TemaInput.UrlTabuleiro
            };
        }

        public TemaOutputDTO ObterTemaPorId(int id)
        {
            throw new NotImplementedException(); //Função aqui
        }

        public List<TemaOutputDTO> ObterTodosTemas()
        {
            throw new NotImplementedException(); //Função aqui
        }
    }
}

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
            if (_temaRepository.ObterPorId(temaInput.Id) != null)
                return null;

            var oTema = new Tema()
            {
                Ativado = temaInput.Ativado,
                Id = temaInput.Id,
                Nome = temaInput.Nome,
                IdPatrocinador = temaInput.IdPatrocinador,
                UrlTabuleiro = temaInput.UrlTabuleiro
            };
            _temaRepository.Alterar(oTema);

            return new TemaOutputDTO()
            {
                Ativado = temaInput.Ativado,
                Id = temaInput.Id,
                IdPatrocinador = temaInput.IdPatrocinador,
                Nome = temaInput.Nome,
                UrlTabuleiro = temaInput.UrlTabuleiro
            };
        }

        public void DeletarTema(int id)
        {
            _temaRepository.Deletar(id);
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
            if (id < 1)
                throw new ArgumentException($"Id: {id} está inválido");

            var tema = _temaRepository.ObterPorId(id);

            if (tema == null || !tema.Ativado)
                throw new KeyNotFoundException($"Tema com Id: {id} não encontrado");

            return new TemaOutputDTO()
            {
                Id = tema.Id,
                Nome = tema.Nome,
                UrlTabuleiro = tema.UrlTabuleiro,
                IdPatrocinador = tema.IdPatrocinador,
                Ativado = tema.Ativado
            };
        }

        public List<TemaOutputDTO> ObterTodosTemas()
        {
            var listaTemas = _temaRepository.ObterTodos();

            return listaTemas.Select(x =>
            {
                return new TemaOutputDTO()
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Ativado = x.Ativado,
                    IdPatrocinador = x.IdPatrocinador,
                    UrlTabuleiro = x.UrlTabuleiro
                };
            }).ToList();
        }
    }
}

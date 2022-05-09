using IES300.API.Domain.DTOs.Tema;
using IES300.API.Domain.Entities;
using IES300.API.Domain.Interfaces.Repositories;
using IES300.API.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IES300.API.Services.Services
{
    public class TemaService : ITemaService
    {
        private readonly ITemaRepository _temaRepository;

        public TemaService(ITemaRepository temaRepository)
        {
            _temaRepository = temaRepository;
        }

        public TemaOutputDTO AlterarTema(TemaUpdateDTO temaUpdate)
        {
            var temaOutput = this.ObterTemaPorId(temaUpdate.Id);

            var tema = new Tema()
            {
                Id = temaUpdate.Id,
                Nome = temaUpdate.Nome,
                IdPatrocinador = temaUpdate.IdPatrocinador,
                UrlTabuleiro = temaUpdate.UrlTabuleiro,
                Ativado = temaOutput.Ativado
            };

            _temaRepository.Alterar(tema);

            return new TemaOutputDTO()
            {
                Ativado = tema.Ativado,
                Id = tema.Id,
                IdPatrocinador = tema.IdPatrocinador,
                Nome = tema.Nome,
                UrlTabuleiro = tema.UrlTabuleiro
            };
        }

        public void DeletarTema(int id)
        {
            if (id < 1)
                throw new ArgumentException($"Id: {id} está inválido");

            var retorno = _temaRepository.Deletar(id);

            if (!retorno)
                throw new KeyNotFoundException($"Tema com Id: {id} não encontrado");
        }

        public TemaOutputDTO InserirTema(TemaInsertDTO TemaInput)
        {
            var tema = new Tema()
            {
                Ativado = true,
                IdPatrocinador = TemaInput.IdPatrocinador,
                Nome = TemaInput.Nome,
                UrlTabuleiro = TemaInput.UrlTabuleiro          
            };

            _temaRepository.Inserir(tema);

            if (tema.Id == 0)
                throw new NullReferenceException("Falha ao inserir Patrocinador");

            return new TemaOutputDTO()
            {
                Nome = tema.Nome,
                IdPatrocinador = tema.IdPatrocinador,
                UrlTabuleiro = tema.UrlTabuleiro,
                Ativado = tema.Ativado,
                Id = tema.Id
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

        public List<TemaOutputDTO> ObterTodosTemas(bool ativado)
        {
            var listaTemas = _temaRepository.ObterTodosTemasComPatrocinador().Where(x => x.Ativado == ativado);

            return listaTemas.Select(x =>
            {
                return new TemaOutputDTO()
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Ativado = x.Ativado,
                    UrlTabuleiro = x.UrlTabuleiro,
                    IdPatrocinador = x.Patrocinador.Id,
                    NomePatrocinador = x.Patrocinador.Nome,
                };
            }).ToList();
        }
    }
}

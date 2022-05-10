using IES300.API.Domain.DTOs.Ficha;
using IES300.API.Domain.Entities;
using IES300.API.Domain.Interfaces.Repositories;
using IES300.API.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IES300.API.Services.Services
{
    public class FichaService : IFichaService
    {
        private readonly IFichaRepository _fichaRepository;

        public FichaService(IFichaRepository fichaRepository)
        {
            _fichaRepository = fichaRepository;
        }

        public FichaOutputDTO InserirFicha(FichaInsertDTO fichaInsert)
        {
            var ficha = new Ficha()
            {
                Nome = fichaInsert.Nome,
                UrlFicha = fichaInsert.UrlFicha,
                IdTema = fichaInsert.IdTema,
                Ativado = true
            };

            _fichaRepository.Inserir(ficha);

            if (ficha.Id == 0)
                throw new NullReferenceException("Falha ao inserir Ficha");

            return new FichaOutputDTO()
            {
                Id = ficha.Id,
                Nome = ficha.Nome,
                IdTema = ficha.IdTema,
                UrlFicha = ficha.UrlFicha,
                Ativado = ficha.Ativado
            };
        }

        public List<FichaOutputDTO> ObterTodosFichas(bool ativado)
        {
            var listaFichas = _fichaRepository.ObterTodasFichasComTema().Where(x => x.Ativado == ativado);

            return listaFichas.Select(x =>
            {
                return new FichaOutputDTO()
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    UrlFicha = x.UrlFicha,
                    Ativado = x.Ativado,
                    IdTema = x.Tema.Id,
                    NomeTema = x.Tema.Nome
                };
            }).ToList();
        }

        public FichaOutputDTO ObterFichaPorId(int id)
        {
            if (id < 1)
                throw new ArgumentException($"Id: {id} está inválido");

            var ficha = _fichaRepository.ObterPorId(id);

            if (ficha == null || !ficha.Ativado)
                throw new KeyNotFoundException($"Ficha com Id: {id} não encontrada");

            return new FichaOutputDTO()
            {
                Id = ficha.Id,
                Nome = ficha.Nome,
                UrlFicha = ficha.UrlFicha,
                IdTema = ficha.IdTema,
                Ativado = ficha.Ativado
            };
        }

        public FichaOutputDTO AlterarFicha(FichaUpdateDTO fichaUpdate)
        {
            var fichaOutput = this.ObterFichaPorId(fichaUpdate.Id);

            var ficha = new Ficha()
            {
                Id = fichaUpdate.Id,
                Nome = fichaUpdate.Nome,
                UrlFicha = fichaUpdate.UrlFicha,
                IdTema = fichaUpdate.IdTema,
                Ativado = fichaOutput.Ativado
            };

            _fichaRepository.Alterar(ficha);

            return new FichaOutputDTO()
            {
                Id = ficha.Id,
                Nome = ficha.Nome,
                UrlFicha = ficha.UrlFicha,
                IdTema = ficha.IdTema,
                Ativado = ficha.Ativado
            };
        }

        public void DeletarFicha(int id)
        {
            if (id < 1)
                throw new ArgumentException($"Id: {id} está inválido");

            var ficha = this.ObterFichaPorId(id);
            var countFichasPorTema = _fichaRepository.ObterFichasPorIdTema(ficha.IdTema).Count();
            if (countFichasPorTema < 3)
                throw new InvalidOperationException($"Impossível deletar ficha, seu Tema tem menos de 3 Peças");

            var retorno = _fichaRepository.Deletar(id);

            if (!retorno)
                throw new KeyNotFoundException($"Ficha com Id: {id} não encontrada");
        }

    }
}
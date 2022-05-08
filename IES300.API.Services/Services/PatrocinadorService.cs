using IES300.API.Domain.DTOs.Patrocinador;
using IES300.API.Domain.Entities;
using IES300.API.Domain.Interfaces.Repositories;
using IES300.API.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IES300.API.Services.Services
{
    public class PatrocinadorService : IPatrocinadorService
    {
        private readonly IPatrocinadorRepository _patrocinadorRepository;

        public PatrocinadorService(IPatrocinadorRepository patrocinadorRepository)
        {
            _patrocinadorRepository = patrocinadorRepository;
        }

        public PatrocinadorOutputDTO InserirPatrocinador(PatrocinadorInsertDTO patrocinadorInsert)
        {
            this.ExisteEmailIgual(patrocinadorInsert.Email);

            var patrocinador = new Patrocinador()
            {
                Nome = patrocinadorInsert.Nome,
                Email = patrocinadorInsert.Email,
                Celular = patrocinadorInsert.Celular,
                UrlLogo = patrocinadorInsert.UrlLogo,
                Website = patrocinadorInsert.Website,
                Ativado = true
            };

            _patrocinadorRepository.Inserir(patrocinador);

            if (patrocinador.Id == 0)
                throw new NullReferenceException("Falha ao inserir Patrocinador");

            return new PatrocinadorOutputDTO()
            {
                Id = patrocinador.Id,
                Nome = patrocinador.Nome,
                Email = patrocinador.Email,
                UrlLogo = patrocinador.UrlLogo,
                Website = patrocinador.Website,
                Celular = patrocinador.Celular,
                Ativado = patrocinador.Ativado
            };
        }

        public List<PatrocinadorOutputDTO> ObterTodosPatrocinadores(bool ativado)
        {
            var listaPatrocinadores = _patrocinadorRepository.ObterTodos().Where(x => x.Ativado == ativado);

            return listaPatrocinadores.Select(x =>
            {
                return new PatrocinadorOutputDTO()
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Email = x.Email,
                    UrlLogo = x.UrlLogo,
                    Website = x.Website,
                    Celular = x.Celular,
                    Ativado = x.Ativado
                };
            }).ToList();
        }

        public PatrocinadorOutputDTO ObterPatrocinadorPorId(int id)
        {
            if (id < 1)
                throw new ArgumentException($"Id: {id} está inválido");

            var patrocinador = _patrocinadorRepository.ObterPorId(id);

            if (patrocinador == null || !patrocinador.Ativado)
                throw new KeyNotFoundException($"Patrocinador com Id: {id} não encontrado");

            return new PatrocinadorOutputDTO()
            {
                Id = patrocinador.Id,
                Nome = patrocinador.Nome,
                Email = patrocinador.Email,
                UrlLogo = patrocinador.UrlLogo,
                Website = patrocinador.Website,
                Celular = patrocinador.Celular,
                Ativado = patrocinador.Ativado
            };
        }

        public PatrocinadorOutputDTO AlterarPatrocinador(PatrocinadorUpdatetDTO patrocinadorUpdate)
        {
            this.ObterPatrocinadorPorId(patrocinadorUpdate.Id);
            this.ExisteEmailIgual(patrocinadorUpdate.Email, patrocinadorUpdate.Id);

            var patrocinador = new Patrocinador()
            {
                Id = patrocinadorUpdate.Id,
                Nome = patrocinadorUpdate.Nome,
                Email = patrocinadorUpdate.Email,
                Celular = patrocinadorUpdate.Celular,
                UrlLogo = patrocinadorUpdate.UrlLogo,
                Website = patrocinadorUpdate.Website
            };

            _patrocinadorRepository.Alterar(patrocinador);

            return new PatrocinadorOutputDTO()
            {
                Id = patrocinador.Id,
                Nome = patrocinador.Nome,
                Email = patrocinador.Email,
                UrlLogo = patrocinador.UrlLogo,
                Website = patrocinador.Website,
                Celular = patrocinador.Celular,
                Ativado = patrocinador.Ativado
            };
        }

        public void DeletarPatrocinador(int id)
        {
            if (id < 1)
                throw new ArgumentException($"Id: {id} está inválido");
            
            var retorno = _patrocinadorRepository.Deletar(id);

            if (!retorno)
                throw new KeyNotFoundException($"Patrocinador com Id: {id} não encontrado");
        }

        private void ExisteEmailIgual(string email, int id = 0)
        {
            var retorno = _patrocinadorRepository.EmailExistenteDePatrocinador(email, id);

            if (retorno)
                throw new ArgumentException($"Email: {email} já existe");
        }
    }
}

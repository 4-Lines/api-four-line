using IES300.API.Domain.DTOs.Patrocinador;
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
    public class PatrocinadorService : IPatrocinadorService
    {
        private readonly IPatrocinadorRepository _patrocinadorRepository;

        public PatrocinadorService(IPatrocinadorRepository patrocinadorRepository)
        {
            _patrocinadorRepository = patrocinadorRepository;
        }

        public PatrocinadorOutputDTO InserirPatrocinador(PatrocinadorInputDTO patrocinadorInput)
        {
            var patrocinador = new Patrocinador()
            {
                Nome = patrocinadorInput.Nome,
                Email = patrocinadorInput.Email,
                Celular = patrocinadorInput.Celular,
                UrlLogo = patrocinadorInput.UrlLogo,
                Website = patrocinadorInput.Website,
                Ativado = true
            };

            _patrocinadorRepository.Inserir(patrocinador);

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

        public List<PatrocinadorOutputDTO> ObterTodosPatrocinadores()
        {
            var listaPatrocinadores = _patrocinadorRepository.ObterTodos();

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
            var patrocinador = _patrocinadorRepository.ObterPorId(id);

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

        public PatrocinadorOutputDTO AlterarPatrocinador(PatrocinadorOutputDTO patrocinadorOutput)
        {
            var patrocinador = _patrocinadorRepository.ObterPorId(patrocinadorOutput.Id);

            if (patrocinador == null)
                return null;

            patrocinador.Nome = patrocinadorOutput.Nome;
            patrocinador.Email = patrocinadorOutput.Email;
            patrocinador.Celular = patrocinadorOutput.Celular;
            patrocinador.UrlLogo = patrocinadorOutput.UrlLogo;
            patrocinador.Website = patrocinadorOutput.Website;

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
    }
}

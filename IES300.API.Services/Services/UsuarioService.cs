using IES300.API.Domain.DTOs.Usuario;
using IES300.API.Domain.Entities;
using IES300.API.Domain.Interfaces.Repositories;
using IES300.API.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using IES300.API.Domain.Utils;

namespace IES300.API.Services.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public UsuarioOutputDTO InserirUsuario(UsuarioInsertDTO UsuarioInput)
        {
            this.ExisteEmailIgual(UsuarioInput.Email);

            var usuario = new Usuario()
            {
                NomeUsuario = UsuarioInput.NomeUsuario,
                Email = UsuarioInput.Email,
                Senha = Criptografia.getMDIHash(UsuarioInput.Senha),
                Ativado = true,
                //NumeroPartidas = 1,
                //NumeroVitorias = 2,
                //NumeroDerrotas = 3,
                NumeroEmpates = 4
            };

            _usuarioRepository.Inserir(usuario);

            if (usuario.Id == 0)
                throw new NullReferenceException("Falha ao inserir Usuario");

            return new UsuarioOutputDTO()
            {
                Id = usuario.Id,
                NomeUsuario = usuario.NomeUsuario,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Ativado = usuario.Ativado,
                NumeroPartidas = usuario.NumeroPartidas,
                NumeroVitorias = usuario.NumeroVitorias,
                NumeroDerrotas = usuario.NumeroDerrotas,
                NumeroEmpates = usuario.NumeroEmpates
            };
        }

        public List<UsuarioOutputDTO> ObterTodosUsuarios(bool ativado)
        {
            var listaUsuarios = _usuarioRepository.ObterTodos().Where(x => x.Ativado == ativado);

            return listaUsuarios.Select(x =>
            {
                return new UsuarioOutputDTO()
                {
                    Id = x.Id,
                    NomeUsuario = x.NomeUsuario,
                    Email = x.Email,
                    Senha = x.Senha,
                    Ativado = x.Ativado,
                    NumeroPartidas = x.NumeroPartidas,
                    NumeroVitorias = x.NumeroVitorias,
                    NumeroDerrotas = x.NumeroDerrotas,
                    NumeroEmpates = x.NumeroEmpates
                };
            }).ToList();
        }

        public UsuarioOutputDTO ObterUsuarioPorId(int id)
        {
            if (id < 1)
                throw new ArgumentException($"Id: {id} está inválido");

            var usuario = _usuarioRepository.ObterPorId(id);

            if (usuario == null || !usuario.Ativado)
                throw new KeyNotFoundException($"Usuario com Id: {id} não encontrado");

            return new UsuarioOutputDTO()
            {
                Id = usuario.Id,
                NomeUsuario = usuario.NomeUsuario,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Ativado = usuario.Ativado,
                NumeroPartidas = usuario.NumeroPartidas,
                NumeroVitorias = usuario.NumeroVitorias,
                NumeroDerrotas = usuario.NumeroDerrotas,
                NumeroEmpates = usuario.NumeroEmpates
            };
        }

        public UsuarioOutputDTO AlterarUsuario(UsuarioUpdateDTO usuarioUpdate)
        {
            var usuarioOutput = this.ObterUsuarioPorId(usuarioUpdate.Id);
            this.ExisteEmailIgual(usuarioUpdate.Email, usuarioUpdate.Id);

            var usuario = new Usuario()
            {
                Id = usuarioUpdate.Id,
                NomeUsuario = usuarioUpdate.NomeUsuario,
                Email = usuarioUpdate.Email,
                Senha = Criptografia.getMDIHash(usuarioUpdate.Senha),
                Ativado = usuarioOutput.Ativado,
                NumeroPartidas = usuarioOutput.NumeroPartidas,
                NumeroVitorias = usuarioOutput.NumeroVitorias,
                NumeroDerrotas = usuarioOutput.NumeroDerrotas,
                NumeroEmpates = usuarioOutput.NumeroEmpates
            };

            _usuarioRepository.Alterar(usuario);

            return new UsuarioOutputDTO()
            {
                Id = usuario.Id,
                NomeUsuario = usuario.NomeUsuario,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Ativado = usuario.Ativado,
                NumeroPartidas = usuario.NumeroPartidas,
                NumeroVitorias = usuario.NumeroVitorias,
                NumeroDerrotas = usuario.NumeroDerrotas,
                NumeroEmpates = usuario.NumeroEmpates
            };
        }

        public void DeletarUsuario(int id)
        {
            if (id < 1)
                throw new ArgumentException($"Id: {id} está inválido");

            var retorno = _usuarioRepository.Deletar(id);
        }

        private void ExisteEmailIgual(string email, int id = 0)
        {
            var retorno = _usuarioRepository.EmailExistenteDeUsuario(email, id);

            if (retorno)
                throw new ArgumentException($"Email: {email} já existe");
        }

        public void ValidarUsuario(UsuarioValidateDTO usuarioInput)
        {
            var nomeUsuario = usuarioInput.NomeUsuario;
            var senha = Criptografia.getMDIHash(usuarioInput.Senha);

            var retorno = _usuarioRepository.UsuarioExistente(nomeUsuario, senha);

            if (retorno)
                throw new ArgumentException($"NomeUsuario e/ou Senha incorretos");
        }
    }
}

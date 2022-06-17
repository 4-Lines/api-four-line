﻿using IES300.API.Domain.Entities.Jogo;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IES300.API.Application.Hub
{
    public class Jogo : Microsoft.AspNetCore.SignalR.Hub
    {
        public static List<Jogador> _salaEspera; // vai armazenar uma lista de jogadores para jogar
        public static List<SalaPartida> _salaJogo;

        int largura = 7;
        int altura = 6;
        int tabuleiro = 42;

        public Jogo()
        {
            if (_salaEspera == null)
                _salaEspera = new List<Jogador>();
            if (_salaJogo == null)
                _salaJogo = new List<SalaPartida>();
        }

        public async void setCampos(int[] campos, int ultimoDaAltura, int player)
        {
            await Clients.All.SendAsync("setCampos", campos, ultimoDaAltura, player);
        }

        public async void ConectarSala(string connectionId, string name)
        {
            var sala = _salaJogo.Find(x => x.Jogador1.IdJogador == "" || x.Jogador2.IdJogador == "");
            if (sala != null)
            {
                if (sala.Jogador1.IdJogador == "")
                    sala.Jogador1.IdJogador = connectionId;
                else
                {
                    sala.Jogador2.IdJogador = connectionId;
                    await Clients.All.SendAsync("InicioPartida", "Jogo Iniciado");
                }
            }
            else
            {
                _salaJogo.Add(new SalaPartida
                {
                    IdSala = _salaJogo.Count,
                    Jogador1 = new Jogador { IdJogador = connectionId, NickName = name },
                    Jogador2 = new Jogador { IdJogador = "", NickName = "" }
                });
            }
        }

        public async Task DistribuiArray(int[] campos, int ultimo, int player, int x, int y, string connectId, int encerrada)
        {
            if (this.PartidaEncerrada(player, x, y, campos))
                encerrada = player;
            else
                encerrada = 0;

            var sala = _salaJogo.Find(x => x.Jogador1.IdJogador == connectId || x.Jogador2.IdJogador == connectId);
            await Clients.Client(sala.Jogador1.IdJogador).SendAsync("DistribuiArray", campos, ultimo, player, x, y, connectId, encerrada);
            await Clients.Client(sala.Jogador2.IdJogador).SendAsync("DistribuiArray", campos, ultimo, player, x, y, connectId, encerrada);
        }

        private bool PartidaEncerrada(int player, int x, int y, int[] campos)
        {
            if (this.verificaLargura(campos, player, y) ||
                this.verificaAltura(campos, player, x) ||
                this.verificaDiagonalPrimaria(campos, player, x, y) ||
                this.verificaDiagonalSecundaria(campos, player, x, y) ||
                this.VerificaEmpate(campos))
                return true;
            else
                return false;
        }

        private bool verificaLargura(int[] campos, int player, int y)
        {
            int contador = 0;
            for (int i = (y * largura); i < (y * (largura + largura)); i++)
            {
                if (campos[i] == player)
                    contador++;
                else
                    contador = 0;

                if (contador == 4)
                    return true;
            }
            return false;
        }

        private bool verificaAltura(int[] campos, int player, int x)
        {
            int contador = 0;
            for (int i = x; i < tabuleiro; i = i + largura)
            {
                if (campos[i] == player)
                    contador++;
                else
                    contador = 0;

                if (contador == 4)
                    return true;
            }
            return false;
        }

        private bool verificaDiagonalPrimaria(int[] campos, int player, int x, int y)
        {
            int diagonal = (x - y);
            int pontoInicial = 0;
            int repeticao = 0;

            if (diagonal < 0)
            {
                pontoInicial = (0 + (largura * diagonal * -1));
                repeticao = (altura + diagonal);
            }
            else if (diagonal > 0)
            {
                pontoInicial = (0 + diagonal);
                repeticao = (largura - diagonal);
            }
            else
            {
                pontoInicial = 0;
                repeticao = altura;
            }

            int contador = 0;
            for (int i = 0; i < repeticao; i++)
            {
                int indice = (pontoInicial + (largura * i) + i);
                int casaPlayer = campos[indice];
                if (casaPlayer == player)
                    contador++;
                else
                    contador = 0;

                if (contador == 4)
                    return true;
            }
            return false;
        }

        private bool verificaDiagonalSecundaria(int[] campos, int player, int x, int y)
        {
            int diagonal = (x + y - (altura - 1));
            int pontoInicial = 0;
            int repeticao = 0;

            if (diagonal < 0)
            {
                pontoInicial = (tabuleiro - largura + (largura * diagonal));
                repeticao = (altura + diagonal);
            }
            else if (diagonal > 0)
            {
                pontoInicial = (tabuleiro - largura + diagonal);
                repeticao = (largura - diagonal);
            }
            else
            {
                pontoInicial = (tabuleiro - largura);
                repeticao = (altura);
            }

            int contador = 0;

            for (int i = 0; i < repeticao; i++)
            {
                int indice = (pontoInicial - (largura * i) + i);
                int casaPlayer = campos[indice];

                if (casaPlayer == player)
                    contador++;
                else
                    contador = 0;

                if (contador == 4)
                    return true;
            }
            return false;
        }

        private bool VerificaEmpate(int[] campos)
        {
            for (int i = 0; i < campos.Length; i++)
            {
                if (campos[i] == 0)
                    return false;
            }
            return true;
        }
    }
}

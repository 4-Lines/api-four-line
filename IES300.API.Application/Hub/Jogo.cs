using IES300.API.Domain.Entities.Jogo;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IES300.API.Application.Hub
{
    public class Sala {
        public string Player1 { get; set; }
        public string Player2 { get; set; }
    }
    public class Jogo : Microsoft.AspNetCore.SignalR.Hub
    {
        public static List<Jogador> _salaEspera; // vai armazenar uma lista de jogadores para jogar
        public static List<SalaPartida> _salaJogo;

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
            if(sala != null)
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
                _salaJogo.Add(new SalaPartida {
                    IdSala = _salaJogo.Count,
                    Jogador1 = new Jogador { IdJogador = connectionId, NickName = name}, 
                    Jogador2 = new Jogador { IdJogador = "", NickName = "" } 
                });
            }
        }
        private void verificaDirecoes(string player, int x, int y)
        {
            verificaLargura(player, y);
            verificaAltura(player, x);
            verificaDiagonalPrimaria(player, x, y);
            verificaDiagonalSecundaria(player, x, y);
            verificaCamposVazios();
        }
        private void verificaLargura(string player, int y)
        {

        }
        private void verificaAltura(string player, int x)
        {

        }
        private void verificaDiagonalPrimaria(string player, int x, int y)
        {

        }
        private void verificaDiagonalSecundaria(string player, int x, int y)
        {

        }
        private void verificaCamposVazios()
        {

        }
        public async Task DistribuiArray(int[] campos, int ultimo, int player, string connectId)
        {
            var sala = _salaJogo.Find(x => x.Jogador1.IdJogador == connectId || x.Jogador2.IdJogador == connectId);
            await Clients.Client(sala.Jogador1.IdJogador).SendAsync("DistribuiArray", campos, ultimo, player, connectId);
            await Clients.Client(sala.Jogador2.IdJogador).SendAsync("DistribuiArray", campos, ultimo, player, connectId);
        }

        //public void DesistirPartida(string connectionId)
        //{
        //    // retirar jogador da classe ou do método Group e encerrar partida
        //}

        public async void Test(string text)
        {
            text = text + " passou no back ";
            await Clients.All.SendAsync("test", text);
        }
    }
}

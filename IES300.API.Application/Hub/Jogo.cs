using IES300.API.Domain.Entities.Jogo;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace IES300.API.Application.Hub
{
    public class Jogo : Microsoft.AspNetCore.SignalR.Hub
    {
        public static List<Jogador> _salaEspera; // vai armazenar uma lista de jogadores para jogar

        public Jogo()
        {
            if (_salaEspera == null)
                _salaEspera = new List<Jogador>();
        }

        public async void setCampos(int[] campos, int ultimoDaAltura, int player)
        {
            await Clients.All.SendAsync("setCampos", campos, ultimoDaAltura, player);
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

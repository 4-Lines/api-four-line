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

        public async void setCampos(int[] campos, int player, int X, int Y, string connnectId)
        {
            await Clients.All.SendAsync("setCampos");
        }

        public async void getCampos(int x, int y)
        {
            x = 6;
            y = 7;
            //for (int i = 0; i < (altura * largura); i++)
            //{
            //    campos[i] = 0;
            //}
            await Clients.All.SendAsync("getCampos", x, y);
        }

        //public void DesistirPartida(string connectionId)
        //{
        //    // retirar jogador da classe ou do método Group e encerrar partida
        //}

        //public async void Test(string text)
        //{
        //    text = text + " passou no back";
        //    await Clients.All.SendAsync("test", text);
        //}
    }
}

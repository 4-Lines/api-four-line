using IES300.API.Domain.Entities.Jogo;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task DistribuiArray(int[] campos, int ultimo, int player, string connectId)
        { 
            await Clients.All.SendAsync("DistribuiArray", campos, ultimo, player,connectId);
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

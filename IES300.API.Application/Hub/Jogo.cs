using IES300.API.Domain.Entities.Jogo;
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

        public void ProcurarPartida(string nickName, string connectionId)
        {
            // ver se usa classe ou método Group
        }

        public void MoverPeca(DadosPartida dadosPartida)
        {
            // atualizar dados da partida
        }

        public void DesistirPartida(string connectionId)
        {
            // retirar jogador da classe ou do método Group e encerrar partida
        }
    }
}

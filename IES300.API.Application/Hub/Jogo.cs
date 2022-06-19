using IES300.API.Domain.Entities.Jogo;
using IES300.API.Domain.Interfaces.Services;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IES300.API.Application.Hub
{
    public class Jogo : Microsoft.AspNetCore.SignalR.Hub
    {
        public static List<Jogador> _salaEspera; // vai armazenar uma lista de jogadores para jogar
        public static List<SalaPartida> _salaJogo;

        private readonly IPatrocinadorService _patrocinadorService;

        int largura = 7;
        int altura = 6;
        int tabuleiro = 42;

        public Jogo(IPatrocinadorService patrocinadorService)
        {
            _patrocinadorService = patrocinadorService;

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
            var salasVazias = _salaJogo.FindAll(x => x.Jogador1.IdJogador == "" && x.Jogador2.IdJogador == "");
            foreach (var salaRemove in salasVazias)
                _salaJogo.Remove(salaRemove);

            var sala = _salaJogo.Find(x => x.Jogador1.IdJogador == "" || x.Jogador2.IdJogador == "");
            if (sala != null)
            {
                if (sala.Jogador1.IdJogador == "")
                    sala.Jogador1.IdJogador = connectionId;
                else
                {
                    sala.Jogador2.IdJogador = connectionId;
                    sala.Jogador2.NickName = name;
                    await Clients.Client(sala.Jogador1.IdJogador).SendAsync("InicioPartida", "Jogo Iniciado");
                    await Clients.Client(sala.Jogador2.IdJogador).SendAsync("InicioPartida", "Jogo Iniciado");
                }
            }
            else
            {
                _salaJogo.Add(new SalaPartida
                {
                    IdSala = _salaJogo.Count,
                    Jogador1 = new Jogador { IdJogador = connectionId, NickName = name },
                    Jogador2 = new Jogador { IdJogador = "", NickName = "" },
                    DadosPatrocinador = _patrocinadorService.ObterPatrocinadorComFichaseTemaAleatorio()
                });
            }
        }

        public async void SolicitarDadosPartida(string connectionId)
        {
            var sala = _salaJogo.Find(x => x.Jogador1.IdJogador == connectionId || x.Jogador2.IdJogador == connectionId);

            if (sala != null)
            {
                if(sala.Jogador1.IdJogador == connectionId)
                    await Clients.Client(sala.Jogador1.IdJogador).SendAsync("obterDadosPartida", sala.Jogador1, sala.Jogador2, sala.DadosPatrocinador);
                else
                    await Clients.Client(sala.Jogador2.IdJogador).SendAsync("obterDadosPartida", sala.Jogador1, sala.Jogador2, sala.DadosPatrocinador);
            }
        }

        public void ConsertaConnectionId(string connectionIdNew, string connectionIdOld)
        {
            var sala = _salaJogo.Find(x => x.Jogador1.IdJogador == connectionIdOld || x.Jogador2.IdJogador == connectionIdOld);

            if (sala != null)
            {
                if(sala.Jogador1.IdJogador == connectionIdOld)
                    sala.Jogador1.IdJogador = connectionIdNew;
                else
                    sala.Jogador2.IdJogador = connectionIdNew;
            }
        }

        public async void DesconectarSala(string connectionId)
        {
            var sala = _salaJogo.Find(x => x.Jogador1.IdJogador == connectionId || x.Jogador2.IdJogador == connectionId);
            
            if (sala.Jogador1 != null && sala.Jogador2 != null) // sala com duas pessoas
            {
                if (sala.Jogador1.IdJogador == connectionId)
                    await Clients.Client(sala.Jogador2.IdJogador).SendAsync("adversarioDesistiu", "Você ganhou");
                else
                    await Clients.Client(sala.Jogador1.IdJogador).SendAsync("adversarioDesistiu", "Você ganhou");
            }

            if(sala != null)
                _salaJogo.Remove(sala); // apaga a sala para evitar bug com validar sala == null
        }

        public async Task DistribuiArray(int[] campos, int ultimo, int player, int? x, int? y, string connectId, int encerrada)
        {
            if (x != null && y != null)
                encerrada = VerificaVitoria(player, x, y, campos);
            else
                encerrada = 0;

            var sala = _salaJogo.Find(x => x.Jogador1.IdJogador == connectId || x.Jogador2.IdJogador == connectId);
            await Clients.Client(sala.Jogador1.IdJogador).SendAsync("DistribuiArray", campos, ultimo, player, connectId, encerrada);
            await Clients.Client(sala.Jogador2.IdJogador).SendAsync("DistribuiArray", campos, ultimo, player, connectId, encerrada);
        }

        private int VerificaVitoria(int player, int? x, int? y, int[] campos)
        {
            player = player == 1 ? 2 : 1;
            if (verificaLargura(campos, player, (int)y) ||
                verificaAltura(campos, player, (int)x) ||
                verificaDiagonalPrimaria(campos, player, (int)x, (int)y) ||
                verificaDiagonalSecundaria(campos, player, (int)x, (int)y))
                return player; //vencedor
            else
            {
                if (VerificaEmpate(campos))
                    return 3; //empate
                else
                    return 0; //nada
            }
        }

        private bool verificaLargura(int[] campos, int player, int y)
        {
            int contador = 0;
            for (int i = (y * largura); i < (y * largura + largura); i++)
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

        //public void TestObject(Jogador str)
        //{
        //    //var obj = JsonSerializer.Deserialize<Jogador>(str); // exemplo de receber string e serializar ela para o objeto
        //    //var json = JsonSerializer.Serialize(obj);

        //    Clients.All.SendAsync("testObject", str);
        //}
    }
}

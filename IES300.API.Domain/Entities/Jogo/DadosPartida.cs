namespace IES300.API.Domain.Entities.Jogo
{
    public class DadosPartida
    {
        public DadosPartida()
        {
            this.MapaTabuleiro = new int[6, 7] { { 0,0,0,0,0,0,0 }, 
                                                 { 0,0,0,0,0,0,0 }, 
                                                 { 0,0,0,0,0,0,0 }, 
                                                 { 0,0,0,0,0,0,0 }, 
                                                 { 0,0,0,0,0,0,0 }, 
                                                 { 0,0,0,0,0,0,0 } };
            this.VezJogador = 0;
            this.Ganhador = 0;
        }

        public int[,] MapaTabuleiro { get; set; } // começa com zero e cada jogada muda para: 1 => P1 / 2 => P2

        public int VezJogador { get; set; } // 1 => P1 / 2 => P2

        public int Ganhador { get; set; } // 0 => ninguém / 1 => P1 / 2 => P2
    }
}

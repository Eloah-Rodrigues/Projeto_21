using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_21.Models
{
    internal class Jogo
    {
        public Baralho Baralho { get; private set; }
        public Jogador Jogador { get; private set; }
        public Banca Banca { get; private set; }
        public ResultadoPartida Resultado { get; private set; }
        public bool PartidaEncerrada
        {
            get { return Resultado != ResultadoPartida.EmAndamento; }
        }
        public Jogo(string nomeJogador)
        {
            Baralho = new Baralho();
            Jogador = new Jogador(nomeJogador);
            Banca = new Banca();
            IniciarPartida();
        }
        public void IniciarPartida()
        {
            Baralho.Reiniciar();
            Jogador.LimparMao();
            Banca.LimparMao();
            Resultado = ResultadoPartida.EmAndamento;

            Jogador.ReceberCarta(Baralho.ComprarCarta());
            Jogador.ReceberCarta(Baralho.ComprarCarta());
            Banca.ReceberCarta(Baralho.ComprarCarta());
            Banca.ReceberCarta(Baralho.ComprarCarta());

            if (Jogador.CalcularPontuacao() == 21)
                Parar();
        }
        public void PedirCarta()
        {
            if (PartidaEncerrada) return;

            Jogador.ReceberCarta(Baralho.ComprarCarta());

            if (Jogador.Estourou)
                Resultado = ResultadoPartida.BancaVenceu;
            else if (Jogador.CalcularPontuacao() == 21)
                Parar();
        }
        public void Parar()
        {
            if (PartidaEncerrada) return;

            JogarBanca();
            DefinirVencedor();
        }
        private void JogarBanca()
        {
            int pontosJogador = Jogador.CalcularPontuacao();
            while (Banca.DeveComprarCarta(pontosJogador))
                Banca.ReceberCarta(Baralho.ComprarCarta());
        }
        private void DefinirVencedor()
        {
            int pJogador = Jogador.CalcularPontuacao();
            int pBanca = Banca.CalcularPontuacao();

            if (Banca.Estourou || pJogador > pBanca)
                Resultado = ResultadoPartida.JogadorVenceu;
            else
                Resultado = ResultadoPartida.BancaVenceu;
        }
        public string ObterMensagemResultado()
        {
            if (Resultado == ResultadoPartida.EmAndamento)
                return "Partida em andamento...";

            if (Jogador.Estourou)
                return "Você estourou! A banca venceu.";

            if (Banca.Estourou)
                return "A banca estourou! Você venceu!";

            if (Resultado == ResultadoPartida.JogadorVenceu)
                return "Você venceu!";

            if (Jogador.CalcularPontuacao() == Banca.CalcularPontuacao())
                return "Empate! A vitória é da banca.";

            return "A banca venceu.";
        }
    }
}

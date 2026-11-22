using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_21.Models
{
    /*
     * CLASSE Jogo
     *
     * É a classe central, que controla as regras e o andamento de uma partida.
     * A interface (Form1) não contém regra nenhuma, só chama os métodos
     * desta classe e mostra o resultado.
     *
     * ASSOCIAÇÃO: o Jogo possui um Baralho, um Jogador e uma Banca, e guarda
     * o Resultado da partida (EmAndamento, JogadorVenceu ou BancaVenceu).
     *
     * Os "private set" das propriedades fazem com que só o próprio Jogo possa
     * alterá-las (encapsulamento). Quem está fora só consegue ler.
     */
    internal class Jogo
    {
        public Baralho Baralho { get; private set; }
        public Jogador Jogador { get; private set; }
        public Banca Banca { get; private set; }
        public ResultadoPartida Resultado { get; private set; }
        /*
         * Propriedade PartidaEncerrada
         *
         * Verdadeira quando o resultado deixou de ser "EmAndamento". A interface usa
         * essa informação para bloquear os botões Pedir Carta e Parar e exibir
         * a mensagem final.
         */
        public bool PartidaEncerrada
        {
            get { return Resultado != ResultadoPartida.EmAndamento; }
        }
        /*
         * Construtor
         *
         * Cria o baralho, o jogador (com o nome recebido) e a banca, e já inicia
         * a primeira partida.
         */
        public Jogo(string nomeJogador)
        {
            Baralho = new Baralho();
            Jogador = new Jogador(nomeJogador);
            Banca = new Banca();
            IniciarPartida();
        }
        /*
         * Método IniciarPartida
         *
         * Prepara uma partida nova: reinicia e embaralha o baralho, esvazia as mãos,
         * define o resultado como "EmAndamento" e distribui 2 cartas para o jogador e
         * 2 para a banca. Se o jogador já começar com 21, chama Parar automaticamente,
         * pois não há mais o que pedir. Com duas cartas é impossível estourar, então
         * só é preciso testar o 21.
         */
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
        /*
         * Método PedirCarta
         *
         * O jogador compra uma carta do baralho. Depois disso:
         *  - Se estourou (mais de 21): a banca vence na hora, sem jogar.
         *  - Se fez exatamente 21: chama Parar, passando a vez para a banca.
         *  - Caso contrário: a partida continua.
         * Se a partida já terminou, o método não faz nada.
         */
        public void PedirCarta()
        {
            if (PartidaEncerrada) return;

            Jogador.ReceberCarta(Baralho.ComprarCarta());

            if (Jogador.Estourou)
                Resultado = ResultadoPartida.BancaVenceu;
            else if (Jogador.CalcularPontuacao() == 21)
                Parar();
        }
        /*
         * Método Parar
         *
         * Encerra a jogada do jogador e passa a vez para a banca: a banca joga
         * automaticamente (JogarBanca) e em seguida o vencedor é definido
         * (DefinirVencedor). Se a partida já terminou, não faz nada.
         */
        public void Parar()
        {
            if (PartidaEncerrada) return;

            JogarBanca();
            DefinirVencedor();
        }
        /*
         * Método JogarBanca (privado)
         *
         * Executa a vez da banca. Pega a pontuação final do jogador e, enquanto
         * a lógica de decisão da Banca (DeveComprarCarta) mandar comprar, entrega
         * uma carta nova a ela. Quando a banca decide parar, o laço termina.
         */
        private void JogarBanca()
        {
            int pontosJogador = Jogador.CalcularPontuacao();
            while (Banca.DeveComprarCarta(pontosJogador))
                Banca.ReceberCarta(Baralho.ComprarCarta());
        }
        /*
         * Método DefinirVencedor (privado)
         *
         * Compara as pontuações e define o resultado seguindo o enunciado:
         *  - Se a banca estourou: o jogador vence.
         *  - Se o jogador fez mais pontos que a banca: o jogador vence.
         *  - Em qualquer outro caso (banca com mais pontos OU empate): a banca vence.
         */
        private void DefinirVencedor()
        {
            int pJogador = Jogador.CalcularPontuacao();
            int pBanca = Banca.CalcularPontuacao();

            if (Banca.Estourou || pJogador > pBanca)
                Resultado = ResultadoPartida.JogadorVenceu;
            else
                Resultado = ResultadoPartida.BancaVenceu;
        }
        /*
         * Método ObterMensagemResultado
         *
         * Devolve o texto final mostrado na tela, escolhendo a mensagem certa:
         * partida em andamento, jogador estourou, banca estourou, jogador venceu,
         * empate (vitória da banca) ou banca venceu. A ordem das verificações
         * importa: primeiro os estouros, depois o empate.
         */
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

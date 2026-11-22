using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_21.Models
{
    /*
     * CLASSE Banca
     *
     * Representa o adversário do jogador, controlado pelo computador.
     *
     * HERANÇA: a Banca herda de Jogador (por isso "Banca : Jogador"). Ela
     * ganha de graça a mão, o cálculo de pontuação e o controle de estouro,
     * e acrescenta apenas o que é só dela: a lógica de decisão.
     *
     * Exigência do enunciado: a banca NÃO decide aleatoriamente. Ela joga
     * depois do jogador, conhecendo a pontuação dele, e compra cartas com
     * um objetivo claro.
     */
    internal class Banca : Jogador
    {
        /*
         * Construtor
         *
         * Chama o construtor da classe pai (base) passando o nome "Banca".
         */
        public Banca() : base("Banca")
        {

        }
        /*
         * Método DeveComprarCarta (lógica de decisão da banca)
         *
         * Recebe a pontuação final do jogador, que já parou e por isso não muda mais.
         * Como o empate favorece a banca, ela só precisa IGUALAR o jogador.
         * A decisão segue 3 regras, nesta ordem:
         *  1. Se a banca estourou (mais de 21): não compra.
         *  2. Se a banca igualou ou superou o jogador: não compra, pois já vence.
         *  3. Caso contrário, está perdendo: compra mais uma carta.
         *
         * Exemplo: jogador parou com 18. Banca com 12 ou 16 compra; com 18 ou 20
         * para, e com 25 estourou e o jogador vence.
         */
        public bool DeveComprarCarta(int pontuacaoJogador)
        {
            int pontuacaoBanca = CalcularPontuacao();

            if (pontuacaoBanca > 21) return false;
            if (pontuacaoBanca >= pontuacaoJogador) return false;
            return true;
        }
    }
}

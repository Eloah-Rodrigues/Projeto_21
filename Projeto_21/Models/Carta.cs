using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_21.Models
{
    /*
     * CLASSE Carta
     *
     * Representa UMA carta do baralho, formada por um Naipe e um Valor.
     *
     * Conceitos de POO aplicados:
     *  - Encapsulamento: as propriedades Naipe e Valor só têm "get" (somente
     *    leitura). Elas são definidas uma única vez, no construtor, e depois
     *    ninguém consegue alterar a carta. Uma carta não muda de naipe.
     *  - Construtor: recebe o naipe e o valor e cria a carta já completa.
     *
     * A classe sabe responder perguntas sobre si mesma: quantos pontos vale,
     * se é Ás, se é vermelha, qual o símbolo do naipe e qual o texto para exibir.
     */
    public class Carta
    {
        public Naipe Naipe { get;  }
        public Valor Valor { get;  }

        /*
         * Construtor
         *
         * Cria uma carta a partir de um naipe e de um valor. O "this" diferencia
         * a propriedade da classe do parâmetro que chegou, que têm o mesmo nome.
         * Depois de criada, a carta não pode ser modificada.
         */
        public Carta(Naipe naipe, Valor valor)
        {
            this.Naipe = naipe;
            this.Valor = valor;
        }
        /*
         * Propriedade EhAs
         *
         * Retorna verdadeiro se a carta for um Ás. O Jogador usa essa informação
         * para saber se existe um Ás na mão e decidir se ele vale 1 ou 11.
         */
        public bool EhAs
        {
            get { return Valor == Valor.As;  }
        }
        /*
         * Propriedade EhVermelha
         *
         * Retorna verdadeiro para Copas e Ouros, que são os naipes vermelhos.
         * Espadas e Paus são pretos. Foi usada para colorir as cartas desenhadas
         * por código, antes de passarmos a usar as imagens PNG.
         */
        public bool EhVermelha
        {
            get { return Naipe == Naipe.Copas || Naipe == Naipe.Ouros; }
        }
        /*
         * Método ObterPontos
         *
         * Devolve os pontos BASE da carta, seguindo o enunciado do trabalho:
         *  - J, Q e K valem 10 pontos.
         *  - Cartas de 2 a 10 valem o próprio número (o enum já guarda esse número).
         *  - O Ás devolve 1. O ajuste do Ás para 11 não é feito aqui, e sim na
         *    classe Jogador, porque depende das outras cartas da mão.
         */
        public int ObterPontos()
        {
            if ((Valor == Valor.Q) || (Valor == Valor.J) || (Valor == Valor.K))
                return 10;
            return (int)Valor;
        }
        /*
         * Método ObterSimboloNaipe
         *
         * Converte o naipe no símbolo correspondente (copas, ouros, espadas e paus)
         * usando códigos Unicode. Serve para montar o texto da carta, por exemplo
         * no método ToString.
         */
        public string ObterSimboloNaipe()
        {
            switch(Naipe)
            {
                case Naipe.Copas: return "\u2665";   // ♥
                case Naipe.Ouros: return "\u2666";   // ♦
                case Naipe.Espadas: return "\u2660"; // ♠
                case Naipe.Paus: return "\u2663";    // ♣
                default: return "?";
            }
        }
        /*
         * Método ObterRotuloValor
         *
         * Devolve o texto curto do valor da carta: "A" para o Ás, "J", "Q" e "K"
         * para as figuras e o número (2 a 10) para as demais. É o texto que
         * aparece escrito na carta.
         */
        public string ObterRotuloValor()
        {
            switch (Valor)
            {
                case Valor.As: return "A";
                case Valor.J: return "J";
                case Valor.Q: return "Q";
                case Valor.K: return "K";
                default: return ((int)Valor).ToString();
            }
        }
        /*
         * Método ToString (sobrescrito)
         *
         * Define como a carta aparece quando convertida em texto, juntando o rótulo
         * e o símbolo do naipe. Exemplo: o Ás de Espadas vira "A" seguido do
         * símbolo de espadas. Ajuda a depurar e a exibir a carta como texto.
         */
        public override string ToString()
        {
            return ObterRotuloValor() + ObterSimboloNaipe();
        }
    }
}

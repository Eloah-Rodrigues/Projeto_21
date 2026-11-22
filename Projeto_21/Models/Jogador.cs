using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_21.Models
{
    /*
     * CLASSE Jogador
     *
     * Representa quem joga: guarda o nome e a mão (as cartas recebidas) e sabe
     * calcular a própria pontuação.
     *
     * Conceitos de POO aplicados:
     *  - Encapsulamento: a lista de cartas (_mao) é privada. Quem está fora
     *    só enxerga a mão como lista somente leitura, e só consegue alterá-la
     *    pelos métodos ReceberCarta e LimparMao.
     *  - Associação: o Jogador possui várias Cartas.
     *  - Herança: a classe Banca herda do Jogador e reaproveita tudo isso.
     */
    internal class Jogador
    {
        private readonly List<Carta> _mao;
        public string Nome { get; }
        /*
         * Construtor
         *
         * Recebe o nome do jogador e cria a mão vazia.
         */
        public Jogador(string Nome)
        {
            this.Nome = Nome;
            _mao = new List<Carta>();
        }
        /*
         * Propriedade Mao
         *
         * Expõe as cartas do jogador SOMENTE para leitura. A interface consegue
         * ver e desenhar as cartas, mas não consegue adicionar nem remover.
         */
        public IReadOnlyList<Carta> Mao
        {
            get { return _mao.AsReadOnly(); }
        }
        /*
         * Método ReceberCarta
         *
         * Adiciona uma carta à mão do jogador. É a única forma de colocar
         * cartas na mão.
         */
        public void ReceberCarta(Carta carta)
        {
            _mao.Add(carta);
        }
        /*
         * Método LimparMao
         *
         * Remove todas as cartas da mão. Usado ao iniciar uma nova partida.
         */
        public void LimparMao()
        {
            _mao.Clear();
        }
        /*
         * Método CalcularPontuacao
         *
         * Soma os pontos da mão aplicando a regra do Ás do enunciado:
         *  1. Soma todas as cartas, com cada Ás contando 1.
         *  2. Se existir pelo menos um Ás e somar mais 10 não ultrapassar 21,
         *     um Ás passa a valer 11.
         *  3. Caso contrário, o Ás continua valendo 1.
         *
         * Só um Ás pode valer 11, porque dois valendo 11 já dariam 22.
         * Exemplos: Ás + 9 = 20 (Ás vale 11); Ás + 9 + 5 = 15 (o Ás vale 1, pois
         * 25 estouraria); Ás + Ás = 12.
         */
        public int CalcularPontuacao()
        {
            int total = _mao.Sum(c => c.ObterPontos());
            bool temAs = _mao.Any(c => c.EhAs);

            if (temAs && total + 10 <= 21)
            {
                total += 10;
            }
            return total;
        }
        /*
         * Propriedade Estourou
         *
         * Verdadeiro quando a pontuação passa de 21. Pelas regras, quem estoura
         * perde automaticamente.
         */
        public bool Estourou
        {
            get { return CalcularPontuacao() > 21; }
        }
        /*
         * Propriedade TemBlackjack
         *
         * Verdadeiro quando a mão tem exatamente duas cartas somando 21
         * (Ás com uma carta de valor 10). É uma propriedade de apoio e não
         * é usada nas regras do jogo.
         */
        public bool TemBlackjack
        {
            get { return _mao.Count == 2 && CalcularPontuacao() == 21; }
        }
    }
}

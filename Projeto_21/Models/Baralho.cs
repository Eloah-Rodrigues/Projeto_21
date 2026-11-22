using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_21.Models
{
    /*
     * CLASSE Baralho
     *
     * Representa o baralho completo de 52 cartas (13 valores x 4 naipes).
     * Guarda as cartas numa lista privada (encapsulamento): quem usa a classe
     * não mexe na lista diretamente, só pede cartas pelos métodos públicos.
     *
     * Associação: o Baralho "possui" várias Cartas. A classe Jogo possui
     * um Baralho.
     *
     * As cartas saem em ordem aleatória, sem repetição: o baralho é criado,
     * embaralhado, e cada compra tira a primeira carta da pilha.
     */
    internal class Baralho
    {
        private readonly List<Carta> _cartas;
        private readonly Random _random;
        /*
         * Construtor
         *
         * Cria a lista de cartas e o gerador de números aleatórios (Random) e
         * chama Reiniciar, que monta e embaralha o baralho. Assim, todo baralho
         * já nasce completo e misturado.
         */
        public Baralho()
        {
            _random = new Random();
            _cartas = new List<Carta>();
            Reiniciar();
        }
        /*
         * Propriedade QuantidadeRestante
         *
         * Informa quantas cartas ainda restam no baralho. Somente leitura.
         */
        public int QuantidadeRestante
        {
            get { return _cartas.Count;  }
        }
        /*
         * Método Criar (privado)
         *
         * Limpa a lista e gera as 52 cartas, percorrendo todos os naipes e, para
         * cada um, todos os valores. É privado porque só o próprio baralho precisa
         * usá-lo, através do método Reiniciar.
         */
        private void Criar()
        {
            _cartas.Clear();

            foreach (Naipe naipe in Enum.GetValues(typeof(Naipe)))
            {
                foreach (Valor valor in Enum.GetValues(typeof(Valor)))
                {
                    _cartas.Add(new Carta(naipe, valor));
                }
            }
        }
        /*
         * Método Embaralhar
         *
         * Mistura as cartas com o algoritmo Fisher-Yates. Percorremos a lista do
         * fim para o começo e, em cada posição, trocamos a carta com outra posição
         * sorteada entre o início e a posição atual. Esse método garante que todas
         * as ordens possíveis têm a mesma chance de acontecer.
         */
        public void Embaralhar()
        {
            for (int i = _cartas.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                Carta temp = _cartas[i];
                _cartas[i] = _cartas[j];
                _cartas[j] = temp;
            }
        }
        /*
         * Método ComprarCarta
         *
         * Entrega a primeira carta da pilha e a remove do baralho. Por isso a mesma
         * carta nunca sai duas vezes na mesma partida, como num baralho real.
         * Se o baralho estiver vazio, lança uma exceção (InvalidOperationException).
         * Isso não deve acontecer no jogo, pois cada partida começa com 52 cartas,
         * mas a exceção protege contra um estado inválido em vez de esconder o erro.
         */
        public Carta ComprarCarta()
        {
            if (_cartas.Count == 0)
            {
                throw new InvalidOperationException("O baralho está vazio.");
            }
            Carta carta = _cartas[0];
            _cartas.RemoveAt(0);
            return carta;
        }
        /*
         * Método Reiniciar
         *
         * Devolve o baralho ao estado inicial: recria as 52 cartas e embaralha.
         * É chamado na criação do baralho e no início de cada partida nova.
         */
        public void Reiniciar()
        {
            Criar();
            Embaralhar();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_21.Models
{
    internal class Jogador
    {
        private readonly List<Carta> _mao;
        public string Nome { get; }
        public Jogador(string Nome)
        {
            this.Nome = Nome;
            _mao = new List<Carta>();
        }
        public IReadOnlyList<Carta> Mao
        {
            get { return _mao.AsReadOnly(); }
        }
        public void ReceberCarta(Carta carta)
        {
            _mao.Add(carta);
        }
        public void LimparMao()
        {
            _mao.Clear();
        }
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
        public bool Estourou
        {
            get { return CalcularPontuacao() > 21; }
        }
        public bool TemBlackjack
        {
            get { return _mao.Count == 2 && CalcularPontuacao() == 21; }
        }
    }
}

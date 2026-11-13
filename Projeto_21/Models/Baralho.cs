using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_21.Models
{
    internal class Baralho
    {
        private readonly List<Carta> _cartas;
        private readonly Random _random;

        public Baralho()
        {
            _random = new Random();
            _cartas = new List<Carta>();
            Reiniciar();
        }

        public int QuantidadeRestante
        {
            get { return _cartas.Count;  }
        }

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

        public void Reiniciar()
        {
            Criar();
            Embaralhar();
        }
    }
}

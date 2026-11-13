using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_21.Models
{
    public class Carta
    {
        public Naipe Naipe { get;  }
        public Valor Valor { get;  }

        public Carta(Naipe naipe, Valor valor)
        {
            this.Naipe = naipe;
            this.Valor = valor;
        }

        public bool EhAs
        {
            get { return Valor == Valor.As;  }
        }

        public bool EhVermelha
        {
            get { return Naipe == Naipe.Copas || Naipe == Naipe.Ouros; }
        }

        public int ObterPontos()
        {
            if ((Valor == Valor.Q) || (Valor == Valor.J) || (Valor == Valor.K))
                return 10;
            return (int)Valor;
        }

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

        public override string ToString()
        {
            return ObterRotuloValor() + ObterSimboloNaipe();
        }
    }
}

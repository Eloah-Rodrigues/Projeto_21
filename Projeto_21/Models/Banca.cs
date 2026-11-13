using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_21.Models
{
    internal class Banca : Jogador
    {
        public Banca() : base("Banca")
        {

        }

        public bool DeveComprarCarta(int pontuacaoJogador)
        {
            int pontuacaoBanca = CalcularPontuacao();

            if (pontuacaoBanca > 21) return false;
            if (pontuacaoBanca >= pontuacaoJogador) return false;
            return true;
        }
    }
}

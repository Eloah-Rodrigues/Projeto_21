using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_21.Models
{
    /*
     * ENUM ResultadoPartida
     *
     * Representa o estado atual da partida:
     *  - EmAndamento: o jogo ainda está acontecendo.
     *  - JogadorVenceu: o jogador ganhou (somou mais que a banca ou a banca estourou).
     *  - BancaVenceu: a banca ganhou (jogador estourou, banca somou mais ou houve empate).
     *
     * A classe Jogo guarda esse estado, e a interface usa para saber se
     * deve bloquear os botões e mostrar a mensagem final.
     */
    public enum ResultadoPartida
    {
        EmAndamento,
        JogadorVenceu,
        BancaVenceu
    }
}

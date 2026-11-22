using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_21.Models
{
    /*
     * ENUM Naipe
     *
     * Representa os 4 naipes do baralho: Copas, Ouros, Espadas e Paus.
     * Um enum é um conjunto fixo de nomes. Com ele, o compilador impede
     * valores inválidos (não existe um quinto naipe) e o código fica mais
     * legível: escrevemos Naipe.Copas em vez de um número solto.
     *
     * Onde é usado:
     *  - Baralho: percorre os 4 naipes para gerar as 52 cartas.
     *  - Carta: define a cor (vermelha ou preta) e o símbolo do naipe.
     *  - GerenciadorImagens: converte o naipe no prefixo do arquivo
     *    (Copas = heart, Ouros = diamond, Espadas = spade, Paus = club).
     */
    public enum Naipe
    {
        Copas,
        Ouros,
        Espadas,
        Paus
    }
}

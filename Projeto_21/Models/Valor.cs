using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_21.Models
{
    /*
     * ENUM Valor
     *
     * Representa as 13 faces possíveis de uma carta: Ás, 2 a 10, J, Q e K.
     * Ele guarda QUAL carta é (a identidade), e não quantos pontos ela vale.
     * Por exemplo, J, Q e K valem 10 pontos, mas são cartas diferentes e
     * possuem imagens diferentes.
     *
     * Os números dentro do enum (As = 1, Dois = 2 ... K = 13) permitem
     * converter as cartas de 2 a 10 diretamente no seu valor numérico.
     * Os pontos do Ás, J, Q e K são tratados na classe Carta.
     */
    public enum Valor
    {
        As = 1,
        Dois = 2,
        Tres = 3,
        Quatro = 4,
        Cinco = 5,
        Seis = 6,
        Sete = 7,
        Oito = 8,
        Nove = 9,
        Dez = 10,
        J = 11,
        Q = 12,
        K = 13,
    }
}

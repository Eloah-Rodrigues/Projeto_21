using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto_21.Models;

namespace Projeto_21
{
    /*
     * CLASSE GerenciadorImagens (estática)
     *
     * Responsável por carregar e entregar as imagens PNG das cartas. É uma classe
     * estática: não precisa ser instanciada, é usada direto com
     * GerenciadorImagens.ObterFrente(...).
     *
     * Funcionamento:
     *  - As imagens ficam na pasta "Cartas", ao lado do executável
     *    (bin\Debug\Cartas ou bin\Release\Cartas). O código descobre essa pasta
     *    sozinho, sem receber nenhum caminho fixo, e lê todos os arquivos *.png.
     *  - Todas as imagens são carregadas uma única vez e guardadas em um
     *    dicionário (cache), indexado pelo nome do arquivo. Isso evita ler o
     *    disco a cada vez que uma carta é desenhada.
     *  - Os arquivos seguem o padrão naipe_valor.png, como heart_1.png,
     *    club_jack.png e spade_10.png, mais o verso back-black.png.
     */
    public static class GerenciadorImagens
    {
        private const string NomeVerso = "back-black.png";

        private static readonly Dictionary<string, Image> _cache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
        /*
         * Construtor estático
         *
         * Roda automaticamente uma única vez, na primeira vez que a classe é usada.
         * Descobre a pasta onde o programa está executando e carrega as imagens
         * da subpasta "Cartas" e também de arquivos soltos ao lado do executável.
         */
        static GerenciadorImagens()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string pastaCartas = Path.Combine(baseDir, "Cartas");

            CarregarPasta(pastaCartas);
            CarregarPasta(baseDir);
        }
        /*
         * Método CarregarPasta
         *
         * Lê todos os arquivos PNG de uma pasta e os guarda no cache. Se a pasta
         * não existir, simplesmente sai. Cada imagem é copiada para um Bitmap novo,
         * o que libera o arquivo no disco, e imagens repetidas são ignoradas.
         */
        private static void CarregarPasta(string pasta)
        {
            if (!Directory.Exists(pasta))
                return;

            foreach (string arquivo in Directory.GetFiles(pasta, "*.png"))
            {
                string nome = Path.GetFileName(arquivo);
                if (_cache.ContainsKey(nome))
                    continue;

                using (var fs = File.OpenRead(arquivo))
                using (var original = Image.FromStream(fs))
                {
                    _cache[nome] = new Bitmap(original);
                }
            }
        }
        /*
         * Método ObterFrente
         *
         * Devolve a imagem da frente de uma carta. Monta o nome do arquivo a partir
         * do naipe e do valor da carta e procura no cache. Retorna null se
         * a imagem não existir.
         */
        public static Image ObterFrente(Carta carta)
        {
            return Buscar(MontarNomeArquivo(carta));
        }
        /*
         * Método ObterVerso
         *
         * Devolve a imagem do verso da carta (back-black.png), usada na carta
         * oculta da banca.
         */
        public static Image ObterVerso()
        {
            return Buscar(NomeVerso);
        }
        /*
         * Método MontarNomeArquivo
         *
         * Junta o prefixo do naipe e o sufixo do valor para formar o nome do arquivo.
         * Exemplo: Copas + Ás vira "heart_1.png"; Paus + Dama vira "club_queen.png".
         */
        private static string MontarNomeArquivo(Carta carta)
        {
            return $"{ObterNomeNaipe(carta.Naipe)}_{ObterNomeValor(carta.Valor)}.png";
        }
        /*
         * Método ObterNomeNaipe
         *
         * Traduz o naipe do jogo para o nome em inglês usado nos arquivos:
         * Copas = heart, Ouros = diamond, Espadas = spade e Paus = club.
         */
        private static string ObterNomeNaipe(Naipe naipe)
        {
            switch (naipe)
            {
                case Naipe.Copas: return "heart";
                case Naipe.Ouros: return "diamond";
                case Naipe.Espadas: return "spade";
                case Naipe.Paus: return "club";
                default: return "";
            }
        }
        /*
         * Método ObterNomeValor
         *
         * Traduz o valor da carta para o sufixo usado nos arquivos: o Ás vira "1",
         * J, Q e K viram "jack", "queen" e "king", e as cartas de 2 a 10 viram o
         * próprio número.
         */
        private static string ObterNomeValor(Valor valor)
        {
            switch (valor)
            {
                case Valor.As: return "1";
                case Valor.J: return "jack";
                case Valor.Q: return "queen";
                case Valor.K: return "king";
                default: return $"{(int)valor}"; // Dois = 2 ... Dez = 10
            }
        }
        /*
         * Método Buscar
         *
         * Procura uma imagem no cache pelo nome do arquivo. Se encontrar, devolve
         * a imagem; se não encontrar, devolve null.
         */
        private static Image Buscar(string nomeArquivo)
        {
            Image imagem;
            _cache.TryGetValue(nomeArquivo, out imagem);
            return imagem;
        }
    }
}

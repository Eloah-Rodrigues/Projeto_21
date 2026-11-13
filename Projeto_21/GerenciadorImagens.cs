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
    public static class GerenciadorImagens
    {
        private const string NomeVerso = "back-black.png";

        private static readonly Dictionary<string, Image> _cache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
        static GerenciadorImagens()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string pastaCartas = Path.Combine(baseDir, "Cartas");

            CarregarPasta(pastaCartas);
            CarregarPasta(baseDir);
        }
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
        public static Image ObterFrente(Carta carta)
        {
            return Buscar(MontarNomeArquivo(carta));
        }
        public static Image ObterVerso()
        {
            return Buscar(NomeVerso);
        }
        private static string MontarNomeArquivo(Carta carta)
        {
            return $"{ObterNomeNaipe(carta.Naipe)}_{ObterNomeValor(carta.Valor)}.png";
        }
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
        private static Image Buscar(string nomeArquivo)
        {
            Image imagem;
            _cache.TryGetValue(nomeArquivo, out imagem);
            return imagem;
        }
    }
}

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Projeto_21.Models;

namespace Projeto_21
{
    /*
     * CLASSE CartaControl
     *
     * É um controle visual personalizado que representa UMA carta na tela.
     * Herda de Control (HERANÇA), então se comporta como qualquer componente do
     * Windows Forms e pode ser colocado dentro de um painel.
     *
     * Ele guarda a Carta que representa (ASSOCIAÇÃO com a classe Carta) e é
     * responsável por duas coisas:
     *  1. DESENHAR a carta, usando a imagem PNG fornecida pelo GerenciadorImagens.
     *  2. ANIMAR a carta, com dois efeitos: entrada (a carta "abre" ao ser
     *     distribuída) e virada (a carta oculta da banca é revelada).
     *
     * As animações funcionam com um Timer que atualiza um progresso de 0 a 1
     * e redesenha a carta a cada passo, mudando a largura dela.
     */
    public class CartaControl : Control
    {
        /*
         * Enum ModoAnimacao (interno)
         *
         * Indica qual animação está em andamento: Nenhum, Entrada (carta sendo
         * distribuída) ou Virar (carta sendo revelada).
         */
        private enum ModoAnimacao { Nenhum, Entrada, Virar }

        private readonly Carta _carta;
        private bool _oculta;

        private ModoAnimacao _modo = ModoAnimacao.Nenhum;
        private float _progresso;
        private readonly System.Windows.Forms.Timer _timer;
        /*
         * Propriedade Oculta
         *
         * Informa se a carta está virada para baixo. O formulário usa para saber
         * se a carta da banca ainda precisa ser revelada.
         */
        public bool Oculta
        {
            get { return _oculta; }
        }
        /*
         * Construtor
         *
         * Recebe a carta e se ela deve começar oculta (mostrando o verso). Define o
         * tamanho (90 x 126), a margem e ativa o desenho próprio com buffer duplo,
         * que evita tremulação durante a animação. Também cria o Timer: a cada 15
         * milissegundos ele aumenta o progresso em 0,08; ao chegar em 1 a animação
         * termina, e se era uma virada a carta passa a ficar descoberta.
         */
        public CartaControl(Carta carta, bool oculta)
        {
            _carta = carta;
            _oculta = oculta;

            Size = new Size(90, 126);
            Margin = new Padding(8);

            SetStyle(ControlStyles.UserPaint |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.ResizeRedraw, true);

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 15;
            _timer.Tick += (s, e) =>
            {
                _progresso += 0.08f;
                if (_progresso >= 1f)
                {
                    _progresso = 1f;
                    _timer.Stop();
                    if (_modo == ModoAnimacao.Virar) _oculta = false;
                    _modo = ModoAnimacao.Nenhum;
                }
                Invalidate();
            };
        }

        //animações
        /*
         * Método AnimarEntrada
         *
         * Inicia a animação de distribuição: a carta aparece abrindo a partir do
         * centro, da largura mínima até a largura total.
         */
        public void AnimarEntrada()
        {
            _modo = ModoAnimacao.Entrada;
            _progresso = 0f;
            _timer.Start();
        }
        /*
         * Método Revelar
         *
         * Inicia a animação de virar a carta oculta. Se a carta já estiver
         * descoberta, não faz nada.
         */
        public void Revelar()
        {
            if (!_oculta) return;
            _modo = ModoAnimacao.Virar;
            _progresso = 0f;
            _timer.Start();
        }
        /*
         * Método CalcularAnimacao
         *
         * Decide, em cada instante da animação, quanto a carta está "aberta" na
         * horizontal (escalaX, de 0 a 1) e se deve mostrar o verso ou a frente.
         *  - Entrada: a largura cresce de forma suave, mais rápida no começo.
         *  - Virar: na primeira metade a carta se fecha mostrando o verso; na segunda
         *    metade ela se abre mostrando a frente. Isso dá a impressão de virar.
         */
        private void CalcularAnimacao(out float escalaX, out bool mostrarVerso)
        {
            escalaX = 1f;
            mostrarVerso = _oculta;

            if (_modo == ModoAnimacao.Entrada)
            {
                escalaX = 1f - (1f - _progresso) * (1f - _progresso); 
            }
            else if (_modo == ModoAnimacao.Virar)
            {
                if (_progresso < 0.5f)
                {
                    escalaX = 1f - 2f * _progresso;  //fecha e mostra o verso
                    mostrarVerso = true;
                }
                else
                {
                    escalaX = 2f * _progresso - 1f;  //abre na frente
                    mostrarVerso = false;
                }
            }
        }

        //desenho
        /*
         * Método OnPaint (sobrescrito)
         *
         * É chamado pelo Windows Forms sempre que a carta precisa ser desenhada.
         * Passos:
         *  1. Ativa a suavização de bordas e texto.
         *  2. Calcula a escala horizontal e se deve mostrar o verso ou a frente.
         *  3. Aplica a escala a partir do centro da carta, que cria o efeito de
         *     abrir e virar (o valor mínimo 0,02 evita largura zero).
         *  4. Pede ao GerenciadorImagens a imagem certa e a desenha no tamanho
         *     do controle.
         * O trecho comentado abaixo era um desenho alternativo por código, usado se a
         * imagem não existisse. Foi desativado porque agora usamos as imagens PNG.
         */
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            float escalaX;
            bool mostrarVerso;
            CalcularAnimacao(out escalaX, out mostrarVerso);

            //efeito de abrir/virar
            g.TranslateTransform(Width / 2f, 0);
            g.ScaleTransform(Math.Max(escalaX, 0.02f), 1f);
            g.TranslateTransform(-Width / 2f, 0);

            Image imagem = mostrarVerso
                ? GerenciadorImagens.ObterVerso()
                : GerenciadorImagens.ObterFrente(_carta);

            if (imagem != null)
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imagem, new Rectangle(0, 0, Width, Height));
            }
            /*
            else
            {
                // Fallback: desenha a carta por código se a imagem não for encontrada
                var area = new Rectangle(1, 1, Width - 3, Height - 3);
                using (GraphicsPath caminho = CriarRetanguloArredondado(area, 10))
                {
                    if (mostrarVerso) DesenharVerso(g, caminho, area);
                    else DesenharFrente(g, caminho);
                }
            }
            */
        }

        /* MÉTODOS DE DESENHO GEOMÉTRICO COMENTADOS
        private void DesenharVerso(Graphics g, GraphicsPath caminho, Rectangle area)
        {
            using (var fundo = new HatchBrush(HatchStyle.DiagonalCross,
                                              Color.LightSteelBlue, Color.MidnightBlue))
            using (var borda = new Pen(Color.White, 3))
            using (var contorno = new Pen(Color.Black, 1))
            {
                g.FillPath(fundo, caminho);
                g.DrawPath(contorno, caminho);

                var interno = Rectangle.Inflate(area, -6, -6);
                using (var caminhoInterno = CriarRetanguloArredondado(interno, 6))
                    g.DrawPath(borda, caminhoInterno);
            }
        }

        private void DesenharFrente(Graphics g, GraphicsPath caminho)
        {
            Color cor = _carta.EhVermelha ? Color.Crimson : Color.Black;
            string valor = _carta.ObterRotuloValor();
            string naipe = _carta.ObterSimboloNaipe();

            using (var fundo = new SolidBrush(Color.White))
            using (var borda = new Pen(Color.DimGray, 1))
            using (var pincel = new SolidBrush(cor))
            using (var fonteValor = new Font("Segoe UI", 14, FontStyle.Bold))
            using (var fonteNaipePequeno = new Font("Segoe UI Symbol", 12, FontStyle.Regular))
            using (var fonteNaipeGrande = new Font("Segoe UI Symbol", 36, FontStyle.Regular))
            using (var centro = new StringFormat())
            {
                centro.Alignment = StringAlignment.Center;
                centro.LineAlignment = StringAlignment.Center;

                g.FillPath(fundo, caminho);
                g.DrawPath(borda, caminho);

                // Naipe grande no centro
                g.DrawString(naipe, fonteNaipeGrande, pincel,
                             new RectangleF(0, 0, Width, Height), centro);

                // Canto superior esquerdo
                DesenharCanto(g, valor, naipe, fonteValor, fonteNaipePequeno, pincel);

                // Canto inferior direito (girado 180°), preservando a transformação da animação
                var estado = g.Save();
                g.TranslateTransform(Width, Height);
                g.RotateTransform(180);
                DesenharCanto(g, valor, naipe, fonteValor, fonteNaipePequeno, pincel);
                g.Restore(estado);
            }
        }

        private void DesenharCanto(Graphics g, string valor, string naipe,
                                   Font fonteValor, Font fonteNaipe, Brush pincel)
        {
            g.DrawString(valor, fonteValor, pincel, 4, 2);
            g.DrawString(naipe, fonteNaipe, pincel, 5, 22);
        }

        private static GraphicsPath CriarRetanguloArredondado(Rectangle r, int raio)
        {
            int d = raio * 2;
            var path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
        */

        protected override void Dispose(bool disposing)
        {
            if (disposing) _timer.Dispose();
            base.Dispose(disposing);
        }
    }
}
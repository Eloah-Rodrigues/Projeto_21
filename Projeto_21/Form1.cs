using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projeto_21.Models;

namespace Projeto_21
{
    /*
     * CLASSE Form1 (parte lógica da tela)
     *
     * É a janela principal do jogo. Herda de Form (HERANÇA) e é "partial": sua
     * definição é dividida entre este arquivo (a lógica) e o Form1.Designer.cs
     * (a criação dos controles visuais).
     *
     * ASSOCIAÇÃO: o formulário possui um objeto Jogo (_jogo). Ele NÃO contém
     * regras do blackjack: apenas chama os métodos do Jogo (PedirCarta, Parar)
     * e mostra o resultado na tela, com cartas animadas.
     *
     * Os métodos são "async": usam "await Task.Delay" para criar pausas entre as
     * animações sem travar a janela. Enquanto anima, o campo _animando bloqueia
     * os botões para evitar cliques no meio de uma animação.
     */
    public partial class Form1 : Form
    {
        private Jogo _jogo;
        private bool _animando;

        private const int PausaEntreCartas = 300; 
        private const int PausaVirar = 450;
        /*
         * Construtor
         *
         * Monta os controles da janela (InitializeComponent) e registra que, assim que
         * a janela for carregada (evento Load), uma partida nova deve ser iniciada.
         */
        public Form1()
        {
            InitializeComponent();
            Load += async (s, e) => await IniciarNovoJogoAsync();
        }

        //controle de partida
        /*
         * Método IniciarNovoJogoAsync
         *
         * Começa uma partida do zero: bloqueia os botões, limpa a mesa, restaura os
         * textos (pontos e mensagem), cria um novo objeto Jogo e atualiza a tela,
         * distribuindo as cartas iniciais com animação. É usado ao abrir o programa
         * e no botão Reiniciar Jogo.
         */
        private async Task IniciarNovoJogoAsync()
        {
            _animando = true;
            AtualizarBotoes();

            LimparPainel(flpJogador);
            LimparPainel(flpBanca);
            lblResultado.Text = "Sua vez! Pedir carta ou parar?";
            lblPontosJogador.Text = "Pontos: 0";
            lblPontosBanca.Text = "Pontos: ?";

            _jogo = new Jogo("Jogador");
            await AtualizarTelaAsync();
        }

        //eventos dos botões
        /*
         * Evento do botão Pedir Carta
         *
         * Pede uma carta ao Jogo e atualiza a tela. As regras (estouro, 21, etc.)
         * são resolvidas dentro da classe Jogo.
         */
        private async void btnPedirCarta_Click(object sender, EventArgs e)
        {
            _jogo.PedirCarta();
            await AtualizarTelaAsync();
        }
        /*
         * Evento do botão Parar
         *
         * Encerra a jogada do jogador. O Jogo faz a banca jogar e define o vencedor;
         * em seguida a tela é atualizada, mostrando as cartas da banca e o resultado.
         */
        private async void btnParar_Click(object sender, EventArgs e)
        {
            _jogo.Parar();
            await AtualizarTelaAsync();
        }
        /*
         * Evento do botão Reiniciar Jogo
         *
         * Reinicia completamente a partida, chamando IniciarNovoJogoAsync.
         */
        private async void btnReiniciar_Click(object sender, EventArgs e)
        {
            await IniciarNovoJogoAsync();
        }

        //interface
        /*
         * Método AtualizarTelaAsync
         *
         * Sincroniza a tela com o estado do Jogo, em etapas:
         *  1. Bloqueia os botões durante a animação.
         *  2. Distribui as cartas novas do jogador e atualiza os pontos dele.
         *  3. Se a partida acabou, vira a carta oculta da banca.
         *  4. Distribui as cartas que a banca comprou.
         *  5. Mostra os pontos da banca (aparece "?" enquanto a partida corre, para
         *     não revelar a mão dela) e a mensagem final.
         *  6. Libera os botões novamente.
         * Os testes "IsDisposed" evitam erro se a janela for fechada no meio da animação.
         */
        private async Task AtualizarTelaAsync()
        {
            _animando = true;
            AtualizarBotoes();

            bool encerrada = _jogo.PartidaEncerrada;

            //novas cartas do jogador
            await SincronizarMaoAsync(flpJogador, _jogo.Jogador, false);
            if (IsDisposed) return;
            lblPontosJogador.Text = $"Pontos: {_jogo.Jogador.CalcularPontuacao()}";

            //se a partida acabou vira carta da banca
            if (encerrada &&
                flpBanca.Controls.Count > 1 &&
                flpBanca.Controls[1] is CartaControl oculta && oculta.Oculta)
            {
                oculta.Revelar();
                await Task.Delay(PausaVirar);
                if (IsDisposed) return;
            }

            //cartas que a banca comprou
            await SincronizarMaoAsync(flpBanca, _jogo.Banca, !encerrada);
            if (IsDisposed) return;

            lblPontosBanca.Text = encerrada
                ? $"Pontos: {_jogo.Banca.CalcularPontuacao()}"
                : "Pontos: ?";

            lblResultado.Text = encerrada ? _jogo.ObterMensagemResultado() : "Sua vez! Pedir carta ou parar?";
            
            _animando = false;
            AtualizarBotoes();
        }
        /*
         * Método SincronizarMaoAsync
         *
         * Compara a mão de um jogador com as cartas que já estão no painel e cria
         * apenas as que faltam. Para cada carta nova, cria um CartaControl, o adiciona
         * ao painel e dispara a animação de entrada, esperando 300 ms antes da
         * próxima. Assim as cartas surgem uma a uma. O parâmetro ocultarSegunda
         * faz a segunda carta da banca aparecer de verso enquanto a partida corre.
         */
        //adiciona as cartas que ainda não estão na mesa
        private async Task SincronizarMaoAsync(FlowLayoutPanel painel, Jogador jogador, bool ocultarSegunda)
        {
            for (int i = painel.Controls.Count; i < jogador.Mao.Count; i++)
            {
                if (IsDisposed) return;

                var carta = new CartaControl(jogador.Mao[i], ocultarSegunda && i == 1);
                painel.Controls.Add(carta);
                carta.AnimarEntrada();

                await Task.Delay(PausaEntreCartas);
            }
        }
        /*
         * Método LimparPainel
         *
         * Remove todas as cartas de um painel. Cada carta é destruída (Dispose) antes
         * de ser retirada, liberando memória e os timers delas.
         */
        private void LimparPainel(FlowLayoutPanel painel)
        {
            painel.SuspendLayout();
            foreach (Control c in painel.Controls)
                c.Dispose();
            painel.Controls.Clear();
            painel.ResumeLayout();
        }
        /*
         * Método AtualizarBotoes
         *
         * Define quais botões ficam habilitados. Pedir Carta e Parar só funcionam
         * com o jogo ativo e sem animação em andamento. Reiniciar Jogo só fica
         * bloqueado durante a animação.
         */
        private void AtualizarBotoes()
        {
            bool jogoAtivo = _jogo != null && !_jogo.PartidaEncerrada;

            btnPedirCarta.Enabled = !_animando && jogoAtivo;
            btnParar.Enabled = !_animando && jogoAtivo;
            btnReiniciar.Enabled = !_animando;
        }
    }
}
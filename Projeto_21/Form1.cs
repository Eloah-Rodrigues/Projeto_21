using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projeto_21.Models;

namespace Projeto_21
{
    public partial class Form1 : Form
    {
        private Jogo _jogo;
        private bool _animando;

        private const int PausaEntreCartas = 300; 
        private const int PausaVirar = 450;       

        public Form1()
        {
            InitializeComponent();
            Load += async (s, e) => await IniciarNovoJogoAsync();
        }

        //controle de partida

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

        private async void btnPedirCarta_Click(object sender, EventArgs e)
        {
            _jogo.PedirCarta();
            await AtualizarTelaAsync();
        }

        private async void btnParar_Click(object sender, EventArgs e)
        {
            _jogo.Parar();
            await AtualizarTelaAsync();
        }

        private async void btnReiniciar_Click(object sender, EventArgs e)
        {
            await IniciarNovoJogoAsync();
        }

        //interface

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

        private void LimparPainel(FlowLayoutPanel painel)
        {
            painel.SuspendLayout();
            foreach (Control c in painel.Controls)
                c.Dispose();
            painel.Controls.Clear();
            painel.ResumeLayout();
        }

        private void AtualizarBotoes()
        {
            bool jogoAtivo = _jogo != null && !_jogo.PartidaEncerrada;

            btnPedirCarta.Enabled = !_animando && jogoAtivo;
            btnParar.Enabled = !_animando && jogoAtivo;
            btnReiniciar.Enabled = !_animando;
        }
    }
}
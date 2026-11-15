using System.Drawing;
using System.Windows.Forms;

namespace Projeto_21
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new Label();
            this.lblBanca = new Label();
            this.lblPontosBanca = new Label();
            this.flpBanca = new FlowLayoutPanel();
            this.lblJogador = new Label();
            this.lblPontosJogador = new Label();
            this.flpJogador = new FlowLayoutPanel();
            this.lblResultado = new Label();
            this.btnPedirCarta = new Button();
            this.btnParar = new Button();
            this.btnReiniciar = new Button();
            this.SuspendLayout();

            //lblTitulo
            this.lblTitulo.Dock = DockStyle.Top;
            this.lblTitulo.Font = new Font("Segoe UI Symbol", 22F, FontStyle.Bold);
            this.lblTitulo.ForeColor = Color.White;
            this.lblTitulo.Height = 60;
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "\u2660 JOGO DE 21 \u2665";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;

            //lblBanca
            this.lblBanca.AutoSize = true;
            this.lblBanca.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblBanca.ForeColor = Color.White;
            this.lblBanca.Location = new Point(20, 70);
            this.lblBanca.Name = "lblBanca";
            this.lblBanca.Text = "BANCA";

            //lblPontosBanca
            this.lblPontosBanca.AutoSize = true;
            this.lblPontosBanca.Font = new Font("Segoe UI", 14F);
            this.lblPontosBanca.ForeColor = Color.Gold;
            this.lblPontosBanca.Location = new Point(620, 70);
            this.lblPontosBanca.Name = "lblPontosBanca";
            this.lblPontosBanca.Text = "Pontos: ?";

            //flpBanca
            this.flpBanca.AutoScroll = true;
            this.flpBanca.BackColor = Color.ForestGreen;
            this.flpBanca.Location = new Point(20, 105);
            this.flpBanca.Name = "flpBanca";
            this.flpBanca.Size = new Size(760, 155);
            this.flpBanca.WrapContents = false;

            //lblJogador
            this.lblJogador.AutoSize = true;
            this.lblJogador.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblJogador.ForeColor = Color.White;
            this.lblJogador.Location = new Point(20, 280);
            this.lblJogador.Name = "lblJogador";
            this.lblJogador.Text = "JOGADOR";

            //lblPontosJogador
            this.lblPontosJogador.AutoSize = true;
            this.lblPontosJogador.Font = new Font("Segoe UI", 14F);
            this.lblPontosJogador.ForeColor = Color.Gold;
            this.lblPontosJogador.Location = new Point(620, 280);
            this.lblPontosJogador.Name = "lblPontosJogador";
            this.lblPontosJogador.Text = "Pontos: 0";

            //flpJogador
            this.flpJogador.AutoScroll = true;
            this.flpJogador.BackColor = Color.ForestGreen;
            this.flpJogador.Location = new Point(20, 315);
            this.flpJogador.Name = "flpJogador";
            this.flpJogador.Size = new Size(760, 155);
            this.flpJogador.WrapContents = false;

            //lblResultado
            this.lblResultado.AutoSize = false;
            this.lblResultado.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblResultado.ForeColor = Color.Yellow;
            this.lblResultado.Location = new Point(20, 490);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new Size(760, 40);
            this.lblResultado.TextAlign = ContentAlignment.MiddleCenter;

            //btnPedirCarta
            this.btnPedirCarta.BackColor = Color.White;
            this.btnPedirCarta.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnPedirCarta.Location = new Point(120, 550);
            this.btnPedirCarta.Name = "btnPedirCarta";
            this.btnPedirCarta.Size = new Size(160, 50);
            this.btnPedirCarta.Text = "Pedir Carta";
            this.btnPedirCarta.UseVisualStyleBackColor = false;
            this.btnPedirCarta.Click += new System.EventHandler(this.btnPedirCarta_Click);

            //btnParar
            this.btnParar.BackColor = Color.White;
            this.btnParar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnParar.Location = new Point(320, 550);
            this.btnParar.Name = "btnParar";
            this.btnParar.Size = new Size(160, 50);
            this.btnParar.Text = "Parar";
            this.btnParar.UseVisualStyleBackColor = false;
            this.btnParar.Click += new System.EventHandler(this.btnParar_Click);

            //btnReiniciar
            this.btnReiniciar.BackColor = Color.White;
            this.btnReiniciar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnReiniciar.Location = new Point(520, 550);
            this.btnReiniciar.Name = "btnReiniciar";
            this.btnReiniciar.Size = new Size(160, 50);
            this.btnReiniciar.Text = "Reiniciar Jogo";
            this.btnReiniciar.UseVisualStyleBackColor = false;
            this.btnReiniciar.Click += new System.EventHandler(this.btnReiniciar_Click);

            //Form1
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.DarkGreen;
            this.ClientSize = new Size(800, 620);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Jogo de 21";

            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblBanca);
            this.Controls.Add(this.lblPontosBanca);
            this.Controls.Add(this.flpBanca);
            this.Controls.Add(this.lblJogador);
            this.Controls.Add(this.lblPontosJogador);
            this.Controls.Add(this.flpJogador);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.btnPedirCarta);
            this.Controls.Add(this.btnParar);
            this.Controls.Add(this.btnReiniciar);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblTitulo;
        private Label lblBanca;
        private Label lblPontosBanca;
        private FlowLayoutPanel flpBanca;
        private Label lblJogador;
        private Label lblPontosJogador;
        private FlowLayoutPanel flpJogador;
        private Label lblResultado;
        private Button btnPedirCarta;
        private Button btnParar;
        private Button btnReiniciar;
    }
}
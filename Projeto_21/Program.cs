using System;
using System.Windows.Forms;

namespace Projeto_21
{
    /*
     * CLASSE Program
     *
     * Ponto de entrada da aplicação: é por aqui que o Windows começa a
     * executar o programa. Contém apenas o método Main.
     */
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
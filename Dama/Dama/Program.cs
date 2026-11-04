using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading; // Servirà per gestire il Thread del server
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dama
{
    internal static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

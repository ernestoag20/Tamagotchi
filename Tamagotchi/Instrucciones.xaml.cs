using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Media;
using System.IO;

namespace Tamagotchi
{
    /// <summary>
    /// Lógica de interacción para Instrucciones.xaml
    /// </summary>
    public partial class Instrucciones : Window
    {
        private Window1 instr_padre;
        SoundPlayer Player = new SoundPlayer();

        public Instrucciones(Window1 instr_padre)
        {
            InitializeComponent();
            this.instr_padre = instr_padre;

            string path = Directory.GetCurrentDirectory();
            Player.SoundLocation = path + "/Sonidos/himno_atleti.wav"; ;
            Player.Play();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Player.Stop();
            this.Close();
        }
    }
}

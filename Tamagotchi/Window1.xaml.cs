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

namespace Tamagotchi
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
   
    public partial class Window1 : Window
    
    {
        MainWindow padre;

        public Window1(MainWindow padre)
        {
           InitializeComponent();
           this.padre = padre;
            
            
        }

        private void btn_empezar_Click(object sender, RoutedEventArgs e)
        {
            if (txt_nombreTamagotchi.Text == "" )
            {
                MessageBoxResult error_nombre = MessageBox.Show("Introduce un nombre por favor");
            }
            else
            {
                
                MessageBoxResult mensaje_inicial = MessageBox.Show(
               "¡Bienvenido " + txt_nombreTamagotchi.Text + "!\n\n¡Consigue todos los puntos posibles" +
               " antes de que Indi quede hambriento, dormido o cansado!");

                padre.setNombre(txt_nombreTamagotchi.Text);
                this.Close();
                Instrucciones instr = new Instrucciones(this);
                instr.ShowDialog();
                          

            }
        }
    }
}

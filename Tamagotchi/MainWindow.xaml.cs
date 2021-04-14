using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Media;
using System.IO;

namespace Tamagotchi
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        DispatcherTimer t1;
        double dec_energia, dec_alimento, dec_diversion = 1.0;
        string nombreTamagotchi;
        int pts_tercero = 0;
        int pts_segundo = 0;
        int pts_primero = 0;
        int puntos = 0;
        int nivel = 0;
        bool repetir = true;
        bool repetir2 = true;
        bool repetir3 = true;
        bool repetir4 = true;
        bool repetir5 = true;
        string ruta = Directory.GetCurrentDirectory();
        int puntos_primero;

        public MainWindow()
        {
            InitializeComponent();
            Window1 ventanaInicial = new Window1(this);
            ventanaInicial.ShowDialog();
            t1 = new DispatcherTimer();
            t1.Interval = TimeSpan.FromMilliseconds(1000);
            t1.Tick += new EventHandler(reloj);
            t1.Start();
            pb_energia.Value = 100;
            pb_diversion.Value = 100;
            pb_alimento.Value = 100;
        }

        public void setNombre(string nombre)
        {
            this.nombreTamagotchi = nombre;
        }

        private async void reloj(object sender, EventArgs e)
        {
            niveles();
            lbl_puntos_actuales.Text = "Puntos: " + puntos;
            lbl_nivel.Text = "Nivel: " + nivel;
            this.pb_energia.Value -= dec_energia;
            this.pb_alimento.Value -= dec_alimento;
            this.pb_diversion.Value -= dec_diversion;

            if (pb_energia.Value == 0 || pb_alimento.Value == 0 || pb_diversion.Value == 0)
            {
                btn_energia.IsEnabled = false;
                btn_comer.IsEnabled = false;
                btn_jugar.IsEnabled = false;
                lbl_gameover.Visibility = Visibility.Visible;
                dec_alimento = 0;
                dec_diversion = 0;
                dec_energia = 0;
                ranking(nombreTamagotchi, puntos);
                t1.Stop();
                lbl_puntuacion.Text = "Tu puntuación han sido: " + puntos + " puntos";
                lbl_puntuacion.Visibility = Visibility.Visible;
                Storyboard morir= (Storyboard)this.Resources["morir"];
                SoundPlayer Player = new SoundPlayer();
                Player.SoundLocation = ruta + "/Sonidos/morir.wav"; ;
                Player.Play();
                morir.Begin();
                await Task.Delay(4000);
                Player.Stop();
                morir.Stop();
                eleccion();
                t1.Start();
            }

            if (puntos >= 1000 && puntos_primero < puntos)
            {
                t1.Stop();
                await Task.Delay(3000);
                dec_alimento = 0;
                dec_diversion = 0;
                dec_energia = 0;
                ranking(nombreTamagotchi, puntos);
                btn_energia.IsEnabled = false;
                btn_comer.IsEnabled = false;
                btn_jugar.IsEnabled = false;
                puntos_primero = puntos;
                t1.Stop();
                SoundPlayer Player = new SoundPlayer();
                Player.SoundLocation = ruta + "/Sonidos/victoria.wav"; 
                Player.Play();
                img_victoria.Visibility = Visibility.Visible;
                lanza.Visibility = Visibility.Hidden;
                bandera_roja.Visibility = Visibility.Hidden;
                banderin.Visibility = Visibility.Hidden;
                Storyboard victoria = (Storyboard)this.Resources["victoria"];
                victoria.Begin();
                await Task.Delay(8000);
                victoria.Stop();
                eleccion();
                Player.Stop();
                t1.Start();
            }
        }

        private void eventoDescansar(object sender, RoutedEventArgs e)
        {
            if (nivel == 0)
            {
                puntos += 10;
            }
            if (nivel == 1)
            {
                puntos += 20;
            }
            if (nivel == 2 || nivel == 3)
            {
                puntos += 45;
            }
            if (nivel == 4 || nivel == 5)
            {
                puntos += 70;
            }
            this.pb_energia.Value += 20;
            btn_jugar.IsEnabled = true;
            btn_comer.IsEnabled = true;
            btn_energia.IsEnabled = true;

            z_sueño.Visibility = Visibility.Visible;
            z_sueño2.Visibility = Visibility.Visible;
            z_sueño3.Visibility = Visibility.Visible;
            
            btn_jugar.IsEnabled = false;
            btn_comer.IsEnabled = false;
            btn_energia.IsEnabled = false;
            Storyboard dormir = (Storyboard)this.Resources["dormir"];
            dormir.Completed += new EventHandler(finDormir);
            dormir.Begin();
            SoundPlayer Player = new SoundPlayer();
            string path = Directory.GetCurrentDirectory();
            Player.SoundLocation = path + "/Sonidos/dormir.wav"; ;
            Player.Play();
        }

        private void finDormir(object sender, EventArgs e)
        {
            btn_jugar.IsEnabled = true;
            btn_comer.IsEnabled = true;
            btn_energia.IsEnabled = true;
        }

        private void finComer(object sender, EventArgs e)
        {
            btn_jugar.IsEnabled = true;
            btn_comer.IsEnabled = true;
            btn_energia.IsEnabled = true;
        }

        private void finJugar(object sender, EventArgs e)
        {
            btn_jugar.IsEnabled = true;
            btn_comer.IsEnabled = true;
            btn_energia.IsEnabled = true;
            pelota_roja.Visibility = Visibility.Hidden;
        }

        private void comer(object sender, RoutedEventArgs e)
        {
            if (nivel == 0)
            {
                puntos += 10;
            }
            if (nivel == 1)
            {
                puntos += 20;
            }
            if (lanza.Visibility == Visibility.Visible)
            {
                puntos += 20;
            }
            if (bandera_roja.Visibility == Visibility.Visible)
            {
                puntos += 80;
            }
            if (banderin.Visibility == Visibility.Visible)
            {
                puntos += 10;
            }
            if (nivel == 2 || nivel == 3)
            {
                puntos += 45;
            }
            if (nivel == 4 || nivel == 5)
            {
                puntos += 70;
            }
            this.pb_alimento.Value += 20;
            btn_jugar.IsEnabled = true;
            btn_comer.IsEnabled = true;
            btn_energia.IsEnabled = true;

            btn_jugar.IsEnabled = false;
            btn_comer.IsEnabled = false;
            btn_energia.IsEnabled = false;
            manzana_.Visibility = Visibility.Visible;
            manzana_uno.Visibility = Visibility.Visible;
            manzana_dos.Visibility = Visibility.Visible;
            manzana_final.Visibility = Visibility.Visible;
            Storyboard sb_comer = (Storyboard)this.Resources["comer"];
            sb_comer.Completed += new EventHandler(finComer);
            Storyboard manzana = (Storyboard)this.Resources["manzana"];
            manzana.Completed += new EventHandler(finComer);
            sb_comer.Begin();
            manzana.Begin();
            SoundPlayer Player = new SoundPlayer();
            Player.SoundLocation = ruta + "/Sonidos/comer.wav"; 
            Player.Play();
        } 

        private void ranking(string nombre, int puntos)
        {

            if (puntos > pts_tercero)
            {
                if (puntos > pts_segundo)
                {
                    if (puntos > pts_primero)
                    {
                        lbl_tercero.Text = lbl_segundo.Text;
                        lbl_segundo.Text = lbl_primero.Text;
                        lbl_primero.Text = nombre + " " + puntos + " pts";
                        lbl_primero.Visibility = Visibility.Visible;
                        pts_primero = puntos;
                      
                    }
                    else
                    {
                        lbl_tercero.Text = lbl_segundo.Text;
                        lbl_segundo.Text = nombre + " " + puntos + " pts";
                        lbl_segundo.Visibility = Visibility.Visible;
                        pts_segundo = puntos;

                    }
                }
                else
                {
                    lbl_tercero.Text = nombre + " " + puntos + " pts";
                    lbl_tercero.Visibility = Visibility.Visible;
                    pts_tercero = puntos;
                }
            }

        }

        private void partida_nueva()
        {
            puntos = 0;
            pb_energia.Value = 100;
            pb_diversion.Value = 100;
            pb_alimento.Value = 100;
            btn_energia.IsEnabled = true;
            btn_comer.IsEnabled = true;
            btn_jugar.IsEnabled = true;
            repetir = true;
            repetir2 = true;
            repetir3 = true;
            repetir4 = true;
            repetir5 = true;
            lbl_puntuacion.Visibility = Visibility.Hidden; ;
            lbl_gameover.Visibility = Visibility.Hidden;
            img_nivel1.Visibility = Visibility.Hidden;
            img_nivel2.Visibility = Visibility.Hidden;
            img_nivel3.Visibility = Visibility.Hidden;
            img_nivel4.Visibility = Visibility.Hidden;
            img_nivel5.Visibility = Visibility.Hidden;
            pnl_canvas.Visibility = Visibility.Visible;
            img_calderon.Visibility = Visibility.Hidden;
            img_bernabeu.Visibility = Visibility.Hidden;
            img_final_futbol.Visibility = Visibility.Hidden;
            img_bayern.Visibility = Visibility.Hidden;
            img_victoria.Visibility = Visibility.Hidden;
            img_wanda_metropolitano.Visibility = Visibility.Hidden;
        }

        private void nuevo_jugador()
        {
            Window1 ventanaInicial = new Window1(this);
            ventanaInicial.ShowDialog();
            partida_nueva();

        }
        private void eleccion()
        {
            MessageBoxResult election = MessageBox.Show(
           "¿Quieres seguir jugando?", "IPO2 Tamagotchi",
            MessageBoxButton.YesNo);
            switch (election)
            {
                case MessageBoxResult.Yes:
                    partida_nueva();
                    break;
                case MessageBoxResult.No:
                    nuevo_jugador();
                    break;
            }
        }

        private void salir(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
            "Práctica realizada por:\nErnesto Plata\n\n ¿Desea Salir?", "IPO2 Tamagotchi",
            MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void inicioArrastrarLanza(object sender, MouseButtonEventArgs e)
        {
            DataObject dataO = new DataObject(((Image)sender));
            DragDrop.DoDragDrop((Image)sender, dataO, DragDropEffects.Move);
        }
        
        private void inicioArrastrarBandera(object sender, MouseButtonEventArgs e)
        {
            DataObject dataO = new DataObject(((Image)sender));
            DragDrop.DoDragDrop((Image)sender, dataO, DragDropEffects.Move);
        }
        private void inicioArrastrarBanderin(object sender, MouseButtonEventArgs e)
        {
            DataObject dataO = new DataObject(((Image)sender));
            DragDrop.DoDragDrop((Image)sender, dataO, DragDropEffects.Move);
        }
        private void añadirObjeto(object sender, DragEventArgs e)
        {
            Image aux = (Image)e.Data.GetData(typeof(Image));
            switch (aux.Name)
            {
                case "img_lanza":
                    lanza.Visibility = Visibility.Visible;
                    break;

                case "img_banderaroja":
                    bandera_roja.Visibility = Visibility.Visible;
                    break;

                case "img_banderin":
                    banderin.Visibility = Visibility.Visible;
                    break;
            }

        }

        private void jugar(object sender, RoutedEventArgs e)
        {
            btn_jugar.IsEnabled = true;
            btn_comer.IsEnabled = true;
            btn_energia.IsEnabled = true;
            if (nivel == 0)
            {
                puntos += 10;
            }
            if(nivel == 1 )
            {
                puntos += 20;
            }
            if (lanza.Visibility == Visibility.Visible)
            {
                puntos += 50;
            }
            if (bandera_roja.Visibility == Visibility.Visible)
            {
                puntos += 40;
            }
            if (banderin.Visibility == Visibility.Visible)
            {
                puntos += 10;
            }

            if(nivel == 2 || nivel == 3)
            {
                puntos += 45;
            }
            if(nivel == 4 || nivel == 5)
            {
                puntos += 70;
            }

            pelota_roja.Visibility = Visibility.Visible;
            btn_jugar.IsEnabled = false;
            btn_comer.IsEnabled = false;
            btn_energia.IsEnabled = false;
            Storyboard sb_jugar = (Storyboard)this.Resources["jugar_pelota"];
            sb_jugar.Completed += new EventHandler(finJugar);
            sb_jugar.Begin();
            this.pb_diversion.Value += 20;
            SoundPlayer Player = new SoundPlayer();
            Player.SoundLocation = ruta + "/Sonidos/jugar_.wav"; ;
            Player.Play();

        }

        private void btn_quitarobjetos_Click(object sender, RoutedEventArgs e)
        {
            banderin.Visibility = Visibility.Hidden;
            bandera_roja.Visibility = Visibility.Hidden;
            lanza.Visibility = Visibility.Hidden;
        }

        private async void animacion_nivel()
        {
            grid_logro.Visibility = Visibility.Visible;
            Storyboard logro = (Storyboard)this.Resources["LogroAtleti"];
            logro.Begin();
            SoundPlayer Player = new SoundPlayer();
            Player.SoundLocation = ruta + "/Sonidos/nivel.wav" ;
            Player.Play();

        }
        private async void niveles()
        {
            if (puntos < 50)
            {
                nivel = 0;
                dec_alimento = 2;
                dec_diversion = 2;
                dec_energia = 2;
                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("Oeste.jpg", UriKind.Relative);
                bi3.EndInit();
                img_fondo.Stretch = Stretch.Fill;
                img_fondo.Source = bi3;

            }
            if (puntos >= 50 && repetir && puntos <120)
            {
                img_calderon.Visibility = Visibility.Visible;
                lbl_logroconseguido.Text = "¡Pasas de Nivel!\n¡Has desbloqueado el BALÓN!";
                lbl_puntosextra.Text = "¡10 pts extra!";
                puntos += 10;
                repetir = false;
                animacion_nivel();

            }
            if (puntos >= 120 && repetir2 && puntos < 300)
            {
                img_bernabeu.Visibility = Visibility.Visible;
                grid_logro.Visibility = Visibility.Visible;
                lbl_logroconseguido.Text = "¡Pasas de Nivel!\n¡Has desbloqueado el SILBATO!";
                lbl_puntosextra.Text = "¡20 pts extra!";
                puntos += 20;
                repetir2 = false;
                animacion_nivel();

            }
            if (puntos >= 300 && repetir3 && puntos < 600)
            {
                img_bayern.Visibility = Visibility.Visible;
                grid_logro.Visibility = Visibility.Visible;
                lbl_logroconseguido.Text = "¡Pasas de Nivel!\n¡Has desbloqueado los GUANTES!";
                lbl_puntosextra.Text = "¡30 pts extra!";
                puntos += 30;
                repetir3 = false;
                animacion_nivel();

            }
            if (puntos >= 600 && repetir4 && puntos < 800)
            {
                img_final_futbol.Visibility = Visibility.Visible;
                grid_logro.Visibility = Visibility.Visible;
                lbl_logroconseguido.Text = "¡Pasas de Nivel!\n¡Has desbloqueado las DEPORTIVAS!";
                lbl_puntosextra.Text = "¡40 pts extra!";
                puntos += 40;
                repetir4 = false;
                animacion_nivel();

            }
            if (puntos >= 800 && repetir5)
            {
                grid_logro.Visibility = Visibility.Visible;
                lbl_logroconseguido.Text = "¡Nivel final!\n¡Has desbloqueado el ESTADIO!";
                repetir5 = false;
                animacion_nivel();
            }

            if (puntos >=50 && puntos < 120)
            {
                nivel = 1;
                dec_alimento = 3;
                dec_diversion = 3;
                dec_energia = 3;
                img_nivel1.Visibility = Visibility.Visible;
                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("Calderon.jpg", UriKind.Relative);
                bi3.EndInit();
                img_fondo.Stretch = Stretch.Fill;
                img_fondo.Source = bi3;

            }
            
            if(puntos >=120 && puntos < 300)
            {
                nivel = 2;
                dec_alimento = 4;
                dec_diversion = 4;
                dec_energia = 4;
                img_nivel2.Visibility = Visibility.Visible;
                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("bernabeu.jpg", UriKind.Relative);
                bi3.EndInit();
                img_fondo.Stretch = Stretch.Fill;
                img_fondo.Source = bi3;

            }

            if (puntos >= 300 && puntos < 600)
            {
                nivel = 3;
                dec_alimento = 5;
                dec_diversion = 5;
                dec_energia = 5;
                img_nivel3.Visibility = Visibility.Visible;
                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("allianz_arena.jpg", UriKind.Relative);
                bi3.EndInit();
                img_fondo.Stretch = Stretch.Fill;
                img_fondo.Source = bi3;
            }

            if (puntos >=600 && puntos < 800 )
            {
                nivel = 4;
                dec_alimento = 5;
                dec_diversion = 5;
                dec_energia = 5;
                img_nivel4.Visibility = Visibility.Visible;
                img_final_futbol.Visibility = Visibility.Visible;
                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("final_futbol.jpg", UriKind.Relative);
                bi3.EndInit();
                img_fondo.Stretch = Stretch.Fill;
                img_fondo.Source = bi3;
            }

            if (puntos >= 600 && puntos < 800)
            {
                nivel = 5;
                dec_alimento = 6;
                dec_diversion = 6;
                dec_energia = 6;
                img_nivel5.Visibility = Visibility.Visible;
                img_wanda_metropolitano.Visibility = Visibility.Visible;
                Image myImage3 = new Image();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("nivel5.jpg", UriKind.Relative);
                bi3.EndInit();
                img_fondo.Stretch = Stretch.Fill;
                img_fondo.Source = bi3;
            }

        }

        

    }
}

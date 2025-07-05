using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Numerics;
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

namespace CarSharing.View
{
    /// <summary>
    /// Логика взаимодействия для AboutUS.xaml
    /// </summary>
    public partial class AboutUS : Window
    {
        public static SoundPlayer player = new SoundPlayer("Q:\\CarSharing\\Sound_Enter2.wav");
        public AboutUS()
        {
            InitializeComponent();
            ThemeChange();
        }

        private void Favourites_Mouse_Enter(object sender, MouseEventArgs e)
        {
            player.Play();
            Favourites.BorderThickness = new Thickness(3, 0, 0, 0);
            Favourites.BorderBrush = new SolidColorBrush(Colors.Red);
            Favourites.Background = new SolidColorBrush(Colors.Blue);

        }
        private void Favourites_Mouse_Leave(object sender, MouseEventArgs e)
        {
            Favourites.BorderThickness = new Thickness(0, 0, 0, 0);
            Favourites.Background = new SolidColorBrush(Colors.Transparent);
        }
        private void Profile_Mouse_Enter(object sender, MouseEventArgs e)
        {
            player.Play();
            Profile1.BorderThickness = new Thickness(3, 0, 0, 0);
            Profile1.BorderBrush = new SolidColorBrush(Colors.Red);
            Profile1.Background = new SolidColorBrush(Colors.Blue);
        }
        private void Profile_Mouse_Leave(object sender, MouseEventArgs e)
        {
            Profile1.BorderThickness = new Thickness(0, 0, 0, 0);
            Profile1.Background = new SolidColorBrush(Colors.Transparent);
        }
        private void Settings_Mouse_Enter(object sender, MouseEventArgs e)
        {
            player.Play();
            Settings.BorderThickness = new Thickness(3, 0, 0, 0);
            Settings.BorderBrush = new SolidColorBrush(Colors.Red);
            Settings.Background = new SolidColorBrush(Colors.Blue);
        }
        private void Settings_Mouse_Leave(object sender, MouseEventArgs e)
        {
            Settings.BorderThickness = new Thickness(0, 0, 0, 0);
            Settings.Background = new SolidColorBrush(Colors.Transparent);
        }
        private void AutoPark_Mouse_Enter(object sender, MouseEventArgs e)
        {
            player.Play();
            Auto_Park.BorderThickness = new Thickness(3, 0, 0, 0);
            Auto_Park.BorderBrush = new SolidColorBrush(Colors.Red);
            Auto_Park.Background = new SolidColorBrush(Colors.Blue);
        }
        private void AutoPark_Mouse_Leave(object sender, MouseEventArgs e)
        {
            Auto_Park.BorderThickness = new Thickness(0, 0, 0, 0);
            Auto_Park.Background = new SolidColorBrush(Colors.Transparent);
        }
        private void About_Mouse_Enter(object sender, MouseEventArgs e)
        {
            player.Play();
            About.BorderThickness = new Thickness(3, 0, 0, 0);
            About.BorderBrush = new SolidColorBrush(Colors.Red);
            About.Background = new SolidColorBrush(Colors.Blue);
        }
        private void About_Mouse_Leave(object sender, MouseEventArgs e)
        {
            About.BorderThickness = new Thickness(0, 0, 0, 0);
            About.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void Auto_ParkBatton_Click(object sender, RoutedEventArgs e)
        {
            Shop_window window = new Shop_window();
            window.Show();
            this.Close();
        }

        private void Favoruties_Click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new FavouritesWindow());
            //FavouritesWindow window = new FavouritesWindow();
            //window.Show();
            //this.Close();
        }

        private void Profile_click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new Profile());
            //Profile window = new Profile();
            //window.Show();
            //this.Close();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new OrdersWindow());
        //    OrdersWindow window = new OrdersWindow();   
        //    window.Show();
        //    this.Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new AboutUS());
            //AboutUS aboutUS = new AboutUS();
            //aboutUS.Show();
            //this.Close();
        }

        private void ThemeChange()
        {
            if (GlobalDataForXAML.Theme == "Day")
            {
                GridHeader.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF18147B"));

            }
            else
            {
                GridHeader.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF100E3D"));
            }

        }

        private void AutoPark_Open(object sender, RoutedEventArgs e)
        {
            Shop_window window = new Shop_window();
            window.Show();
            this.Close();
        }

        private void Undo_click(object sender, MouseButtonEventArgs e)
        {
            Navigation.Undo();
        }

        private void Rendo_click(object sender, MouseButtonEventArgs e)
        {
            Navigation.Redo();
        }
    }
}

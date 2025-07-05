using CarSharing.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;
using CarSharing.ViewModel;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Globalization;

namespace CarSharing.View
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        public static SoundPlayer player = new SoundPlayer("Q:\\CarSharing\\Sound_Enter2.wav");
        public Profile()
        {
            InitializeComponent();
            ThemeChange();
            ProfileUserName.Text = GlobalData.Login;
            LoginDate.Text = $" : {GlobalData.DateTime.ToShortDateString()}";
            AboutPassword.Password = GlobalData.Password;
            AboutEmail.Text = GlobalData.Email;
            Console.WriteLine(GlobalData.ImagePath);

            try
            {
                ImageProfile.Source = new BitmapImage(new Uri(GlobalData.ImagePath, UriKind.Absolute));
            } catch (Exception ex) { }
            try
            {
                RefreshBackground();
            }
            catch (Exception ex) { }
            try
            {
                AvatarUpMenu.ImageSource = new BitmapImage(new Uri(GlobalData.ImagePath, UriKind.Absolute));
            }
            catch (Exception ex) { }
            DataContext = new Main1();

            Order_main OrdersHistory = new Order_main();
           
            ItemControlsOrdersHistory.DataContext = OrdersHistory.OrdersHistoryById(GlobalData.Id); ;
            
            //todo ipdate IdOrders
            ItemControlsOrdersHistory.ItemsSource = OrdersHistory.OrdersHistoryById(GlobalData.Id);
            

        }

        private void RefreshBackground()
        {
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(GlobalData.ImagePath, UriKind.Absolute));
            ImageProfileBackground.Background = brush;

        }
        private void Favourites_Mouse_Enter(object sender, MouseEventArgs e)
        {
            if(GlobalDataForXAML.IsSound) player.Play();
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
            if (GlobalDataForXAML.IsSound) player.Play();
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
            if (GlobalDataForXAML.IsSound) player.Play();
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
            if (GlobalDataForXAML.IsSound) player.Play();
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
            if (GlobalDataForXAML.IsSound) player.Play();
            About.BorderThickness = new Thickness(3, 0, 0, 0);
            About.BorderBrush = new SolidColorBrush(Colors.Red);
            About.Background = new SolidColorBrush(Colors.Blue);
        }
        private void About_Mouse_Leave(object sender, MouseEventArgs e)
        {
            About.BorderThickness = new Thickness(0, 0, 0, 0);
            About.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void Profile_click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new Profile());
           
        }

        private void EnglishButton_Click(object sender, RoutedEventArgs e)
        {
            LocalizationManager.SetLanguage("en-US");
        }

        private void RussianButton_Click(object sender, RoutedEventArgs e)
        {
            LocalizationManager.SetLanguage("ru-RU");
        }

        private void Change_Lenguage(object sender, MouseButtonEventArgs e)
        {
            if (CountyPng.Tag?.ToString() == "US")
            {
                LocalizationManager.SetLanguage("ru-RU");
                CountyPng.Tag = "RU";
                CountyPng.Source = new BitmapImage(new Uri("Q:/CarSharing/Russia.png", UriKind.RelativeOrAbsolute));

            }
            else if (CountyPng.Tag?.ToString() == "RU")
            {
                LocalizationManager.SetLanguage("en-US");
                CountyPng.Tag = "US";
                CountyPng.Source = new BitmapImage(new Uri("Q:/CarSharing/USA.png", UriKind.RelativeOrAbsolute));

            }
        }

        private void ThemePng_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

      

        private void Sound_Change(object sender, MouseButtonEventArgs e)
        {
            if (SoundPng.Tag?.ToString() == "ON")
            {
                SoundPng.Tag = "OFF";
                GlobalDataForXAML.IsSound = false;
                SoundPng.Source = new BitmapImage(new Uri("Q:/CarSharing/mute.png", UriKind.RelativeOrAbsolute));

            }
            else if (SoundPng.Tag?.ToString() == "OFF")
            {
                SoundPng.Tag = "ON";
                GlobalDataForXAML.IsSound = true;
                SoundPng.Source = new BitmapImage(new Uri("Q:/CarSharing/sound.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void ButtonAboutUs(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new AboutUS());
            //AboutUS window = new AboutUS();
            //this.Close();
            //window.Show();
        }

        private void SelectImage_Click(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.png;*.bmp;*.gif";
            string imagePath = openFileDialog.FileName;
            if (openFileDialog.ShowDialog() == true)
            {
                ImageProfile.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));



                ImageBrush brush = new ImageBrush();// photoProfile Background
                brush.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                ImageProfileBackground.Background = brush;

                // Avatar in menu
                AvatarUpMenu.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));

                GlobalData.ImagePath = openFileDialog.FileName;

                SaveImagePath(openFileDialog.FileName);// photoProfile
            }
        }

        public static void SaveImagePath(string imagePath)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarSharing;Trusted_Connection=True";
            string query = "UPDATE Users SET ImagePath = @ImagePath WHERE Login = @Login";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Login", GlobalData.Login);
                        command.Parameters.AddWithValue("@ImagePath", imagePath);

                        int rowsAffected = command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }


        private void Auto_ParkBatton_Click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new Shop_window());
            //Shop_window window = new Shop_window();
            //window.Show();
            //this.Close();

        }

        private void FavouritesWindow_Click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new FavouritesWindow());
            //FavouritesWindow window = new FavouritesWindow();
            //window.Show();
            //this.Close();
        }

        private void ProfileChangeSave_Click(object sender, RoutedEventArgs e)
        {


            if (ValidatePassword(AboutPassword.Password) && ValidateEmail(AboutEmail.Text))
            {
                SaveUserData(AboutEmail.Text, AboutPassword.Password.ToString());
                ForEmail.SendEmail(GlobalData.Email,"Уведомление", "Ваш пароль успешно изменен!");
            }
            else if (!ValidatePassword(AboutPassword.Password))
            {
                MessBox.ShowCustomMessageBox("ForPassword");
            }
                
        }

        bool ValidatePassword(string password)
        {
            return password.Length >= 8 &&
                   Regex.IsMatch(password, @"[A-Z]") &&
                   Regex.IsMatch(password, @"[a-z]") &&
                   Regex.IsMatch(password, @"\d");
        }
        static bool ValidateEmail(string email)
        {
            string emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailRegex);
        }

        public static void SaveUserData(string Email, string password)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarSharing;Trusted_Connection=True";
            string query = "UPDATE Users SET Email = @Email, Password = @password WHERE Login = @Login";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@Login", GlobalData.Login);

                        int rowsAffected = command.ExecuteNonQuery();
                        MessBox.ShowCustomMessageBox(rowsAffected > 0 ? "ProfileChanged" : "ErrorSave");

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Favoruties_Click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new FavouritesWindow());
            //FavouritesWindow window = new FavouritesWindow();
            //window.Show();
            //this.Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new AboutUS());
            //AboutUS window = new AboutUS();
            //window.Show();
            //this.Close();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new OrdersWindow());
            //OrdersWindow window = new OrdersWindow();
            //window.Show();
            //this.Close();
        }

        private void Open_CarWindow(object sender, MouseButtonEventArgs e)
        {
            var button = sender as Image;
            var car = button.DataContext as Order;


            Main1 x = new Main1();

            CarWindow window = new CarWindow(x.GetProduct(car.id_car));
            window.Show();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta * 0.6); // Уменьшаем скорость
                e.Handled = true;
            }
        }

        private void ThemeChange()
        {
            if (GlobalDataForXAML.Theme == "Day")
            {
                GridHeader.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF18147B"));
                Bottom_Border_Profile.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF18147B"));
                Border_1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF18147B"));
                Border_3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF18147B"));
            }
            else
            {
                GridHeader.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF100E3D"));
                Bottom_Border_Profile.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF100E3D"));
                Border_1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF100E3D"));
                Border_3.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF100E3D"));
            }

        }

        private void Change_Theme(object sender, MouseButtonEventArgs e)
        {
            if (ThemePng.Tag?.ToString() == "Moon")
            {
                ThemePng.Tag = "Earth";
                ThemePng.Source = new BitmapImage(new Uri("Q:/CarSharing/Earth3.png", UriKind.RelativeOrAbsolute));
                GlobalDataForXAML.Theme = "Day";
                ThemeChange();
            }
            else if (ThemePng.Tag?.ToString() == "Earth")
            {
                ThemePng.Tag = "Moon";
                ThemePng.Source = new BitmapImage(new Uri("Q:/CarSharing/Moon3.png", UriKind.RelativeOrAbsolute));
                GlobalDataForXAML.Theme = "Night";
                ThemeChange();
            }
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

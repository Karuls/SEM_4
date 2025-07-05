using CarSharing.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Логика взаимодействия для FavouritesWindow.xaml
    /// </summary>
    public partial class FavouritesWindow : Window
    {
        public static SoundPlayer player = new SoundPlayer("Q:\\CarSharing\\Sound_Enter2.wav");
        public static SoundPlayer playerLike = new SoundPlayer("C:\\Users\\fedor\\OneDrive\\Desktop\\CarSharing\\CarSharing\\Model\\Sounds\\like1.wav");

        public FavouritesWindow()
        {
            InitializeComponent();
            ThemeChange();
            Main1 viewModel = new Main1();
            DataContext = viewModel; 
            viewModel.SortForFavoruties(GlobalData.FavorutiesCarsId);

            try
            {
                AvatarUpMenu.ImageSource = new BitmapImage(new Uri(GlobalData.ImagePath, UriKind.Absolute));
            }
            catch (Exception ex) { }
        }

        private void Search_Mouse_Enter(object sender, MouseEventArgs e)
        {
            if (Search.Text == "Search")
            {
                Search.Text = string.Empty;
            }
        }

        private async void Search_Mouse_Leave(object sender, MouseEventArgs e)
        {
            if (Search.Text != "Search")
            {
                await Task.Delay(3000);
                Search.Text = "Search";
            }
        }
        private void Favourites_Mouse_Enter(object sender, MouseEventArgs e)
        {
            if (GlobalDataForXAML.IsSound) player.Play();
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
            Profile.BorderThickness = new Thickness(3, 0, 0, 0);
            Profile.BorderBrush = new SolidColorBrush(Colors.Red);
            Profile.Background = new SolidColorBrush(Colors.Blue);
        }
        private void Profile_Mouse_Leave(object sender, MouseEventArgs e)
        {
            Profile.BorderThickness = new Thickness(0, 0, 0, 0);
            Profile.Background = new SolidColorBrush(Colors.Transparent);
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
            //Profile window = new Profile();
            //window.Show();
            //this.Close();
        }

        private void Like_auto_click(object sender, RoutedEventArgs e)
        {


            if (sender is Button button)
            {
                if (GlobalDataForXAML.IsSound) playerLike.Play();
                var selectedCar = button.DataContext as Product;
                var heartImage = (Image)button.Content;

                string currentImage = heartImage.Source?.ToString() ?? "";

                if (currentImage.Contains("heart.png"))
                {
                    heartImage.Source = new BitmapImage(new Uri("/Model/Images/liked.png", UriKind.Relative));

                    if (GlobalData.FavorutiesCarsId == null)
                    {
                        GlobalData.FavorutiesCarsId = new List<int>();
                    }
                    //GlobalData.FavorutiesCarsId.Add(selectedCar.Id);
                    UpdateFavoritesInDatabase(selectedCar.Id);

                }
                else
                {
                    heartImage.Source = new BitmapImage(new Uri("/Model/Images/heart.png", UriKind.Relative));
                    if (GlobalData.FavorutiesCarsId.Contains(selectedCar.Id))
                    {
                        GlobalData.FavorutiesCarsId.Remove(selectedCar.Id);
                        ForDeleteFavoritesIdInDatabase();
                        Main1 viewModel = new Main1();
                        DataContext = viewModel;
                        viewModel.SortForFavoruties(GlobalData.FavorutiesCarsId);

                    }

                }
            }
        }

        private void DESC_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as Main1)?.SortProductsByPrice(false);
        }

        private void ABS_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as Main1)?.SortProductsByPrice(true);
        }



        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Search.Text) && !Search.Text.Contains("Search"))
            {
                var viewModel = DataContext as Main1;
                if (viewModel != null)
                {
                    viewModel.SortForFavoruties(GlobalData.FavorutiesCarsId);
                    viewModel.SortProductsByCarName(Search.Text, false);
                }

            }
            else {  
                Main1 viewModel = new Main1();
                DataContext = viewModel;
                viewModel.SortForFavoruties(GlobalData.FavorutiesCarsId);
            }
            
        }

        private void AboutCar_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedProduct = button.DataContext as Product;
            var detailWindow = new CarWindow(selectedProduct);
            detailWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Favourites window = new Favourites();
            //window.Show();
            //this.Close();
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

        private void AboutWindow_Click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new AboutUS());
            //AboutUS w = new AboutUS();
            //w.Show();
            //this.Close();
        }

        private void UpdateFavoritesInDatabase(int newCarId)
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarSharing;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // 1️⃣ Получаем текущий список избранных автомобилей из БД
                    string selectQuery = "SELECT FavorutiesCarsId FROM Users WHERE Id = @UserId";
                    string currentFavoritesString = "";

                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@UserId", GlobalData.Id);
                        var result = selectCommand.ExecuteScalar();
                        if (result != null)
                        {
                            currentFavoritesString = result.ToString();
                        }
                    }

                    // 2️⃣ Преобразуем строку в список ID
                    List<int> favoriteCars = DeserializeFavorites(currentFavoritesString);

                    // 3️⃣ Добавляем новый ID (если он ещё не существует в списке)
                    if (!favoriteCars.Contains(newCarId))
                    {
                        favoriteCars.Add(newCarId);
                        GlobalData.FavorutiesCarsId.Add(newCarId);
                    }

                    // 4️⃣ Сериализуем обновлённый список обратно в строку
                    string updatedFavorites = SerializeFavorites(favoriteCars);

                    // 5️⃣ Обновляем данные в БД
                    string updateQuery = @"UPDATE Users SET FavorutiesCarsId = @FavorutiesCarsId WHERE Id = @UserId";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@FavorutiesCarsId", updatedFavorites);
                        updateCommand.Parameters.AddWithValue("@UserId", GlobalData.Id);

                        updateCommand.ExecuteNonQuery();
                        // MessageBox.Show($"Автомобиль {newCarId} добавлен в избранное!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        public static string SerializeFavorites(List<int> favorites)
        {
            return favorites != null && favorites.Count > 0 ? string.Join(",", favorites) : string.Empty;
        }

        public static List<int> DeserializeFavorites(string csvString)
        {
            return !string.IsNullOrEmpty(csvString) ? csvString.Split(',').Select(int.Parse).ToList() : new List<int>();
        }


        private void ForDeleteFavoritesIdInDatabase()
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarSharing;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // 5️⃣ Обновляем данные в БД
                    string updateQuery = @"UPDATE Users SET FavorutiesCarsId = @FavorutiesCarsId WHERE Id = @UserId";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@FavorutiesCarsId", SerializeFavorites(GlobalData.FavorutiesCarsId));
                        updateCommand.Parameters.AddWithValue("@UserId", GlobalData.Id);

                        updateCommand.ExecuteNonQuery();
                        //MessageBox.Show($"Автомобиль удален избранного!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new OrdersWindow());
            //OrdersWindow window = new OrdersWindow();
            //window.Show();
            //this.Close();
        }

        private void about_click(object sender, RoutedEventArgs e)
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

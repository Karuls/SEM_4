using CarSharing.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarSharing.View
{
    /// <summary>
    /// Логика взаимодействия для Shop_window.xaml
    /// </summary>
    public partial class Shop_window : Window
    {
        public static SoundPlayer player = new SoundPlayer("Q:\\CarSharing\\Sound_Enter2.wav");
        public static SoundPlayer playerLikeDelete = new SoundPlayer("C:\\Users\\fedor\\OneDrive\\Desktop\\CarSharing\\CarSharing\\Model\\Sounds\\facebook.wav");
        private bool SortByCoast = false;
        private bool SortBySpeed = false;
        private bool SortByMotor = false;
        public Shop_window()
        {
            InitializeComponent();
            DataContext = new Main1();
            ThemeChange();
            if (GlobalData.isAdmin)
            {
                Admin.Visibility = Visibility.Visible;
                Admin.IsEnabled = true;

            }
            else { Admin.Visibility = Visibility.Hidden; Admin.IsEnabled = false; }


            try
            {
                AvatarUpMenu.ImageSource = new BitmapImage(new Uri(GlobalData.ImagePath, UriKind.Absolute));
            }
            catch (Exception ex) { }
            //Main1 viewModel = new Main1();
            //DataContext = viewModel;
            //viewModel.FavorutiesLoad();
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

        private void Admin_Mouse_Enter(object sender, MouseEventArgs e)
        {
            if (GlobalDataForXAML.IsSound) player.Play();
            Admin.BorderThickness = new Thickness(3, 0, 0, 0);
            Admin.BorderBrush = new SolidColorBrush(Colors.Red);
            Admin.Background = new SolidColorBrush(Colors.Blue);
        }
        private void Admin_Mouse_Leave(object sender, MouseEventArgs e)
        {
            Admin.BorderThickness = new Thickness(0, 0, 0, 0);
            Admin.Background = new SolidColorBrush(Colors.Transparent);
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
                
                var selectedCar = button.DataContext as Product;
                var heartImage = (Image)button.Content;

                string currentImage = heartImage.Source?.ToString() ?? "";

                if (currentImage.Contains("heart.png"))
                {
                    if (GlobalDataForXAML.IsSound) player.Play();



                    heartImage.Source = new BitmapImage(new Uri("/Model/Images/liked.png", UriKind.Relative));
                   
                    if (GlobalData.FavorutiesCarsId == null)
                    {
                        GlobalData.FavorutiesCarsId = new List<int>();
                    }
                    //GlobalData.FavorutiesCarsId.Add(selectedCar.Id);
                    UpdateFavoritesInDatabase(selectedCar.Id);
                    MessBox.ShowCustomMessageBox("AddToFavoruties");
                }
                else
                {
                    if (GlobalDataForXAML.IsSound) GlobalDataForXAML.playerLike.Play();
                    heartImage.Source = new BitmapImage(new Uri("/Model/Images/heart.png", UriKind.Relative));
                    if (GlobalData.FavorutiesCarsId.Contains(selectedCar.Id))
                    {
                        GlobalData.FavorutiesCarsId.Remove(selectedCar.Id);
                        ForDeleteFavoritesIdInDatabase();
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
                (DataContext as Main1)?.SortProductsByCarName(Search.Text, false);

            }
            else DataContext = new Main1();
        }

        private void AboutCar_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedProduct = button.DataContext as Product;
            var detailWindow = new CarWindow(selectedProduct);
            detailWindow.Show();
        }

        private void Undo_click(object sender, MouseButtonEventArgs e)
        {
            GlobalDataForXAML.Undo();
        }

        private void Rendo_click(object sender, MouseButtonEventArgs e)
        {
            GlobalDataForXAML.Redo();
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
                    SqlTransaction transaction = connection.BeginTransaction();

                    // Получаем текущий список избранных автомобилей из БД
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

                    List<int> favoriteCars = DeserializeFavorites(currentFavoritesString);

                    // ✅ Сохраняем предыдущее состояние

                    GlobalDataForXAML.undoStack.Push(new List<int>(favoriteCars));
                    GlobalDataForXAML.redoStack.Clear();

                    if (!favoriteCars.Contains(newCarId))
                    {
                        favoriteCars.Add(newCarId);
                        GlobalData.FavorutiesCarsId.Add(newCarId);  
                    }

                    string updatedFavorites = SerializeFavorites(favoriteCars);

                    // Обновляем данные в БД
                    string updateQuery = @"UPDATE Users SET FavorutiesCarsId = @FavorutiesCarsId WHERE Id = @UserId";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@FavorutiesCarsId", updatedFavorites);
                        updateCommand.Parameters.AddWithValue("@UserId", GlobalData.Id);

                        updateCommand.ExecuteNonQuery();
                       // MessageBox.Show($"Автомобиль {newCarId} добавлен в избранное!");
                    }
                    transaction.Commit();
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

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta * 0.5); // Уменьшаем скорость
                e.Handled = true;
            }
        }

        private void Admin_PanelClick(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new Admin_Panel());
            //Admin_Panel admin_Panel = new Admin_Panel();
            //admin_Panel.Show();
            //this.Close();
        }

        private void OrdersWimdow_Click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new OrdersWindow());
            //OrdersWindow ordersWindow = new OrdersWindow();
            //this.Close();
            //ordersWindow.Show();
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

        private void Sort_By_Coast(object sender, MouseButtonEventArgs e)
        {
           var cars = new Main1();
            DataContext = cars;
            if (SortByCoast)
            {
              cars.SortProductsByPrice(SortByCoast);
               SortByCoast = false;
            }
            else
            {
                cars.SortProductsByPrice(SortByCoast);
                SortByCoast = true;
            }
            
        }

        private void Sort_By_Spped(object sender, MouseButtonEventArgs e)
        {
            var cars = new Main1();
            DataContext = cars;
            if (SortByCoast)
            {
                cars.SortProductsBySpeed(SortByCoast);
                SortByCoast = false;
            }
            else
            {
                cars.SortProductsBySpeed(SortByCoast);
                SortByCoast = true;
            }
        }

        private void Sort_By_Motor(object sender, MouseButtonEventArgs e)
        {
               var cars = new Main1();
            DataContext = cars;
            if (SortByCoast)
            {
                cars.SortProductsByMotor(SortByCoast);
                SortByCoast = false;
            }
            else
            {
                cars.SortProductsByMotor(SortByCoast);
                SortByCoast = true;
            }
        }
    }
}

using CarSharing.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CarSharing.View
{
    /// <summary>
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        public static SoundPlayer player = new SoundPlayer("Q:\\CarSharing\\Sound_Enter2.wav");
        public static SoundPlayer playerLike = new SoundPlayer("C:\\Users\\fedor\\OneDrive\\Desktop\\CarSharing\\CarSharing\\Model\\Sounds\\like1.wav");

        public OrdersWindow()
        {
            InitializeComponent();
            
            Main1 viewModel = new Main1();
            DataContext = viewModel;
          
            viewModel.SortForFavoruties(GlobalData.OrdersCarsId);
            ThemeChange();
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

               
                
                    heartImage.Source = new BitmapImage(new Uri("/Model/Images/heart.png", UriKind.Relative));
                    if (GlobalData.OrdersCarsId.Contains(selectedCar.Id))
                    {
                        GlobalData.OrdersCarsId.Remove(selectedCar.Id);
                        ForDeleteFavoritesIdInDatabase();
                        Main1 viewModel = new Main1();
                        DataContext = viewModel;
                        viewModel.SortForFavoruties(GlobalData.OrdersCarsId);

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
                    viewModel.SortForFavoruties(GlobalData.OrdersCarsId);
                    viewModel.SortProductsByCarName(Search.Text, false);
                }

            }
            else
            {
                Main1 viewModel = new Main1();
                DataContext = viewModel;
                viewModel.SortForFavoruties(GlobalData.OrdersCarsId);
            }

        }

        private void AboutCar_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var selectedProduct = button.DataContext as Product;
            var detailWindow = new CarWindow(selectedProduct);
            detailWindow.Show();
        }

        private void Auto_ParkBatton_Click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new Shop_window());
            //Shop_window window = new Shop_window();
            //window.Show();
            //this.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
                    string updateQuery = @"UPDATE Users SET Orders = @Orders WHERE Login = @Login";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Orders", SerializeFavorites(GlobalData.OrdersCarsId));
                        updateCommand.Parameters.AddWithValue("@Login",GlobalData.Login);

                        updateCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        private void AddHourToRent(object sender, RoutedEventArgs e)
        {
            if (sender is not Button clickedButton) return;
            var button = sender as Button;
            var car = button.DataContext as Product;
            int price = car.Цена;
            

            // Находим TextBlock с часами
            StackPanel cardContainer = FindParent<StackPanel>(clickedButton);
            if (cardContainer == null) return;

            TextBlock hoursBlock = FindChild<TextBlock>(cardContainer, "HoursForRentBox");
            if (hoursBlock == null) return;

            if (int.TryParse(hoursBlock.Text, out int hours))
            {
                hours++;
                hoursBlock.Text = hours.ToString();

                TextBlock textHoursBlock = FindChild<TextBlock>(cardContainer, "TEXTHoursForRentBox");
                if (textHoursBlock != null)
                {
                    textHoursBlock.Text = hours == 1 ? " hour" : " hours";
                }
                UpdateRentButtonPrice(clickedButton, price);
            }
        }

        private void DeleteHourToRent(object sender, RoutedEventArgs e)
        {
            if (sender is not Button clickedButton) return;
            var button = sender as Button;
            var car = button.DataContext as Product;
            int price = car.Цена;

            StackPanel cardContainer = FindParent<StackPanel>(clickedButton);
            if (cardContainer == null) return;

            TextBlock hoursBlock = FindChild<TextBlock>(cardContainer, "HoursForRentBox");
            if (hoursBlock == null) return;

            if (int.TryParse(hoursBlock.Text, out int hours) && hours > 1)
            {
                hours--;
                hoursBlock.Text = hours.ToString();

                TextBlock textHoursBlock = FindChild<TextBlock>(cardContainer, "TEXTHoursForRentBox");
                if (textHoursBlock != null)
                {
                    textHoursBlock.Text = hours == 1 ? " hour" : " hours";
                }

                
                UpdateRentButtonPrice(clickedButton, price);
            }
        }

        private void UpdateRentButtonPrice(Button clickedButton, int price)
        {
            
            Border cardBorder = FindParent<Border>(clickedButton);
            if (cardBorder == null)
            {
                Console.WriteLine("Card Border not found!");
                return;
            }

            // 2. Находим ГЛАВНЫЙ StackPanel (который непосредственный ребенок Border)
            StackPanel mainStackPanel = FindChild<StackPanel>(cardBorder);
            if (mainStackPanel == null)
            {
                Console.WriteLine("Main StackPanel not found!");
                return;
            }

            // 3. Находим TextBlock с часами
            TextBlock hoursBlock = FindChild<TextBlock>(cardBorder, "HoursForRentBox");
            if (hoursBlock == null || !int.TryParse(hoursBlock.Text, out int hours))
            {
                Console.WriteLine("HoursForRentBox not found or invalid value!");
                return;
            }

            
            Button rentButton = FindChild<Button>(cardBorder, "RentButton");
            if (rentButton == null)
            {
                Console.WriteLine("RentButton not found by name, trying by content...");

                // Альтернативный поиск по содержимому
                var allButtons = FindVisualChildren<Button>(cardBorder);
                rentButton = allButtons.FirstOrDefault(b => b.Content?.ToString()?.Contains("Rent for") == true);

                if (rentButton == null)
                {
                    Console.WriteLine("RentButton not found at all!");
                    return;
                }
            }
          
                decimal pricePerHour = price;
                decimal totalPrice = pricePerHour * hours;
                string text = Application.Current.TryFindResource($"RentFor") as string ?? "Rent for ";
                rentButton.Content = $"{text} {totalPrice}$";
                
           
        }


        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            if (parentObject is T parent) return parent;
            return FindParent<T>(parentObject);
        }

        public static T FindChild<T>(DependencyObject parent, string childName = null) where T : DependencyObject
        {
            if (parent == null) return null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T foundChild && (string.IsNullOrEmpty(childName) || (child is FrameworkElement fe && fe.Name == childName)))
                {
                    return foundChild;
                }
                var result = FindChild<T>(child, childName);
                if (result != null) return result;
            }
            return null;
        }
        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T tChild)
                {
                    yield return tChild;
                }

                foreach (T childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }

        private void RentCar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button clickedButton) return;
              StackPanel cardContainer = FindParent<StackPanel>(clickedButton);
            if (cardContainer == null) return;
            Border cardBorder = FindParent<Border>(clickedButton);
            if (cardBorder == null)
            {
                Console.WriteLine("Card Border not found!");
                return;
            }

            // 2. Находим ГЛАВНЫЙ StackPanel
            StackPanel mainStackPanel = FindChild<StackPanel>(cardBorder);
            if (mainStackPanel == null)
            {
                Console.WriteLine("Main StackPanel not found!");
                return;
            }

            // 3. Находим TextBlock с часами
            TextBlock hoursBlock = FindChild<TextBlock>(cardBorder, "HoursForRentBox");
            if (hoursBlock == null || !int.TryParse(hoursBlock.Text, out int hours))
            {
                Console.WriteLine("HoursForRentBox not found or invalid value!");
                return;
            }


            var button = sender as Button;
            var car = button.DataContext as Product;
            int coast = car.Цена * hours;
            if (GlobalDataForXAML.IsSound) GlobalDataForXAML.playerLike.Play();
            SaveOrdersInDatabase(GlobalData.Id, car.Id, car.ImagePath, coast, hours);
            
            Main1 viewModel = new Main1();
            DataContext = viewModel;

            GlobalData.OrdersCarsId.Remove(car.Id);
            ForDeleteFavoritesIdInDatabase();

            viewModel.SortForFavoruties(GlobalData.OrdersCarsId);



        }

        private void About_Us_Click(object sender, RoutedEventArgs e)
        {
            Navigation.OpenWindow(new AboutUS());
            //AboutUS aboutUS = new AboutUS();
            //aboutUS.Show();
            //this.Close();
        }

     
        private void SaveOrdersInDatabase(int id_user, int id_car, string imagepath, int coast, int hours)
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarSharing;Integrated Security=True";
            DateTime dateTime = DateTime.Now;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    string query = @"INSERT INTO Orders 
                    (id_user, id_car, imagepath, date, coast, hours) 
                    VALUES 
                    (@id_user, @id_car, @imagepath, @date, @coast, @hours);
                    SELECT SCOPE_IDENTITY();";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@id_user", id_user);
                        command.Parameters.AddWithValue("@id_car", id_car);
                        command.Parameters.AddWithValue("@imagepath", imagepath);
                        command.Parameters.AddWithValue("@date", dateTime);
                        command.Parameters.AddWithValue("@coast", coast);
                        command.Parameters.AddWithValue("@hours", hours);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {

                            MessBox.ShowCustomMessageBox("OrderFinish");                           
                        }
                        else
                        {
                            MessageBox.Show($"Ошибка: товар не добавлен.");
                        }
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

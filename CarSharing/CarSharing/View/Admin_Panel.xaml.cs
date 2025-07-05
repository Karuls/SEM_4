using CarSharing.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace CarSharing.View
{
    /// <summary>
    /// Логика взаимодействия для Admin_Panel.xaml
    /// </summary>
    public partial class Admin_Panel : Window
    {
        public static SoundPlayer player = new SoundPlayer("Q:\\CarSharing\\Sound_Enter2.wav");
        public static SoundPlayer playerLike = new SoundPlayer("C:\\Users\\fedor\\OneDrive\\Desktop\\CarSharing\\CarSharing\\Model\\Sounds\\like1.wav");

        public Admin_Panel()
        {
            InitializeComponent();
            ThemeChange();
            GlobalData.FavorutiesCarsId = new List<int> { 1, 2, 3 };
            
            //1
            Main1 CarsContent = new Main1();
            CarsInfo.DataContext = CarsContent;
            CarsInfo.ItemsSource = CarsContent.Products;

            //2
            Feedbacks_Main feedbacksMain = new Feedbacks_Main();
            ItemControlsFeedbacks.DataContext = feedbacksMain;
            ItemControlsFeedbacks.ItemsSource = feedbacksMain.Feedbacks;

            //3 
            User_Main users = new User_Main();
            ItemControlsUsers.DataContext = users;
            ItemControlsUsers.ItemsSource = users.Users;

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




        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(Search.Text) && !Search.Text.Contains("Search"))
            //{
            //    var viewModel = DataContext as Main1;
            //    if (viewModel != null)
            //    {
            //        viewModel.SortForFavoruties(GlobalData.FavorutiesCarsId);
            //        viewModel.SortProductsByCarName(Search.Text, false);
            //    }

            //}
            //else
            //{
            //    Main1 viewModel = new Main1();
            //    DataContext = viewModel;
            //    viewModel.SortForFavoruties(GlobalData.FavorutiesCarsId);
            //}

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
            Navigation.OpenWindow(new FavouritesWindow());
            //FavouritesWindow window = new FavouritesWindow();
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

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ImageChange_Click(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == true)
            {
                if (sender is Image image)
                {
                    var car = image.DataContext as Product;

                    if (car != null)
                    {
                        car.ImagePath = openFileDialog.FileName;

                        image.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                    }
                }
            }
        }





        private void UpdateProductInDatabase(int Id, string CarName, int Цена, string ImagePath,
                                    string Допустимый_пробег, string Features,
                                    string Объем_Двигателя, int Top_speed)
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarSharing;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    string query = @"UPDATE Products 
                             SET CarName = @CarName, 
                                 Цена = @Цена, 
                                 ImagePath = @ImagePath, 
                                 Допустимый_пробег = @Допустимый_пробег,
                                 Features = @Features,
                                 Объем_Двигателя = @Объем_Двигателя,
                                 Top_speed = @Top_speed
                             WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@Id", Id);
                        command.Parameters.AddWithValue("@CarName", CarName);
                        command.Parameters.AddWithValue("@Цена", Цена);
                        command.Parameters.AddWithValue("@ImagePath", ImagePath ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Допустимый_пробег", Допустимый_пробег ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Features", Features ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Объем_Двигателя", Объем_Двигателя ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Top_speed", Top_speed);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessBox.ShowCustomMessageBox("Successfully");

                        }
                        else
                        {
                            MessBox.ShowCustomMessageBox("Error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            // Получаем кнопку, которая была нажата
            Button clickedButton = sender as Button;
            if (clickedButton == null) return;

            // Получаем родительский контейнер (Border)
            Border parentBorder = FindParent<Border>(clickedButton);
            if (parentBorder == null) return;

            // Ищем все TextBox внутри этого контейнера
            var textBoxes = FindVisualChildren<TextBox>(parentBorder);

            // Создаём переменные для хранения данных
            string carId = "";
            string carName = "";
            string carPrice = "";
            string carMileage = "";
            string carEngineVolume = "";
            string carTopSpeed = "";
            string carFeatures = "";
            string carImagePath = "";

            foreach (var textBox in textBoxes)
            {
                switch (textBox.Name)
                {
                    case "CarInfoId":
                        carId = textBox.Text;
                        if (carId != null)
                        {
                            string input = carId;

                            // Регулярное выражение: 1-3 цифры
                            Regex regex = new Regex(@"^\d{1,3}$");

                            if (!regex.IsMatch(textBox.Text))
                            {
                                MessBox.ShowCustomMessageBox("OnlyDigits");
                                return;
                            }
                        }
                        break;
                    case "CarInfoCarName":
                        carName = textBox.Text;
                        break;
                    case "CarInfoЦена":
                        carPrice = textBox.Text;
                        if (carPrice != null)
                        {
                            string input = carPrice;

                            // Регулярное выражение: 1-3 цифры
                            Regex regex = new Regex(@"^\d{1,6}$");

                            if (!regex.IsMatch(textBox.Text))
                            {
                                MessBox.ShowCustomMessageBox("OnlyDigits");
                                return;
                            }
                        }
                        break;
                    case "CarInfoДопустимый_пробег":
                        carMileage = textBox.Text;
                        break;
                    case "CarInfoОбъем_Двигателя":
                        carEngineVolume = textBox.Text;
                        break;
                    case "CarInfoTop_speed":
                        carTopSpeed = textBox.Text;
                        if (carTopSpeed != null)
                        {
                            string input = carTopSpeed;

                            // Регулярное выражение: 1-3 цифры
                            Regex regex = new Regex(@"^\d{1,4}$");

                            if (!regex.IsMatch(textBox.Text))
                            {
                                MessBox.ShowCustomMessageBox("OnlyDigits");
                                return;
                            }
                        }
                        break;
                    case "CarInfoFeatures":
                        carFeatures = textBox.Text;
                        break;
                }
            }


            var image = FindVisualChildren<Image>(parentBorder).FirstOrDefault(img => img.Name == "CarInfoImagePath");
            if (image != null)
            {
                carImagePath = image.Source.ToString();
            }


            UpdateProductInDatabase(int.Parse(carId), carName, int.Parse(carPrice), carImagePath, carMileage, carFeatures, carEngineVolume, int.Parse(carTopSpeed));
        }


        



        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null)
            {
                if (parent is T tParent)
                {
                    return tParent;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }

        // Вспомогательный метод для поиска дочерних элементов
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

        private void ImageAdd_Click(object sender, MouseButtonEventArgs e)
        {
        
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == true)
            {
               
                        AddImagePath.Source = new BitmapImage(new Uri(openFileDialog.FileName));                 
                
            }
        }

        private void AddCar_ToDataBase(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex(@"^\d{1,4}$");

            if (!regex.IsMatch(AddCoast.Text))
            {
                MessBox.ShowCustomMessageBox("OnlyDigits");
                return;
            }
            if (!regex.IsMatch(AddSpeed.Text))
            {
                MessBox.ShowCustomMessageBox("OnlyDigits");
                return;
            }
            if (!regex.IsMatch(AddПробег.Text))
            {
                MessBox.ShowCustomMessageBox("OnlyDigits");
                return;
            }
            if (IsIdExists(int.Parse(AddId.Text)))
            {
                MessBox.ShowCustomMessageBox("AlreadyHaveId");
                MessageBox.Show("Ой-ой!\nТакой ID уже есть", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            SaveProductInDatabase();


        }


        static bool IsIdExists(int idToCheck)
        {
            bool exists = false;
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarSharing;Trusted_Connection=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT COUNT(*) FROM Products WHERE Id = @id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", idToCheck);
                        exists = (int)command.ExecuteScalar() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            return exists;
        }

        private void SaveProductInDatabase()
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarSharing;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    string query = @"INSERT INTO Products 
                 (Id,CarName, Цена, ImagePath, Допустимый_пробег, Features, Объем_Двигателя, Top_speed) 
                 VALUES 
                 (@Id, @CarName, @Цена, @ImagePath, @Допустимый_пробег, @Features, @Объем_Двигателя, @Top_speed)";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@Id", int.Parse(AddId.Text));
                        command.Parameters.AddWithValue("@CarName", AddCarName.Text);
                        command.Parameters.AddWithValue("@Цена", int.Parse(AddCoast.Text));
                        command.Parameters.AddWithValue("@ImagePath", AddImagePath.Source.ToString() ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Допустимый_пробег", int.Parse(AddПробег.Text));
                        command.Parameters.AddWithValue("@Features", AddFeatures.Text ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Объем_Двигателя", AddMotor.Text ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Top_speed", int.Parse(AddSpeed.Text));

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessBox.ShowCustomMessageBox("Successfully");
                        }
                        else
                        {
                            MessBox.ShowCustomMessageBox("Error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        private void DeleteProductFromDatabase(int DeleteId)

        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarSharing;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"DELETE FROM Products WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", DeleteId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessBox.ShowCustomMessageBox("Successfully");
                        }
                        else
                        {
                            MessBox.ShowCustomMessageBox("Error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var car = button.DataContext as Product;
            DeleteProductFromDatabase(car.Id);

            Main1 CarsContent = new Main1();
            CarsInfo.DataContext = CarsContent;
            CarsInfo.ItemsSource = CarsContent.Products;

        }

        private void DeleteFeedback(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var feedback = button.DataContext as Feedback;
            DeleteFeedbackFromDatabase(feedback.Id);
            Feedbacks_Main feedbacksMain = new Feedbacks_Main();
            ItemControlsFeedbacks.DataContext = feedbacksMain;
            ItemControlsFeedbacks.ItemsSource = feedbacksMain.Feedbacks;



        }

        private void DeleteFeedbackFromDatabase(int DeleteId)
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarSharing;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"DELETE FROM Feedbacks WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", DeleteId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessBox.ShowCustomMessageBox("Successfully");
                        }
                        else
                        {
                            MessBox.ShowCustomMessageBox("Error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button.DataContext as User;
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarSharing;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"DELETE FROM Users WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", user.Id);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessBox.ShowCustomMessageBox("Successfully");
                            User_Main users = new User_Main();
                            ItemControlsUsers.DataContext = users;
                            ItemControlsUsers.ItemsSource = users.Users;
                        }
                        else
                        {
                            MessBox.ShowCustomMessageBox("Error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }


        private void UserImageChange_Click(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.png;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == true)
            {
                if (sender is Image image)
                {
                    var user = image.DataContext as User;

                    if (user != null)
                    {
                        user.ImagePath = openFileDialog.FileName;

                        image.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                    }
                }
            }
        }

        private void UserDataRefresh_Click(object sender, RoutedEventArgs e)
        {


            Button clickedButton = sender as Button;
            var user = clickedButton.DataContext as User;
            if (clickedButton == null) return;

            Border parentBorder = FindParent<Border>(clickedButton);
            if (parentBorder == null) return;

           
            var textBoxes = FindVisualChildren<TextBox>(parentBorder);

            var UserId = user.Id;
            string StringUserImagePath = "";
            string StringUserLogin = "";
            string StringUserRole = "";

            foreach (var textBox in textBoxes)
            {
                switch (textBox.Name)
                {
                    case "UserImagePath":
                        StringUserImagePath = textBox.Text;
                        break;
                    case "UserLogin":
                        StringUserLogin = textBox.Text;
                        break;
                    case "UserImageRole":
                        StringUserRole = textBox.Text;
                        if (!StringUserRole.ToLower().Contains("user") && !StringUserRole.ToLower().Contains("admin"))
                        {
                            MessBox.ShowCustomMessageBox("OnlyAdminOrUser");
                            
                                return;
                        }
                        break;
                  
                }
            }


            var image = FindVisualChildren<Image>(parentBorder).FirstOrDefault(img => img.Name == "UserImagePath");
            if (image != null)
            {
                Uri uri = new Uri(image.Source.ToString());
                StringUserImagePath = uri.LocalPath;
            }
            UpdateUserInDatabase(UserId, StringUserImagePath, StringUserLogin, StringUserRole);
            RefreshGlobalData(UserId);
            try
            {AvatarUpMenu.ImageSource = new BitmapImage(new Uri(GlobalData.ImagePath, UriKind.Absolute));}
            catch (Exception ex) { }



        }


        private void UpdateUserInDatabase(int Id, string ImagePath, string Login, string Role)
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarSharing;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    string query = @"UPDATE Users 
                 SET ImagePath = @ImagePath, 
                     Login = @Login, 
                     Role = @Role
                 WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@Id", Id);
                        command.Parameters.AddWithValue("@ImagePath", ImagePath);
                        command.Parameters.AddWithValue("@Login", Login);                       
                        command.Parameters.AddWithValue("@Role", Role);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessBox.ShowCustomMessageBox("Successfully");

                        }
                        else
                        {
                            MessBox.ShowCustomMessageBox("Error");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }


        private void RefreshGlobalData(int Id)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarSharing;Trusted_Connection=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Users WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", Id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) 
                            {
                                GlobalData.Login = reader["Login"].ToString();
                                GlobalData.ImagePath = reader["ImagePath"].ToString();
                                string role = reader["Role"].ToString();
                                if (role.Contains("dmin"))
                                {
                                    GlobalData.isAdmin = true;
                                }
                                else GlobalData.isAdmin = false;

                            }
                            else
                            {
                                Console.WriteLine("Пользователь с таким логином не найден.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {

            Navigation.OpenWindow(new OrdersWindow());
            //var window = new OrdersWindow();
            //Navigation.OpenWindow(window);

            //window.Show();
            //this.Close();
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

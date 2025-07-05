using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarSharing.View;
using Microsoft.Data.SqlClient;
using System.Windows.Media.Animation;
using CarSharing.Model;
using CarSharing.ViewModel;

namespace CarSharing
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random random = new Random();
        public MainWindow()
        {
            
            InitializeComponent();
            DataContext = new MainViewModel();
            LocalizationManager.SetLanguage("en-US");

            if (DataBaseCheck.DatabaseExists())
            {
                MessageBox.Show("+");
            }
            else DataBaseCheck.MyDataBase();

            this.Cursor = new Cursor("C:\\Users\\fedor\\OneDrive\\Desktop\\CarSharing\\CarSharing\\Model\\Sounds\\Cursor1.ani");
            


        }

        private void EnglishButton_Click(object sender, RoutedEventArgs e)
        {
            LocalizationManager.SetLanguage("en-US");
        }

        private void RussianButton_Click(object sender, RoutedEventArgs e)
        {
            LocalizationManager.SetLanguage("ru-RU");
            
        }
        private void Register_Window_Open(object sender, MouseButtonEventArgs e)
        {
            Register register = new Register();
            register.Show();
            
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



        // ---------------------------------------- Работа с Базой Данных
        private void Shop_Open(object sender, RoutedEventArgs e)
        {
            if (ValidateLoginAndPassword(Login_Field.Text.ToString(), Password_Field.Password.ToString()))
            {
                GlobalDataForXAML.playerLike.Play();
                GetUserData(Login_Field.Text);
                InvalidUserData.Text = "";
                Shop_window window = new Shop_window();
                window.Show();
                this.Close();
            }
            else InvalidUserData.SetResourceReference(TextBlock.TextProperty, "InvalidLogin");
            

        }


        // ---------------------------------------- 
        static bool ValidateLoginAndPassword(string Login, string Password)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarSharing;Trusted_Connection=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Login = @Login AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Login", Login);
                        command.Parameters.AddWithValue("@Password", Password);

                        int count = (int)command.ExecuteScalar(); // Получаем количество совпадений
                        return count > 0; // Если больше 0, логин и пароль совпадают
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    return false;
                }
            }
        }
        //------------------------------------
        public static void GetUserData(string Login)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarSharing;Trusted_Connection=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Users WHERE Login = @Login";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Login", Login);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Проверяем, есть ли строки в результате запроса
                            {
                                GlobalData.Id = int.Parse(reader["Id"].ToString());
                                GlobalData.Login = reader["Login"].ToString();
                                GlobalData.ImagePath = reader["ImagePath"].ToString();
                                GlobalData.Email = reader["Email"].ToString();
                                GlobalData.Password = reader["Password"].ToString();
                                GlobalData.DateTime = Convert.ToDateTime(reader["DateTime"]);
                                GlobalData.FavorutiesCarsId = ParseFavoritesCars(reader["FavorutiesCarsId"].ToString());
                                GlobalData.OrdersCarsId = ParseFavoritesCars(reader["Orders"].ToString());
                                string role = reader["Role"].ToString();
                                if (role.Contains("dmin"))
                                {
                                    GlobalData.isAdmin = true;
                                }else GlobalData.isAdmin = false;

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

        static List<int> ParseFavoritesCars(string favorutiesCarsId)
        {
            var list = new List<int>();
            if (!string.IsNullOrEmpty(favorutiesCarsId))
            {
                string[] ids = favorutiesCarsId.Split(','); // Например, если данные в формате "1,2,3"
                foreach (var id in ids)
                {
                    if (int.TryParse(id, out int result))
                    {
                        list.Add(result);
                    }
                }
            }
            return list;
        }

        private void ButtonAbout(object sender, RoutedEventArgs e)
        {
            AboutUS window = new AboutUS();
            this.Close();
            window.Show();
        }
    }
}

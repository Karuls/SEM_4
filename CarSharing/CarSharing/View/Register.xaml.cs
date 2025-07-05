using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace CarSharing.View
{
  
    public partial class Register : Window
    {

        public Register()
        {
            InitializeComponent();
        }

      

        // Валидация полей
        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            bool AllowsToLogin = true; 

            if (!ValidateEmail(Email_Field.Text.ToString()))
            {
                AllowsToLogin = false;
                Error_Email.SetResourceReference(TextBlock.TextProperty, "InvalidEmail");
            }else Error_Email.Text = "";

            if (!ValidateLogin(Login_Field.Text.ToString()))
            {
                AllowsToLogin = false;
                Error_Login.SetResourceReference(TextBlock.TextProperty, "InvalidLogin");
            }
            else Error_Login.Text = "";

            if (Password_Field1.Password != Password_Field2.Password)
            {

                AllowsToLogin = false;
                Error_Passwords.SetResourceReference(TextBlock.TextProperty, "InvalidPasswords");
            }
            else {
                if (!ValidatePassword(Password_Field1.Password)) {
                    AllowsToLogin = false;
                    Error_Passwords.SetResourceReference(TextBlock.TextProperty, "InvalidPasswords");
                    MessBox.ShowCustomMessageBox("ForPassword");
                }
            }
            Error_Passwords.Text = "";
            

            if (AllowsToLogin)
            {
                DateTime dateTime = DateTime.Now;
                SaveToDatabase(Login_Field.Text, Email_Field.Text, Password_Field1.Password, dateTime, "");
            }


        }

        private void Close_Register(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        static bool ValidateEmail(string email)
        {            
            string emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailRegex);
        }

        bool ValidatePassword(string password)
        {
            return password.Length >= 8 &&
                   Regex.IsMatch(password, @"[A-Z]") && 
                   Regex.IsMatch(password, @"[a-z]") && 
                   Regex.IsMatch(password, @"\d"); 
        }



static bool ValidateLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login) || login.Length < 5 || login.Length > 15)
            {
                return false;
            }

            foreach (char c in login)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }

            if (CheckValueExists(login)) {
                MessageBox.Show("Есть такой логин");
                return false;
            }

            return true;
        }

        private void SaveToDatabase(string login, string email, string password, DateTime dateTime, string favouritesCarsId)
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarSharing;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"INSERT INTO Users (id, Login, Email, Password, DateTime, Role) 
                             VALUES (@id,@Login, @Email, @Password, @DateTime, @Role)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Добавляем параметры
                        command.Parameters.AddWithValue("@id", GetRowCount() + 10);
                        command.Parameters.AddWithValue("@Login", login);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@DateTime", dateTime);
                        command.Parameters.AddWithValue("@Role", "User"); 
                        command.ExecuteNonQuery(); // Выполняем запрос
                        MessBox.ShowCustomMessageBox("RegisterWell");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }



        static int GetRowCount(string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarSharing;Trusted_Connection=True")
        {
            int count = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT COUNT(*) FROM Users";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        count = (int)command.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            return count;
        }

        static bool CheckValueExists(string valueToCheck, string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarSharing;Trusted_Connection=True")
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT COUNT(*) FROM Users WHERE Login = {valueToCheck}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ValueToCheck", valueToCheck);
                        int count = (int)command.ExecuteScalar(); // Получаем количество совпадений
                        return count > 0; // Если больше 0, значение существует
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    return false;
                }
            }
        }
    }
}

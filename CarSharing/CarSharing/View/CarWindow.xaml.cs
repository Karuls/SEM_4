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
  
    public partial class CarWindow : Window
    {
        private int CurrentWindowId;
        public CarWindow(Product product)
        {
            InitializeComponent();

            this.DataContext = product;
            CurrentWindowId = product.Id;

            ThemeChange();

            Feedbacks_Main feedbacksMain = new Feedbacks_Main();
            ItemControlsFeedbacks.DataContext = feedbacksMain;
            ItemControlsFeedbacks.ItemsSource = feedbacksMain.SortForCar(product.Id); // Фильтрация по CarID

            try
            {
                UserPhotoToComment.Source = new BitmapImage(new Uri(GlobalData.ImagePath, UriKind.Absolute));
            }
            catch (Exception ex) { }

        }

        private void UserLoginFeedback_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(UserLoginFeedback.Text))
                {
                    MessBox.ShowCustomMessageBox("Error");
                }else SaveToDatabase();

                UserLoginFeedback.Text = "";
            }
            
        }


        private void SaveToDatabase()
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarSharing;Integrated Security=True";
            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"INSERT INTO Feedbacks (Id, feedback, id_user, AvatarPath, Login_user, Date, id_car) 
                             VALUES (@Id, @feedback, @id_user, @AvatarPath, @Login_user, @Date, @id_car)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        
                        command.Parameters.AddWithValue("@Id", IdForfeedback()) ;
                        command.Parameters.AddWithValue("@feedback", UserLoginFeedback.Text);
                        command.Parameters.AddWithValue("@id_user", GlobalData.Id);
                        command.Parameters.AddWithValue("@AvatarPath", GlobalData.ImagePath);
                        command.Parameters.AddWithValue("@Login_user", GlobalData.Login);
                        command.Parameters.AddWithValue("@Date", DateTime.Now);
                        command.Parameters.AddWithValue("@id_car", CurrentWindowId);
                        command.ExecuteNonQuery();
                        MessBox.ShowCustomMessageBox("CommitAdd");
                        //Refresh
                        Feedbacks_Main feedbacksMain = new Feedbacks_Main();
                        ItemControlsFeedbacks.DataContext = feedbacksMain;
                        ItemControlsFeedbacks.ItemsSource = feedbacksMain.SortForCar(CurrentWindowId);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}");
                }
            }
        }


        public int GetRowCount(string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarSharing;Trusted_Connection=True")
        {
            int count = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT COUNT(*) FROM Feedbacks";
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

            return count+10;
        }

        public int IdForfeedback()
        {
            int count = GetRowCount();
            while (IdInBase(count)) {
                count++;
                IdInBase(count);
            }
           return count;
        }

        public bool IdInBase(int feedbackId, string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CarSharing;Trusted_Connection=True")
        {
            int count = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string checkQuery = "SELECT COUNT(*) FROM Feedbacks WHERE Id = @FeedbackId";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@FeedbackId", feedbackId);
                        int exists = (int)checkCommand.ExecuteScalar();

                        if (exists > 0) // Если ID найден
                        {
                            string query = "SELECT COUNT(*) FROM Feedbacks";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                count = (int)command.ExecuteScalar();
                                return true;
                            }
                        }
                        else
                        {
                            return false; //нету
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
            return true;

            
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {

            if (GlobalDataForXAML.IsSound) GlobalDataForXAML.playerLike.Play();
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarSharing;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT Orders FROM Users WHERE Login = @Login";
                    string currentFavoritesString = "";

                    using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@Login", GlobalData.Login);
                        var result = selectCommand.ExecuteScalar();
                        if (result != null)
                        {
                            currentFavoritesString = result.ToString();
                        }
                    }

                    List<int> Orders = DeserializeFavorites(currentFavoritesString);

                    if (!Orders.Contains(CurrentWindowId))
                    {
                        Orders.Add(CurrentWindowId);
                        GlobalData.OrdersCarsId.Add(CurrentWindowId);
                    }

                    string updatedOrders = SerializeFavorites(Orders);

                    string updateQuery = @"UPDATE Users SET Orders = @Orders WHERE Login = @Login";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Orders", updatedOrders);
                        updateCommand.Parameters.AddWithValue("@Login", GlobalData.Login);

                        updateCommand.ExecuteNonQuery();
                        MessBox.ShowCustomMessageBox("AddToOrders");


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

    }
}

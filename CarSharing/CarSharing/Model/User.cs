using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSharing.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateTime { get; set; }
        public string FavorutiesCarsId { get; set; }
        public string Orders { get; set; }
        public string Role { get; set; }
        public string? ImagePath { get; set; }
    }


    public class User_Main
    {

        public ObservableCollection<User> Users { get; set; }

        public User_Main()
        {
            LoadUsers();
        }
        private void LoadUsers()
        {
            using (var db = new EntityFramework()) // Подключение к вашей БД
            {
                Users = new ObservableCollection<User>(db.Users.ToList());

            }
        }
    }


    public static class GlobalData
    {
        public static int Id { get; set; }
        public static string Login { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static DateTime DateTime { get; set; }
        public static List<int>? FavorutiesCarsId { get; set; }
        public static List<int>? OrdersCarsId { get; set; }
        public static bool isAdmin { get; set; }
        public static string? ImagePath { get; set; }
    }

    public static class ForUndo
    {
        public static Stack<List<int>>? FavorutiesCarsId { get; set; }
        public static Stack<List<int>>? OrdersCarsId { get; set; }
        public static Stack<Order> OrdersHistory { get; set; }
        public static Stack<Product> Products { get; set; }

        public static string lastupdate;

    }
}
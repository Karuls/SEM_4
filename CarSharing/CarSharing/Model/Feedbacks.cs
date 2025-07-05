using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class Feedback
{
    public int Id { get; set; }
    public string feedback { get; set; }
    public int id_user { get; set; }
    public string AvatarPath { get; set; }
    public string Login_user { get; set; }
    public DateTime Date { get; set; }

    public int Id_car { get; set; }

}

namespace CarSharing.Model
{
    public class Feedbacks_Main
    {

        public ObservableCollection<Feedback> Feedbacks { get; set; }

        public Feedbacks_Main()
        {
            LoadFeedbacks();
        }
        private void LoadFeedbacks()
        {
            using (var db = new EntityFramework()) // Подключение к вашей БД
            {
                Feedbacks = new ObservableCollection<Feedback>(db.Feedbacks.ToList());

            }
        }

        public ObservableCollection<Feedback> SortForCar(int CarID)
        {

            using (var context = new EntityFramework())
            {
                var sortedFeedbacks = context.Feedbacks
                    .Where(f => f.Id_car == CarID)
                    .OrderBy(f => f.Id) 
                    .ToList();

                return new ObservableCollection<Feedback>(sortedFeedbacks);
            }

        }

    }
}

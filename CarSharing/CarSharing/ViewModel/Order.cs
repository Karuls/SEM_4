using CarSharing.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Order
{
    public int Id { get; set; }  // Первичный ключ
    public int id_user { get; set; }
    public int id_car { get; set; }
    public string imagepath { get; set; }
    public DateTime date { get; set; }
    public int coast { get; set; }
    public int hours { get; set; }

    public int Index { get; set; }
}
public class Order_main
{
    public ObservableCollection<Order> Orders { get; set; }

    public Order_main()
    {
        LoadOrders();
    }

    private void LoadOrders()
    {
        using (var db = new EntityFramework()) // Подключение к вашей БД
        {
            Orders = new ObservableCollection<Order>(db.Orders.ToList());
            var List = Orders.ToList();

            for (int i = 0; i < List.Count; i++) {
                List[i].Index = i+1;
            }

            Orders.Clear();

            foreach (var product in List)
            {
                Orders.Add(product);
            }

        }
    }

    public ObservableCollection<Order> OrdersHistoryById(int ID)
    {
      
            //Orders = new ObservableCollection<Order>(db.Orders.ToList());
            for (int i = 0; i < Orders.Count; i++)
            {
                if (Orders[i].id_user != ID)
                {
                    Orders.Remove(Orders[i]);
                }
            }
            for(int i = 0;i < Orders.Count; i++)
            {
                Orders[i].Index = i + 1;
            }
            return Orders;


           
        
    }
}


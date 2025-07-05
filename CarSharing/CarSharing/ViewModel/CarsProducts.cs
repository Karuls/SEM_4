using CarSharing.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Product
{
    public int Id { get; set; }  // Первичный ключ
    public string CarName { get; set; }
    public string? Пробег { get; set; }
    public string? Цвет { get; set; }
    public int? Количество { get; set; }
    public int Цена { get; set; }
    public string ImagePath { get; set; }
    public string Допустимый_пробег { get; set; }
    public string Features { get; set; }
    public string Объем_Двигателя { get; set; }
    public string? Transmission { get; set; }
    public int Top_speed { get; set; }
    public int? IsFavoruties { get; set; }
}

public class Main1
{
    public ObservableCollection<Product> Products { get; set; }

    public Main1()
    {
        LoadProducts();
    }

    private void LoadProducts()
    {
        using (var db = new EntityFramework()) // Подключение к вашей БД
        {
            Products = new ObservableCollection<Product>(db.Products.ToList());
            var List = Products.ToList();

           // GlobalData.FavorutiesCarsId = new List<int> { 1,2,3,4 };
            if (GlobalData.FavorutiesCarsId.Count != 0)
            {
                for (int i = 0; i < List.Count; i++)
                {
                    if (GlobalData.FavorutiesCarsId.Contains(List[i].Id))
                    {
                        List[i].IsFavoruties = 1;
                    }
                }

                Products.Clear();

                foreach (var product in List)
                {
                    Products.Add(product);
                }
            }
          

        }
    }
    public void SortProductsByPrice(bool ascending)
    {
        var sortedList = ascending
         ? Products.OrderBy(p => p.Цена).ToList()
         : Products.OrderByDescending(p => p.Цена).ToList();

        Products.Clear();

        foreach (var product in sortedList)
        {
            Products.Add(product); 
        }
    }

    public void SortProductsByCarName(string carname, bool ascending = true)
    {
        if (!string.IsNullOrEmpty(carname))
        {
            var filteredList = Products.Where(p => p.CarName.ToLower().Contains(carname.ToLower()));

            var sortedList = ascending
                ? filteredList.OrderBy(p => p.Цена).ToList()
                : filteredList.OrderByDescending(p => p.Цена).ToList();

            Products.Clear();

            foreach (var product in sortedList)
            {
                Products.Add(product);
            }
        }
    }

    public void SortForFavoruties(List<int> CARSID, bool ascending = true)
    {
        if (CARSID == null || CARSID.Count == 0) {
            Products.Clear();
            return;
        } 

        var filteredList = Products.Where(p => CARSID.Contains(p.Id)).ToList();

        Products.Clear();

        foreach (var product in filteredList)
        {
            Products.Add(product);
        }
    }

    public void SortForOrders(List<int> CARSID, bool ascending = true)
    {
        if (CARSID == null || CARSID.Count == 0) return;

        var filteredList = Products.Where(p => CARSID.Contains(p.Id)).ToList();

        Products.Clear();

        foreach (var product in filteredList)
        {
            Products.Add(product);
        }
    }

    public Product GetProduct(int id)
    {

        using (var db = new EntityFramework())
        {
            Products = new ObservableCollection<Product>(db.Products.ToList());
            var List = Products.ToList();

            for(int i = 0; i < List.Count; i++)
            {
                if (List[i].Id == id)
                {
                    return List[i];
                }
            }

            return null;

        }

    }

    public void SortProductsBySpeed(bool ascending)
    {
        var sortedList = ascending
         ? Products.OrderBy(p => p.Top_speed).ToList()
         : Products.OrderByDescending(p => p.Top_speed).ToList();

        Products.Clear();

        foreach (var product in sortedList)
        {
            Products.Add(product);
        }
    }

    public void SortProductsByMotor(bool ascending)
    {
        var sortedList = ascending
         ? Products.OrderBy(p => p.Объем_Двигателя).ToList()
         : Products.OrderByDescending(p => p.Объем_Двигателя).ToList();

        Products.Clear();

        foreach (var product in sortedList)
        {
            Products.Add(product);
        }
    }

    //public void FavorutiesLoad()
    //{
    //    var List = Products.ToList();

    //    for (int i = 0; i < List.Count; i++)
    //    {
    //        if (GlobalData.FavorutiesCarsId.Contains(List[i].Id))
    //        {
    //            List[i].IsFavoruties = true;
    //        }
    //    }

    //    Products.Clear();

    //    foreach (var product in List)
    //    {
    //        Products.Add(product);
    //    }
    //}




}

using CarSharing.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;


    public static class GlobalDataForXAML
    {
    public static string ConnectionStringToDB = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarSharing;Integrated Security=True";
    public static SoundPlayer playerLike = new SoundPlayer("C:\\Users\\fedor\\OneDrive\\Desktop\\CarSharing\\CarSharing\\Model\\Sounds\\facebook.wav");
     public static string Theme = "Night";
     public static bool IsSound = true;

    public static Stack<List<int>> undoStack = new Stack<List<int>>();
    public static Stack<List<int>> redoStack = new Stack<List<int>>(); 


    public static void Redo()
    {
        if (redoStack.Count > 0)
        {
            undoStack.Push(new List<int>(GlobalData.FavorutiesCarsId));
            GlobalData.FavorutiesCarsId = redoStack.Pop();
            UpdateFavoritesInDatabase();
        }
    }

    public static void Undo()
    {
        if (undoStack.Count > 0)
        {
            redoStack.Push(new List<int>(GlobalData.FavorutiesCarsId));
            GlobalData.FavorutiesCarsId = undoStack.Pop();
            UpdateFavoritesInDatabase();
        }
    }

    private static void UpdateFavoritesInDatabase()
    {
        string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CarSharing;Integrated Security=True";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();

             

                // 4️⃣ Сериализуем обновлённый список обратно в строку
                string updatedFavorites = SerializeFavorites(GlobalData.FavorutiesCarsId);

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
                //MessageBox.Show($"Ошибка: {ex.Message}");
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

}


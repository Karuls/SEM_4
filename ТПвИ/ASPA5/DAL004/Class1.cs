using System.Collections.Immutable;
using System.Text.Json;

namespace DAL004
{
    public class Class1
    {

    }

    public interface IRepository : IDisposable
    {
        string BasePath { get; }                                            // полный директорий для JSON и фотографий
        Celebrity[] getAllCelebrities();                                    // получить весь список знаменитостей
        Celebrity? getCelebrityById(int id);                                // получить знаменитость по Id
        Celebrity[] getCelebritiesBySurname(string surname);                // получить знаменитостей по фамилии
        string? getPhotoPathById(int id);                                   // получить путь для GET-запроса к фотографии
        int? addCelebrity(Celebrity celebrity);                             // добавить знаменитость, =id новой знаменитости 
        bool delCelebrityById(int id);                                      // удалить знаменитость по id, =true - успех
        int? upCelebrityById(int id, Celebrity celebrity);                  // изменить знаменитость по id, =id - новый id - успех
        int SaveChange();                                                   // сохранить изменения в JSON, =количество изменений
        
        bool DoesSurnameExist(string surname);
        bool DoesCelebrityExist(int id);
    }

    public class Repository: IRepository
    {
        public static string JSONFileName { get; set; } = "Celebrity.json";
        public string BasePath { get; set;}
        private Celebrity[] celebrities;

        public Repository(string basePath)
        {
            BasePath = basePath;
            string JSONFilePath = Path.Combine(basePath, JSONFileName);

            if (!File.Exists(JSONFilePath))
            {
                throw new FileNotFoundException($"Не найдено {JSONFilePath}");
            }
            string jsonData = File.ReadAllText(JSONFilePath);
            celebrities = JsonSerializer.Deserialize<Celebrity[]>(jsonData) ?? throw new InvalidOperationException();


        }
        public Celebrity[] getAllCelebrities()
        {

            return celebrities;
        }

        public Celebrity? getCelebrityById(int id)
        {
            return celebrities.FirstOrDefault(c => c.Id == id);
        }

        public Celebrity[] getCelebritiesBySurname(string surname)
        {
            return celebrities.Where(c => c.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase)).ToArray();
        }

        public string? getPhotoPathById(int id)
        {
            Celebrity? celebrity = getCelebrityById(id);
            return celebrity?.PhotoPath;
        }

        public int? addCelebrity(Celebrity celebrity)
        {
            int newId = celebrities.Any() ? celebrities.Max(c => c.Id) + 1 : 1;
            if (celebrity.Id <= 0)
            {
                celebrity = celebrity with { Id = newId };
            }
            else if (celebrities.Any(cel => cel.Id == celebrity.Id))
                return null;
            celebrities = celebrities.Append(celebrity).ToArray();
            return celebrity.Id;
        }

        public bool delCelebrityById(int id)
        {
            var count = celebrities.Length;
            celebrities=celebrities.Where((c) => c.Id != id).ToArray();
            return celebrities.Length < count;
        }

        public int? upCelebrityById(int id, Celebrity celebrity)
        {
            int index = Array.FindIndex(celebrities, c => c.Id == id);
            if(index == -1)
                return null;
            celebrities[index] = celebrity;
            return id;
        }

        public int SaveChange()
        {
          
            string JSONFilePath = Path.Combine(BasePath, JSONFileName);
            string jsonData = JsonSerializer.Serialize(celebrities, new JsonSerializerOptions {WriteIndented=true });
            File.WriteAllText(JSONFilePath, jsonData);
            return celebrities.Length;
        }
        public static IRepository Create(string basePath)
        {
            return new Repository(basePath);
        }
        public void Dispose()
        {

        }
        public bool DoesSurnameExist(string surname)
        {  
            return celebrities.Any(c => c.Surname == surname);
        }
        public bool DoesCelebrityExist(int id)
        {
            return celebrities.Any((c) => c.Id == id);
        }
    }
   
    public record Celebrity(int Id, string Firstname, string Surname, string PhotoPath);

}

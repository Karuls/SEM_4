using DAL_Celebrity_MSSQL;
internal class Program
{
    static void Main(string[] args)
    {
        string CS = "Server=DESKTOP-75R24I1; Database = Lab6; TrustServerCertificate=True; Trusted_Connection=true";
        

        Init init = new Init(CS);
        Init.Execute(create: true, delete: true);

        Func<Celebrity, string> PrintC = (Celebrity c) => $"Id = {c.Id}, FullName = {c.FullName}, Nationality = {c.Nationality}, ReqPhotoPath = {c.ReqPhotoPath}";
        Func<LifeEvent, string> PrintL = (LifeEvent l) => $"Id = {l.Id}, CelebrityId = {l.CelebrityId}, Date = {l.Date},Description = {l.Description}, ReqPhotoPath = {l.ReqPhotoPath}";
        Func<string, string> puri = (f) => $"{f}";

        using (IRepository repo = Repository.Create(CS))
        {
            {
                Console.WriteLine("------GetAllCelebrity()------");
                repo.GetAllCelebrities().ForEach(celeb => Console.WriteLine(PrintC(celeb)));
            }
            {
                Console.WriteLine("------GetAllLifeEvents()------");
                repo.GetAllLifeEvents().ForEach(even => Console.WriteLine(PrintL(even)));
            }
            {
                Console.WriteLine("------AddCelebrity()------");

                Celebrity c = new Celebrity() { FullName = "Albert Einstien", Nationality = "DE", ReqPhotoPath = puri("Einstein.jpg") };

                if (repo.AddCelebrity(c)) Console.WriteLine($"OK: AddCelebrity{PrintC(c)}");

                else Console.WriteLine($"ERROR: AddCelebrity{PrintC(c)}");

            }
            {
                Console.WriteLine("------AddCelebrity()------");
                Celebrity c = new Celebrity() { FullName = "Samuel Huntington", Nationality = "US", ReqPhotoPath = puri("Huntington.jpg") };
                if (repo.AddCelebrity(c)) Console.WriteLine($"OK: AddCelebrity {PrintC(c)}");
                else Console.WriteLine($"ERROR: AddCelebrity{PrintC(c)}");
            }
            {
                Console.WriteLine("------DelCelebrity()------");
                int id = 0;
                id = repo.GetCelebrityByName("Albert Einstien");
                if (id > 0) { repo.DeleteCelebrity(id); Console.WriteLine($"OK: DelCelebrity {id}"); }
                // Затем в консоль выводится подтверждение:
                else Console.WriteLine($"ERROR: GetCelebIdByNam ");
            }
            {
                Console.WriteLine("------UpdCelebrity()------");

                int id = 0;
                id = repo.GetCelebrityByName("Samuel Huntington");
                if (id > 0)
                {
                    Celebrity? c = repo.GetCelebrityById(id);
                    if (c is not null)
                    {
                        c.FullName += " Updated";
                        repo.UpdateCelebrity(id, c);
                        Console.WriteLine($"OK: UpdCelebrity{PrintC(c)}");
                    }
                    else Console.WriteLine($"ERROR: GetCelebById ");
                }

                else Console.WriteLine($"ERROR: GetCelebIdByNam ");
            }

            {
                Console.WriteLine("------AddLifeEvent()------");

                int id = 0;
                id = repo.GetCelebrityByName("Samuel Huntington Updated");
                if (id > 0)
                {// 
                    LifeEvent le = new LifeEvent() { CelebrityId = id, Date = new DateTime(1991, 1, 1), Description = "Kogda ua posplu?", ReqPhotoPath = null };
                    LifeEvent le2 = new LifeEvent() { CelebrityId = id, Date = new DateTime(1991, 6, 1), Description = "SPATTTT:(((", ReqPhotoPath = null };
                    LifeEvent le3 = new LifeEvent() { CelebrityId = id, Date = new DateTime(1991, 9, 1), Description = "OOP:(", ReqPhotoPath = null };
                    Celebrity? c = repo.GetCelebrityById(id);
                    if (c is not null)
                    {
                        if (repo.AddLifeEvent(le)) Console.WriteLine($"OK: AddLifeEvent {PrintL(le)}");
                        else Console.WriteLine($"ERROR: AddLifeEvent {PrintL(le)}");
                        if (repo.AddLifeEvent(le2)) Console.WriteLine($"OK: AddLifeEvent {PrintL(le2)}");
                        else Console.WriteLine($"ERROR: AddLifeEvent {PrintL(le2)}");
                        if (repo.AddLifeEvent(le3)) Console.WriteLine($"OK: AddLifeEvent {PrintL(le3)}");
                        else Console.WriteLine($"ERROR: AddLifeEvent {PrintL(le3)}");
                    }
                    else Console.WriteLine($"ERROR: GetCelebById ");
                }

                else Console.WriteLine($"ERROR: GetCelebIdByNam ");
            }

            {
                Console.WriteLine("------DelLifeEvent()------");
                int id = 22;
                if (repo.DeleteLifeEvent(id)) { Console.WriteLine($"OK: DelEvent {id} "); }
                else Console.WriteLine($"ERROR: DelEvent {id} ");
            }

            {
                Console.WriteLine("------UpdLifeEvent()------");
                int id = 21;
                LifeEvent? l1 = repo.GetLifeEventById(id);

                if (l1 is not null)
                {
                    l1.Description += "Дата смерти";
                    if (repo.UpdateLifeEvent(id, l1)) Console.WriteLine($"OK: UpdEvent {id},{PrintL(l1)} ");
                    else Console.WriteLine($"ERROR: UpdLifeEvent {id},{PrintL(l1)} ");
                }
            }

            {
                Console.WriteLine("------GetLifeEventsByCelebrityId()------");
                int id = 0;
                if ((id = repo.GetCelebrityByName("Samuel Huntington Updated")) > 0)
                {
                    Celebrity? c = repo.GetCelebrityById(id);
                    if (c != null)
                        repo.GetLifeEventsByCelebrityId(c.Id).ForEach(l => Console.WriteLine($"OK: GetEventsByCelebrityId, {id} {PrintL(l)} ")); 
                    else Console.WriteLine($"ERROR: GetEventsByCelebrityId {id} ");
                }

                else Console.WriteLine($"ERROR: GetCelebIdByName ");
            }

            {
                Console.WriteLine("------GetCelebrityByLifeEventId()------");
                int id = 23;
                Celebrity? c;
                if ((c = repo.GetCelebrityByLifeEventId(id)) != null)
                    Console.WriteLine($"OK: {PrintC(c)} "); 
                else Console.WriteLine($"ERROR: GetCelebrityByLifeEventId {id}");
            }
        }
        Console.WriteLine("--------->"); Console.ReadKey();
    }
}
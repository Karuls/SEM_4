/*// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");*/

using System;
using System.Runtime.InteropServices;
using DAL004;

namespace Test_DAL004
{
    class Program
    {
        private static void Main(string[] args)
        {

            Repository.JSONFileName = "Celebrities.json";
            using (IRepository repository = Repository.Create("C:\\Users\\fedor\\OneDrive\\Desktop\\ASPA\\Celebrities"))
            {
                void print(string label)
                {
                    Console.WriteLine("---"+ label+"-------------");
                    foreach(Celebrity celebrity in repository.getAllCelebrities())
                    {
                        Console.WriteLine($"Id ={celebrity.Id}, Firstname = {celebrity.Firstname}, "+
                            $"Surname = {celebrity.Surname}, PhotoPath = {celebrity.PhotoPath}");
                    }
                };
                print("start");

                int? testdel1 = repository.addCelebrity(new Celebrity(8, "TestDel1", "TestDel1", "Photo/TestDel1.jpg"));
                int? testdel2 = repository.addCelebrity(new Celebrity(9, "TestDel2", "TestDel2", "Photo/TestDel2.jpg"));
                int? testup1 = repository.addCelebrity(new Celebrity(10, "TestUp1", "TestUp1", "Photo/TestUp1.jpg"));
                int? testup2 = repository.addCelebrity(new Celebrity(11, "TestUp2", "TestUp2", "Photo/TestUp2.jpg"));
                repository.SaveChange();
                print("add 4");

                if(testdel1 != null)
                    if(repository.delCelebrityById((int)testdel1)) Console.WriteLine($"delete {testdel1}"); else Console.WriteLine($"delete {testdel1} error");
                if (testdel2 != null)
                    if (repository.delCelebrityById((int)testdel2)) Console.WriteLine($"delete {testdel2}"); else Console.WriteLine($"delete {testdel2} error");
                if (repository.delCelebrityById(1000)) Console.WriteLine($"delete{1000}"); else Console.WriteLine($"delete {1000} error");
                repository.SaveChange();
                print("del 2");
                /*
                                if (testup1 != null)
                                    if (repository.upCelebrityById((int)testup1, new Celebrity(0, "Updated1", "Updated1", "Photo/Updated1.jpg"))) Console.WriteLine($"update {testup1}");
                                    else Console.WriteLine($"update {testup1} error");*/
                if (testup1 != null)
                {
                    int? updatedResult1 = repository.upCelebrityById((int)testup1, new Celebrity(12, "Updated1", "Updated1", "Photo/Updated1.jpg"));
                    if (updatedResult1 != null)
                    {
                        Console.WriteLine($"update {testup1}");
                    }
                    else
                    {
                        Console.WriteLine($"update {testup1} error");
                    }
                }

                if(testup2 != null)
                {
                    int? updatedResult2 = repository.upCelebrityById((int)testup2, new Celebrity(13, "Updated2", "Updated2", "Photo/Updated2.jpg"));
                    if (updatedResult2 != null)
                    {
                        Console.WriteLine($"update {testup2}");
                    }
                    else
                    {
                        Console.WriteLine($"update {testup2} error");
                    }
                }

                int? updatedResult1000 = repository.upCelebrityById(1000, new Celebrity(0, "Updated1000", "Updated1000", "Photo/Updated1000.jpg"));
                if (updatedResult1000 != null)
                {
                    Console.WriteLine($"update {1000}");
                }
                else
                {
                    Console.WriteLine($"update {1000} error");
                }
                
                repository.SaveChange();
                print("upd 2");
            }
        }
    }
}
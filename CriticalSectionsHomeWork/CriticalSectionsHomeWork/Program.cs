using Bogus;
using CriticalSectionsHomeWork.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
class Program
{
    static  List<User> listOfUsers = new();
    static  object obj = new();
    static  int countThreads = 5; 

    static void generateUsers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Faker<User> faker = new();

            var users = faker.RuleFor(u => u.Name, f => f.Person.FirstName)
                .RuleFor(u => u.Surname, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.DateOfBirth, f => f.Person.DateOfBirth)
                .Generate(50);

            var json = JsonSerializer.Serialize(users);
            File.WriteAllText($"users{i + 1}.json", json);
        }
    }

    static void SingleThread()
    {
        for (int i = 1; i <= 5; i++)
        {
            string filePath = $"users{i}.json";
            var json = File.ReadAllText(filePath);
            var users = JsonSerializer.Deserialize<List<User>>(json);

            lock (obj)
            {
                listOfUsers.AddRange(users!);
            }
        }
    }

    static void MultipleThread()
    {
        for (int i = 1; i <= 5; i++)
        {
            int index = i;

            ThreadPool.QueueUserWorkItem(t =>
            {
                string filePath = $"users{index}.json";
                var json = File.ReadAllText(filePath);
                var users = JsonSerializer.Deserialize<List<User>>(json);

                lock (obj) 
                {
                    listOfUsers.AddRange(users!);
                }

                Interlocked.Decrement(ref countThreads);
            });
        }

        while (countThreads > 0)
        {
            Thread.Sleep(100);
        }
    }
    static void Main(string[] args)
    {
        generateUsers(5);

        Console.WriteLine("1. Single Thread");
        Console.WriteLine("2. Multiple Threads");

        var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                SingleThread();
                break;
            case "2":
                MultipleThread();
                break;
            default:
                Console.WriteLine("Wrong operation!");
                Thread.Sleep(1000);
                break;
        }

        Console.WriteLine($"Total User Count: {listOfUsers.Count}");
    }
}

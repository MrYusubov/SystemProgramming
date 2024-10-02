using System;
using System.IO;
using System.Threading;

class Program
{
    static void EncryptFile(string path, string key)
    {
        try
        {
            string oldName = Path.GetFileNameWithoutExtension(path);
            string newName = $"{oldName}Encrypted.txt";
            string newPath = Path.Combine(Path.GetDirectoryName(path)!, newName);

            string data = File.ReadAllText(path);
            string encData = EncryptData(data, key);

            File.WriteAllText(newPath, encData);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Something went wrong!");
        }
    }

    static string EncryptData(string data, string key)
    {
        char[] arr = new char[data.Length];

        for (int i = 0; i < data.Length; i++)
        {
            arr[i] = (char)(data[i] ^ key[i]); 
        }

        return new string(arr);
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Enter Path: ");
        string path = Console.ReadLine()!;

        if (!File.Exists(path) || Path.GetExtension(path) != ".txt")
        {
            Console.WriteLine("Wrong Path!");
            return;
        }

        Console.WriteLine("Enter the key: ");
        string key = Console.ReadLine()!;

        ThreadPool.QueueUserWorkItem(_ =>
        {
            EncryptFile(path, key);
            Console.WriteLine("File Encrypted Successfully");
        });

        Console.ReadKey(); 
    }


}

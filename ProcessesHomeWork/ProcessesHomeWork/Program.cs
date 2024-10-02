using System.Diagnostics;
public class Program
{
    private static void Main(string[] args)
    {
        //Task
        // =>
        //console app
        // butun processler gorunur, sonda ise, New Task, End Task bolmesi olur.
        // eger New Task sechilse, process adi daxil olunur, ve hemin process ishe dushur.
        // eger End Task sechilse, process id'si daxil olunur, ve hemin process kill olunur
        // <=
        while (true)
        {
            Console.WriteLine("1.Show All Tasks");
            Console.WriteLine("2.New Task");
            Console.WriteLine("3.End Task");
            Console.WriteLine("4.Exit");
            Console.WriteLine("Enter Your Choice: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    var processes = Process.GetProcesses();
                    foreach (var process in processes)
                    {
                        Console.WriteLine($"Id: {process.Id} , Process Name: {process.ProcessName}");
                    }
                    break;
                case "2":
                    try
                    {
                        Console.WriteLine("Enter the Process Name to Start: ");
                        var procName = Console.ReadLine();
                        Process.Start(procName!);
                        Console.WriteLine("Process started succesifully!");
                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Please be careful,Something Went wrong!");
                        Thread.Sleep(1000);
                    }
                    finally { Console.Clear(); }
                    break;
                case "3":
                    try
                    {
                        Console.WriteLine("Enter Process Id which you Kill: ");
                        var procId = Convert.ToInt32(Console.ReadLine());
                        var proc = Process.GetProcessById(procId);
                        proc.Kill();
                        Console.WriteLine("Process Killed succesifully!");
                        Thread.Sleep(1000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Please be careful,Something Went wrong!");
                        Thread.Sleep(1000);
                    }
                    finally { Console.Clear(); }
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Wrong operation!");
                    Thread.Sleep(1000);
                    break;
            }
        }


    }
}
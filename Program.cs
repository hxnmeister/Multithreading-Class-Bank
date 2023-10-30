using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Multithreading__Class_Bank_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int money = 0;
            int percent = 0;
            Bank bank = null;
            char choice;
            string name = null;

            do
            {
                Console.Clear();
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("\n No information has been added yet!");
                }
                else
                {
                    Console.WriteLine("\n Data that is currently available:\n");
                    Console.WriteLine($"  Name: {name}");
                    Console.WriteLine($"  Percent: {percent}%");
                    Console.WriteLine($"  Money: {money} UAH");
                }

                Console.WriteLine("\n Select an option:\n");
                Console.WriteLine("  1. Add/Edit information;");
                Console.WriteLine("  0. End session;\n");

                Console.Write("\n Enter your choice: ");
                choice = Console.ReadKey(false).KeyChar;

                Console.Clear();
                switch (choice)
                {
                    case '1':
                        if(string.IsNullOrEmpty(name))
                        {
                            Console.Write("\n Enter name: ");
                            name = Console.ReadLine();

                            while (!CheckNumberInput(out percent, "percent"));
                            while (!CheckNumberInput(out money, "money"));

                            bank = new Bank(money, percent, name);
                        }
                        else
                        {
                            char subChoice;

                            do
                            {
                                Console.WriteLine("\n Choose field to edit:\n");
                                Console.WriteLine("  1. Name;");
                                Console.WriteLine("  2. Percent;");
                                Console.WriteLine("  3. Money;");
                                Console.WriteLine("  0. Return to main menu;\n");

                                Console.Write("\n Enter your choice: ");
                                subChoice = Console.ReadKey(false).KeyChar;

                                Console.Clear();
                                switch (subChoice)
                                {
                                    case '1':
                                        Console.Write("\n Enter name: ");
                                        bank.Name = Console.ReadLine();
                                        break;

                                    case '2':
                                        while (!CheckNumberInput(out percent, "percent"));
                                        bank.Percent = percent;
                                        break;

                                    case '3':
                                        while (!CheckNumberInput(out money, "money")) ;
                                        bank.Money = money;
                                        break;

                                    case '0':
                                        Console.WriteLine("\n Returning to main menu...\n");
                                        break;

                                    default:
                                        Console.WriteLine($"\n Invalid character \"{subChoice}\", please re-enter!");
                                        Console.WriteLine("----------------------------------------");
                                        break;
                                }

                            } while (subChoice != '0');
                        }

                        break;

                    case '0':
                        Console.WriteLine("\n The application is closing...\n\n");
                        return;

                    default:
                        Console.WriteLine($"\n Invalid character \"{choice}\", please re-enter!");
                        Console.WriteLine("----------------------------------------");
                        break;
                }
            } while (choice != '0');
        }

        static bool CheckNumberInput(out int number, string numberName)
        {
            Console.Write($" Enter {numberName}: ");

            if (Int32.TryParse(Console.ReadLine(), out number))
            {
                return true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"\n Incorrect input, try again!");
                return false;
            }
        }
    }
}

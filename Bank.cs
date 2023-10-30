using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Multithreading__Class_Bank_
{
    internal class Bank
    {
        private int money;
        private int percent;
        private string name;
        private readonly string fileName;

        public Bank(int money, int percent, string name, string fileName = "BankData")
        {
            this.money = money;
            this.percent = percent;
            this.name = name;
            this.fileName = fileName;
        }

        public int Money
        {
            get 
            { 
                return money; 
            }
            set
            {
                DedicateThread(fileName);

                this.money = value;
            }
        }
        public int Percent
        {
            get 
            { 
                return percent; 
            }
            set
            {
                DedicateThread(fileName);

                this.percent = value;
            }
        }
        public string Name
        {
            get 
            { 
                return name; 
            }
            set
            {
                DedicateThread(fileName);

                this.name = value;
            }
        }

        private void DedicateThread(object fileName)
        {
            Thread dedicatedThread = new Thread(new ParameterizedThreadStart(SaveDataToFile)) { IsBackground = true };
            dedicatedThread.Start((string)fileName);
            dedicatedThread.Join();
        }

        private void SaveDataToFile(object item)
        {
            try
            {
                if (item is String fileName)
                {
                    using (StreamWriter sw = new StreamWriter($"../../{fileName}.txt", true))
                    {
                        sw.WriteLine($" Saved at: {DateTime.Now.ToLocalTime()}");
                        sw.WriteLine($" Name: {name}");
                        sw.WriteLine($" Money: {money}");
                        sw.WriteLine($" Percent: {percent}");
                        sw.WriteLine("---------------------");

                        Console.WriteLine($"\n All previous data was saved to {fileName}.txt");
                    }
                }
                else
                {
                    throw new Exception($" Data type {item.GetType().Name} is not supported!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Exception Message: {ex.Message}");
            }
        }
    }
}

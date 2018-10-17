using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        private List<VendingMachineItem> items = new List<VendingMachineItem>();

        public decimal CurrentMoneyProvided { get; set; }
        public decimal TotalSales { get; set; }

        public VendingMachine()
        {
            CurrentMoneyProvided = 0.00M;
            TotalSales = 0.00M;
        }

        public VendingMachineItem[] ItemsArray()
        {
            return items.ToArray();
        }
        public void ReadFile()
        {
            string filePath = @"C:\VendingMachine";
            string fileName = "vendingmachine.csv";
            string fullPath = Path.Combine(filePath, fileName);

            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split('|');
                        items.Add(new VendingMachineItem(line[0], line[1], line[2]));
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading file");
                Console.WriteLine(ex.Message);
            }
        }
        public string FeedMoney(decimal input)
        {
            string success = "Failure";
            if (input == 1 || input == 2 || input == 5 || input == 10)
            {
                CurrentMoneyProvided += input;
                Audit("FEED MONEY:", CurrentMoneyProvided - input, CurrentMoneyProvided);
                success = "Success";
            }
            return success;
        }
        public string SelectProduct(string input)
        {
            string status = "Success";
            int counter = 0;
            foreach (VendingMachineItem item in items)
            {
                if (input == item.Name || input == item.SlotID)
                {
                    if (item.Price <= CurrentMoneyProvided)
                    {
                        if (item.Quantity > 0)
                        {
                            item.Quantity--;
                            TotalSales += item.Price;
                            CurrentMoneyProvided -= item.Price;
                            status = item.PrintMessage;
                            Audit(item.Name + " " + item.SlotID, CurrentMoneyProvided + item.Price, CurrentMoneyProvided);
                        }
                        else
                        {
                            status = "Sold Out";
                        }
                    }
                    else
                    {
                        status = "Insufficient Funds";
                    }
                    counter++;
                }
            }
            if (counter == 0)
            {
                status = "Fake Item";
            }
            return status;
        }
        public string FinishTransaction()
        {
            decimal startingValue = CurrentMoneyProvided;
            Dictionary<string, int> change = new Dictionary<string, int>()
            {
                {"Quarters", 0}, {"Dimes", 0}, {"Nickels", 0}
            };
            while (CurrentMoneyProvided >= 0.25M)
            {
                CurrentMoneyProvided -= .25M;
                change["Quarters"]++;
            }
            while (CurrentMoneyProvided >= 0.10M)
            {
                CurrentMoneyProvided -= .10M;
                change["Dimes"]++;
            }
            while (CurrentMoneyProvided >= 0.05M)
            {
                CurrentMoneyProvided -= .05M;
                change["Nickels"]++;
            }
            string coinUsage = $"You have {change["Quarters"]} quarter(s), {change["Dimes"]} dime(s), and {change["Nickels"]} nickel(s) worth of change.";
            Audit("GIVE CHANGE:", startingValue, CurrentMoneyProvided);
            return coinUsage;
        }
        public void Audit(string action, decimal initialValue, decimal outgoingValue)
        {
            string filePath = @"C:\VendingMachine";
            string fileName = "Log.txt";
            string fullPath = Path.Combine(filePath, fileName);

            try
            {
                using (StreamWriter sw = new StreamWriter(fullPath, true))
                {
                    sw.WriteLine($"{DateTime.Now} {action} {initialValue} {outgoingValue}");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading file");
                Console.WriteLine(ex.Message);
            }
        }

        public void SalesReport()
        {
            string kobe = DateTime.Now.ToString().Replace(' ', '_').Replace('/', '-').Replace(':', '-');
            string filePath = @"C:\VendingMachine";
            string fileName = $"{kobe}_SalesReport.txt";
            string fullPath = Path.Combine(filePath, fileName);

            try
            {
                using (StreamWriter sw = new StreamWriter(fullPath, true))
                {
                    foreach(VendingMachineItem item in items)
                    {
                        sw.WriteLine($"{item.Name}|{5 - item.Quantity}");
                    }
                    sw.WriteLine();
                    sw.WriteLine($"** TOTAL SALES ** ${TotalSales}");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading file");
                Console.WriteLine(ex.Message);
            }
        }
    }
}

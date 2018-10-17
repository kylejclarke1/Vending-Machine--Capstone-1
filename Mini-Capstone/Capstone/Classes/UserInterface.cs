using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class UserInterface
    {
        private VendingMachine vendingMachine = new VendingMachine();

        public void RunInterface()
        {
            vendingMachine.ReadFile();
            bool done = false;
            while (!done)
            {
                done = RunMainMenu();
            }
        }

        public bool RunMainMenu()
        {
            Console.WriteLine("Please select option:");
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) End");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                DisplayItems();
                return false;
            }
            else if (choice == "2")
            {
                bool finished = false;
                while (!finished)
                {
                    finished = RunPurchaseMenu();
                }
                return false;
            }
            else if (choice == "3")
            {
                return true;
            }
            else if (choice == "9")
            {
                vendingMachine.SalesReport();
                return false;
            }
            else
            {
                Console.WriteLine("Pick a number >:(");
                return false;
            }
        }
        public bool RunPurchaseMenu()
        {
            Console.WriteLine("Please select option:");
            Console.WriteLine("(1) Feed Money");
            Console.WriteLine("(2) Select Product");
            Console.WriteLine("(3) Finish Transaction");
            Console.WriteLine($"Current Money Provided: ${vendingMachine.CurrentMoneyProvided}");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("What is the value of dollar bill you are entering?");
                decimal inputAmount = decimal.Parse(Console.ReadLine());
                if (vendingMachine.FeedMoney(inputAmount) == "Failure")
                {
                    Console.WriteLine("Enter a valid dollar bill amount!");
                }
                return false;
            }
            else if (choice == "2")
            {
                Console.WriteLine("Choose your product:");
                string productChoice = Console.ReadLine();
                string result = vendingMachine.SelectProduct(productChoice);
                if (result == "Sold Out")
                {
                    Console.WriteLine("SOLD OUT");
                }
                else if (result == "Insufficient Funds")
                {
                    Console.WriteLine("Insufficient funds, peon.");
                }
                else if (result == "Fake Item")
                {
                    Console.WriteLine("This item does not exist!");
                }
                else
                {
                    Console.WriteLine(result);
                }
                return false;
            }
            else if (choice == "3")
            {
                Console.WriteLine(vendingMachine.FinishTransaction());
                return true;
            }
            else
            {
                Console.WriteLine("Pick a number >:(");
                return false;
            }
        }

        public void DisplayItems()
        {
            foreach (VendingMachineItem item in vendingMachine.ItemsArray())
            {
                Console.WriteLine($"{item.SlotID} {item.Name} {item.Price} {item.Quantity}");
            }            
        }
    }
}

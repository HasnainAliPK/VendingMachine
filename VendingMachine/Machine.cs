using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    internal class Machine
    {
        private List<Item> Items;
        private decimal accumulatedMoney;
        private Timer timer;
        private bool moneyDeposited;
        private bool backToMainPage;

        public Machine()
        {
            Items = new List<Item>
            {
                new Item("Coca Cola", 15.0m, 10),
                new Item("Pepsi", 15.0m, 10),
                new Item("Faxe Kondi", 15.0m, 10),
                new Item("Fanta", 15.0m, 10),
                new Item("Sprite", 15.0m, 10),
                new Item("Gazoz", 15.0m, 10),
                new Item("Capri Sun", 10.0m, 10),
                new Item("Kildevand", 15.0m, 10),
                new Item("Dansk Vand", 15.0m, 10),

                new Item("Haribo", 15.0m, 10),
                new Item("Skumbananer", 15.0m, 10),
                new Item("Chokolade", 15.0m, 10),
                new Item("Corny Bar", 15.0m, 10),
                new Item("Chips", 15.0m, 10),



            };

            accumulatedMoney = 0;
            moneyDeposited = false;
            backToMainPage = false;
        }

        public void InsertMoney(decimal amount)
        {
            accumulatedMoney += amount;
            Console.Clear();
            Console.WriteLine($"You have inserted : {accumulatedMoney} kr.");
            StartTimer();
            moneyDeposited = true;
        }

        public void StartTimer()
        {
            timer = new Timer(TimeExpired, null, 30000, Timeout.Infinite);
        }

        private void TimeExpired(object state)
        {
            if (moneyDeposited)
            {
                Console.Clear();
                Console.WriteLine("You took too long to decide. Your money has returned");
                CancelPurchase();
                backToMainPage = true;
            }

        }

        // Method to display the available items in the vending machine
        public void ShowItems()
        {
            Console.Clear();
            Console.WriteLine("Available items:");

            // Loop through each item in the Items list
            for (int i = 0; i < Items.Count; i++)
            {
                // Print the item number (i + 1), name, price, and stock amount
                Console.WriteLine($"{i + 1}. {Items[i].Name} - Price: {Items[i].Price} kr. - In stock: {Items[i].StockAmount}");
            }
        }


        // Method to handle the purchase of an item based on user choice
        public void BuyItem(int choice)
        {
            // Check if the user's choice is valid (within the range of available items)
            if (choice < 1 || choice > Items.Count)
            {
                Console.Clear();
                Console.WriteLine("Invalid choice");
                return;
            }

            // Get the selected item based on the user's choice
            Item selectedItem = Items[choice - 1];

            // Check if the selected item is sold out
            if (selectedItem.StockAmount == 0)
            {
                Console.Clear();
                Console.WriteLine("This item is sold out"); // Notify the user that the item is out of stock
                return; // Exit the method if the item is not available
            }

            // Check if the user has enough money to buy the selected item
            if (accumulatedMoney >= selectedItem.Price)
            {
                selectedItem.StockAmount--; // Decrease the stock amount of the selected item by 1
                accumulatedMoney -= selectedItem.Price; // Deduct the item price from the accumulated money
                Console.Clear();
                Console.WriteLine($"You have bought {selectedItem.Name}. Remaining money: {accumulatedMoney} kr."); // Confirm the purchase
                StopTimer();

                // Check if there is any remaining money after the purchase
                if (accumulatedMoney > 0)
                {
                    Console.WriteLine($"You get {accumulatedMoney} kr. in return."); // Notify the user about the return money
                    accumulatedMoney = 0; // Reset accumulated money after returning the change
                }
            }
            else
            {
                // Notify the user that they do not have enough money for the selected item
                Console.Clear();
                Console.WriteLine($"You don't have enough money. {selectedItem.Name} costs {selectedItem.Price} kr., and you only have {accumulatedMoney} kr.");
            }
        }


        public void CancelPurchase()
        {
            Console.Clear();
            Console.WriteLine($"You get {accumulatedMoney} kr. in return.");
            accumulatedMoney = 0;
            moneyDeposited = false;
            StopTimer();
        }

        private void StopTimer()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }

        public bool BackToTheMainPage()
        {
            return backToMainPage;
        }

        public void ResetBackToFrontFlag()
        {
            backToMainPage = false;
        }
        public void AdminMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the administration menu:");
            Console.WriteLine("1. Restock items");
            Console.WriteLine("2. Remove money");
            Console.WriteLine("3. Adjust item prices");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RestockItems();
                    break;
                case "2":
                    RemoveMoney();
                    break;
                case "3":
                    AdjustPrice();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

        // Method to handle restocking of items in the vending machine
        private void RestockItems()
        {
            Console.Clear();
            Console.WriteLine("Which item do you want to restock?");
            ShowItems(); // Display the current list of items to the user

            // Read the user's choice for which item to restock
            int choice = int.Parse(Console.ReadLine());

            // Check if the user's choice is valid (within the range of available items)
            if (choice < 1 || choice > Items.Count)
            {
                Console.Clear();
                Console.WriteLine("Invalid choice"); // Notify the user of an invalid selection
                return; // Exit the method if the choice is invalid
            }

            Console.WriteLine("How many do you want to add?"); // Ask for the amount to restock
            int amount = int.Parse(Console.ReadLine()); // Read the amount to add from the user

            // Update the stock amount of the chosen item
            Items[choice - 1].StockAmount = amount;
            Console.Clear();
            Console.WriteLine($"{Items[choice - 1].Name} has now been restocked"); // Confirm restocking
        }


        private void RemoveMoney()
        {
            Console.Clear();
            Console.WriteLine($"{accumulatedMoney} kr. has been removed from the machine.");
            accumulatedMoney = 0;
        }

        // Method to adjust the price of a selected item in the vending machine
        private void AdjustPrice()
        {
            Console.Clear();
            Console.WriteLine("Which item's price would you like to adjust?"); // Prompt user to select which item's price to adjust
            ShowItems(); // Display the current list of items to the user

            // Read the user's choice for which item's price to adjust
            int choice = int.Parse(Console.ReadLine());

            // Check if the user's choice is valid (within the range of available items)
            if (choice < 1 || choice > Items.Count)
            {
                Console.Clear();
                Console.WriteLine("Invalid choice"); // Notify the user of an invalid selection
                return; // Exit the method if the choice is invalid
            }

            Console.WriteLine("Enter the new price:"); // Tells user to enter the new price
            decimal newPrice = decimal.Parse(Console.ReadLine()); // Read the new price from the user

            // Update the price of the chosen item
            Items[choice - 1].Price = newPrice;
            Console.Clear();
            Console.WriteLine($"The price for {Items[choice - 1].Name} is now {newPrice} kr."); // Confirm the price adjustment
        }

    }

}

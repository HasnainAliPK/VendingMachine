namespace VendingMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Machine machine = new Machine();
            bool start = true;

            while (start)
            {
                
                if (machine.BackToTheMainPage())
                {
                    machine.ResetBackToFrontFlag();
                    continue;
                }
                
                
                Console.Clear();
                Console.WriteLine("\nWelcome to the vending machine!");
                Console.WriteLine("1. Insert money");
                Console.WriteLine("2. Buy item");
                Console.WriteLine("3. Cancel purchase and get a refund");
                Console.WriteLine("4. Administration menu");
                Console.WriteLine("5. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Hvor mange penge vil du indkaste?");
                        decimal money = decimal.Parse(Console.ReadLine());
                        machine.InsertMoney(money);
                        break;

                    case "2":
                        machine.ShowItems();
                        Console.WriteLine("Hvilken vare vil du købe?");
                        int itemChoice = int.Parse(Console.ReadLine());
                        machine.BuyItem(itemChoice);
                        break;

                    case "3":
                        machine.CancelPurchase();
                        break;

                    case "4":
                        machine.AdminMenu();
                        break;

                    case "5":
                        start = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice");                 
                        break;
                }
            }
        }
    }
}

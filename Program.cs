using System;

namespace FileAcessTask
{

    class Program
    {

        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            List<Item> items = Load();
            int slctn = 0;
            while (slctn != 6)
            {
                Console.WriteLine("1. Add New Item");
                Console.WriteLine("2. List All Items");
                Console.WriteLine("3. Show Total Cost");
                Console.WriteLine("4. Clear List");
                Console.WriteLine("5. Save List");
                Console.WriteLine("6. Exit");
                Console.Write("Select: ");
                string? input = Console.ReadLine();
                slctn = int.Parse(input);
                if (slctn == 1)
                {
                    Console.WriteLine("Add New Item");
                    items.Add(AddItem());
                }
                else if (slctn == 2)
                {
                    Console.WriteLine("Items are");
                    foreach (Item item in items)
                        Console.WriteLine("Title: " + item.title + "\nQuantity: " + item.quantity + "\nPrice: " + item.price);
                }
                else if (slctn == 3) Console.WriteLine("Total Cost: " + TotalCost(items));
                else if (slctn == 4)
                {
                    items.Clear();
                    Console.WriteLine("List Cleared");
                }
                else if (slctn == 5)
                {
                    Save(items);
                    Console.WriteLine("Saved List");
                }
                else if (slctn == 6) Console.WriteLine("Thanks for using the Shopping List App");
                else
                {
                    Console.WriteLine("Incorrect selection");
                    Menu();
                }
            }
        }

        public static void Save(List<Item> items)
        {
            using (StreamWriter writer = new StreamWriter("./data.csv"))
            {
                foreach (var t in items)
                {
                    writer.WriteLine(t.title + "," + t.quantity + "," + t.price);
                }
            }
        }

        public static List<Item> Load()
        {
            List<Item> items = new List<Item>();
            if (File.Exists("./data.csv"))
            {
                string[] lines = File.ReadAllLines("./data.csv");
                foreach (string line in lines)
                {
                    string[] values = line.Split(",");
                    items.Add(new Item(values[0], int.Parse(values[1]), double.Parse(values[2])));
                }
            }
            return items;
        }

        public static Item AddItem()
        {
            Console.Write("Title of item: ");
            string? name = Console.ReadLine();
            Console.Write("Quantity of items: ");
            string? q = Console.ReadLine();
            int quan = int.Parse(q);
            Console.Write("Price of item: ");
            string? c = Console.ReadLine();
            double cost = double.Parse(c);
            Item item = new Item(name, quan, cost);
            return item;
        }

        public static double TotalCost(List<Item> items)
        {
            double total = 0;
            foreach (Item item in items) total += item.price * item.quantity;
            return total;
        }


    }

    public class Item
    {
        public string title { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public Item(string title, int quantity, double price)
        {
            this.title = title;
            this.quantity = quantity;
            this.price = price;
        }
    }
}

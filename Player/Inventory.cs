public class Inventory
{
    public  Player Player;
    public Item Item;

    public Inventory(Player player, Item item)
    {
        Player = player;
        Item = item;
    }

    private List<Item> items = new List<Item>();
    private Item? equippedWeapon = null;
    private Player? owner;

    public Inventory(Player? player = null)
    {
        owner = player;
    }

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
        Console.WriteLine("You Equipped Thors his old swords");
    }

    public void ShowInventory()
    {
        Console.WriteLine("\n========== INVENTORY ==========");
        if (items.Count == 0)
        {
            Console.WriteLine("( Empty )");
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                string equipStatus = (items[i] == equippedWeapon) ? "[EQUIPPED]" : "";
                Console.WriteLine($"{i + 1}. {items[i].Name} {equipStatus}");
            }
        }
        Console.WriteLine("===============================");
    }

    public Item? GetEquippedWeapon()
    {
        return equippedWeapon;
    }

    // Voor shop
    public int Count => items.Count;

    public Item? GetItem(int index)
    {
        int actual = index - 1;
        if (actual >= 0 && actual < items.Count)
            return items[actual];
        return null;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public void Open()
    {
        bool browsing = true;

        while (browsing)
        {
            Console.Clear();
            ShowInventory();
            Console.WriteLine("\nOptions: [V] View  [E] Equip [X] Exit");
            Console.Write("> ");

            string input = Console.ReadLine()?.ToUpper();

            if (input == "X")
            {
                browsing = false;
            }
            else if (input == "V" || input == "E" || input == "U")
            {
                Console.Write("Enter Item Number: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (input == "E") SelectWeapon(choice);
                    if (input == "U") UseItem(choice);
                }
                else
                {
                    Console.WriteLine("Invalid number.");
                }

                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
            }
        }
    }

    public void SelectWeapon(int index)
    {
        int actualIndex = index - 1;
        if (actualIndex >= 0 && actualIndex < items.Count)
        {
            if (items[actualIndex].IsWeapon)
            {
                equippedWeapon = items[actualIndex];
                Console.WriteLine($"\nYou have equipped: {equippedWeapon.Name}");
            }
            else
            {
                Console.WriteLine($"\n{items[actualIndex].Name} cannot be equipped as a weapon.");
            }
        }
    }

    public void UseItem(int index)
    {
        if (owner == null)
            return;

        int actualIndex = index - 1;
        if (actualIndex >= 0 && actualIndex < items.Count)
        {
            bool consumed = items[actualIndex].Use(owner);
            if (consumed)
                items.RemoveAt(actualIndex);
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }
}
public class Inventory
{
    public  Player Player;
    public Item Item;

    private List<Item> items = new List<Item>();
    private Item? equippedWeapon = null;

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
        //Console.WriteLine($"You received: {newItem.Name}");
    }

    public bool PlayerHasItemInInventory(string item)
    {
        return items.Any(i => i.Name == item);
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
            else if (input == "V" || input == "E")
            {
                Console.Write("Enter Item Number: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (input == "V") ViewItemDetails(choice);
                    if (input == "E") SelectWeapon(choice);
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

    public void ViewItemDetails(int index)
    {
        int actualIndex = index - 1;
        if (actualIndex >= 0 && actualIndex < items.Count)
        {
            Item item = items[actualIndex];
            Console.WriteLine($"\n--- {item.Name} ---");
            Console.WriteLine($"Description: {item.Description}");
            Console.WriteLine($"Damage: {item.Damage}");
            Console.WriteLine($"Type: {(item.IsWeapon ? "Weapon" : "Utility")}");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }
}
public class Inventory
{
    public Player Player;
    private List<Item> items = new List<Item>();
    private Item? equippedItem = null;
    private Weapon? equippedWeapon = null;

    public Inventory(Player player)
    {
        Player = player;
    }

    public void AddItem(Player player, Item newItem)
    {
        items.Add(newItem);
        if (equippedWeapon == null && newItem.IsWeapon)
        {
            equippedWeapon = new Weapon(newItem);
            player.weapon = equippedWeapon;
        }
    }

    public bool PlayerHasItemInInventory(string item)
    {
        return items.Any(i => i.Name == item);
    }

    public void ShowInventory()
    {
        Console.WriteLine("\n========== INVENTORY ==========");
        string equipStatus = "";
        if (items.Count == 0)
        {
            Console.WriteLine("( Empty )");
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].IsWeapon)
                {
                    equipStatus = (equippedWeapon != null && items[i] == equippedWeapon.Item) ? "[EQUIPPED]" : "";
                }
                else
                {
                    equipStatus = (items[i] == equippedItem) ? "[EQUIPPED]" : "";
                }
                Console.WriteLine($"{i + 1}. {items[i].Name} {equipStatus}");
            }
        }
        Console.WriteLine("===============================");
    }

    public Weapon? GetEquippedWeapon()
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
                equippedWeapon = new Weapon(items[actualIndex]);
                Console.WriteLine($"\nYou have equipped: {equippedWeapon.Name}");
                Player.weapon = equippedWeapon;
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
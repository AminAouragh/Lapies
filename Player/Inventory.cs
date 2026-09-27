public class Inventory
{
    public Player Player;
    public List<Item> items = new List<Item>();
    private Item? equippedItem = null;
    private Weapon? equippedWeapon = null;

    public Inventory(Player player)
    {
        Player = player;
    }

    public void AddItem(Player player, Item newItem)
    {
        if (equippedWeapon == null && newItem.IsWeapon)
        {
            equippedWeapon = new Weapon(newItem);
            player.weapon = equippedWeapon;
        }
        items.Add(newItem);
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine($"\n{player.Name.ToUpper()} HAS RECEIVED: {newItem.Name}");
        Console.ResetColor();
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
                return;
            }

            try
            {
                if (input == "V" || input == "E")
                {
                    Console.Write("Enter Item Number: ");
                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        if (input == "V") ViewItemDetails(choice);
                        if (input == "E" && items[choice - 1].IsWeapon) SelectWeapon(choice);
                        if (input == "E" && items[choice - 1].IsHealing) SelectHealing(choice);
                    }
                    else
                    {
                        Console.WriteLine("Invalid number.");
                    }
                }
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Nothing here, it's empty");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Nothing here, it's empty");
            }
            finally
            {
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
            }

        }
    }

    public void SelectHealing(int index)
    {
        int actualIndex = index - 1;
        string choice = "";
        if (actualIndex >= 0 && actualIndex < items.Count)
        {
            if (items[actualIndex].IsHealing)
            {
                do
                {
                    Console.WriteLine($"\n{Player.Name}: {Player.HP}hp");
                    Console.WriteLine("Would you like to heal? (Y/N)");
                    choice = Console.ReadLine();
                }
                while (choice.ToLower() != "y" && choice.ToLower() != "n");
                if (choice.ToLower() == "y")
                {
                    Player.HP = Math.Min(Player.HP + items[actualIndex].Heal, 100);
                    RemoveItem(items[actualIndex]);
                    Console.WriteLine($"\nYou're now at {Player.HP}hp");
                }
                else if (choice.ToLower() == "n")
                {
                    return;
                }
            }
            else
            {
                Console.WriteLine($"\n{items[actualIndex].Name} cannot be selected as healing item.");
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
            Console.WriteLine($"Healing: {item.Heal}");
            Console.WriteLine($"Type: {(item.IsWeapon ? "Weapon" : item.IsHealing ? "Healing Item" : "")}");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }
}
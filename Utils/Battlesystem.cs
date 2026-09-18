public static class Battlesystem
{
    private static readonly Random RandomGenerator = new();
    private const int CriticalHitChance = 20;
    private const int canRunInt = 20;

    public static void StartBattle(Player player, Monster monster)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"----- {player.Name} VS {monster.Name} -----");
        Console.ResetColor();

        while (player.HP > 0 && monster.HP > 0)
        {
            Console.WriteLine(player.Name + ": " + player.HP + " HP");
            Console.WriteLine(monster.Name + ": " + monster.HP + " HP");

            Console.WriteLine("Press A to attack or R to try to run.\n");
            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.R)
            {
                bool canRun = RandomGenerator.Next(18, 21) == canRunInt;
                if (canRun)
                {
                    Console.WriteLine("You ran away.");
                    Console.ReadKey(true);
                    return;
                }
                else
                {
                    Console.WriteLine("You couldn't escape.");
                    Thread.Sleep(500);
                    continue;
                }
            }

            if (key != ConsoleKey.A)
            {
                continue;
            }

            bool criticalHit = RandomGenerator.Next(100) < CriticalHitChance;
            int damage = player.weapon.Damage;

            if (criticalHit)
            {
                damage *= 2;
            }

            monster.TakeDamage(damage);

            if (criticalHit)
            {
                Console.WriteLine($"Critical hit! You dealt {damage} damage!");
            }
            else
            {
                Console.WriteLine($"You dealt {damage} damage!");
            }

            if (monster.HP <= 0)
            {
                player.EarnXP(100);
                player.MonstersDefeated++;
                Console.WriteLine("You won and earned 100 XP!");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("The monster attacks!");

            player.TakeDamage(monster.Damage);
        }

        if (player.IsDead())
        {
            Console.WriteLine("You lost, try again!");
            Console.ReadKey(true);
        }
    }

    public static void StartBattleNPC(Player player, NPC npc)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"----- {player.Name} VS {npc.Name} -----");
        Console.ResetColor();
        ConsoleKey key;

        do
        {
            Console.WriteLine($"{player.Name} HP: {player.HP}");
            Console.WriteLine($"{npc.Name} HP: {npc.HP}");

            Console.WriteLine("\nPress [A] to attack");
            key = Console.ReadKey(true).Key;
            int damageNPC = 0;
            int damagePlayer = player.weapon.DealDamage(player.weapon.Damage);
            bool criticalHit = RandomGenerator.Next(100) < CriticalHitChance;
            bool blocked = RandomGenerator.Next(15, 25) == CriticalHitChance; // 1 op 10 vgm

            if (key == ConsoleKey.A)
            {
                if (criticalHit)
                {
                    damagePlayer *= 2;
                }
                npc.TakeDamage(damagePlayer);
            }

            if (npc.IsEnemy)
            {
                if (npc.Name == "Askeladd")
                {
                    damageNPC += npc.DealDamage(10, 50);
                    player.TakeDamage(damageNPC);
                }
                else if (npc.Name == "Thorkell")
                {
                    Console.WriteLine("Press [D] to block the opponents attack\n");
                    ConsoleKey blockKey = Console.ReadKey(true).Key;
                    damageNPC += npc.DealDamage(15, 20);
                    if (blockKey == ConsoleKey.D && blocked)
                    {
                        Console.WriteLine("You blocked the attack!");
                    }
                    else
                    {
                        player.TakeDamage(damageNPC);
                    }
                }
                else
                {
                    player.TakeDamage(damageNPC);
                }
            }


        }while (player.IsAlive() && npc.HP > 0);


        string won = player.IsAlive()? $"\n{player.Name} WON!" : $"\n{npc.Name} WON!";
        Console.WriteLine(won);
        Thread.Sleep(1000);
        return;
    }
}

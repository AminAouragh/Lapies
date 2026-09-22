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
                bool canRun = RandomGenerator.Next(15, 21) == canRunInt;
                if (canRun)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You ran away.");
                    Console.ResetColor();
                    Thread.Sleep(500);
                    return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You couldn't escape.");
                    Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"Critical hit! You dealt {damage} damage!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You dealt {damage} damage!");
                Console.ResetColor();
            }

            if (monster.HP <= 0)
            {
                player.EarnXP(100);
                player.MonstersDefeated++;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("You won and earned 100 XP!");
                Console.ResetColor();
                Thread.Sleep(1000);
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{monster.Name} attacks and did {monster.Damage} damage!\n");
            Console.ResetColor();

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
            Console.WriteLine("Press [A] to attack");
            key = Console.ReadKey(true).Key;

            if (key != ConsoleKey.A)
            {
                continue;
            }

            int damageNPC = 0;
            int damagePlayer = player.weapon.DealDamage(player.weapon.Damage);
            bool criticalHit = RandomGenerator.Next(100) < CriticalHitChance;
            bool blocked = RandomGenerator.Next(15, 20) == CriticalHitChance; // 1 op 10 vgm

            if (key == ConsoleKey.A)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nYou dealt {damagePlayer} damage!");
                Console.ResetColor();
                if (criticalHit)
                {
                    damagePlayer *= 2;
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine($"Critical hit! You dealt {damagePlayer} damage!");
                    Console.ResetColor();
                }
                npc.TakeDamage(damagePlayer);
            }

            if (npc.IsEnemy)
            {
                if (npc.Name == "Askeladd")
                {
                    damageNPC += npc.DealDamage(10, 50);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{npc.Name} attacked and dealt {damageNPC} damage!\n");
                    Console.ResetColor();
                    player.TakeDamage(damageNPC);
                }
                else if (npc.Name == "Thorkell")
                {
                    Console.WriteLine("Press [D] to block the opponents attack\n");
                    ConsoleKey blockKey = Console.ReadKey(true).Key;
                    damageNPC += npc.DealDamage(15, 20);
                    if (blockKey == ConsoleKey.D && blocked)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nYou blocked the attack!\n");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{npc.Name} attacked and dealt {damageNPC} damage!\n");
                        Console.ResetColor();
                        player.TakeDamage(damageNPC);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{npc.Name} attacked and dealt {damageNPC} damage!\n");
                    Console.ResetColor();
                    player.TakeDamage(damageNPC);
                }
            }

        } while (player.IsAlive() && npc.HP > 0);


        string won = player.IsAlive()? $"\n{player.Name} WON!" : $"\n{npc.Name} WON!";
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(won);
        Console.ResetColor();
        Thread.Sleep(3000);
        return;
    }
}

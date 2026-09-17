public static class Battlesystem
{
    private static readonly Random RandomGenerator = new();
    private const int CriticalHitChance = 20;

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

            Console.WriteLine("Press A to attack or R to run.");
            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.R)
            {
                Console.WriteLine("You ran away.");
                Console.ReadKey(true);
                return;
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
                player.EnemiesDefeated++;
                Console.WriteLine("You won and earned 100 XP!");
                Console.ReadKey(true);
                return;
            }

            Console.WriteLine("The monster attacks!");

            player.TakeDamage(monster.Damage);
        }

        if (player.IsDead())
        {
            Console.WriteLine("You lost!");
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

            Console.WriteLine("\nPress [a] to attack\n");
            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.A)
            {
                int damagePlayer = player.weapon.DealDamage(5, 15);
                npc.TakeDamage(damagePlayer);

                if (npc.IsEnemy)
                {
                    int damageNPC = npc.DealDamage(10, 50);
                    player.TakeDamage(damageNPC);
                }
            }

        }while (player.IsAlive() && npc.HP > 0);


        string won = player.IsAlive()? $"{player.Name} WON!" : $"{npc.Name} WON!";
        Console.WriteLine(won);
        Console.ReadKey(true);
        return;
    }
}

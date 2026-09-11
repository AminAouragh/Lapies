public static class Scenes
{

    static readonly Dictionary<string, ConsoleColor> speakerColors = new()
    {
        { "THORS (Father)", ConsoleColor.White },
        { "ASKELADD", ConsoleColor.DarkRed },
        { "THORKELL", ConsoleColor.DarkYellow },
        { "CANUTE", ConsoleColor.DarkCyan },
        { "LEIF", ConsoleColor.Green },
        { $"PLAYER", ConsoleColor.Cyan }
    };

    public static string PrintLine(Player player, string line)
    {
        string continueOn = "";
        if (line.StartsWith("["))
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(line);
            Console.ResetColor();
            return null;
        }
        int index = line.IndexOf(':');
        string speaker = index == -1 ? "" : line.Substring(0, index).Trim(); //pakt gwn de naam alleen als er ":" is, zo niet, skot line

        if (speakerColors.TryGetValue(speaker, out ConsoleColor color)) // kleurtje voor de naam
        {
            if (speaker == "PLAYER")
            {
                speaker = player.Name;
            }
            Console.ForegroundColor = color;
            Console.Write(speaker);
            Console.ResetColor();
            Console.WriteLine(line.Substring(index));
        }
        else
        {
            continueOn = line;
            Console.WriteLine(line);
        }
        return continueOn;
    }

    public static void PlayScene(Player player, string[] lines)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("After each line press [Enter] to show the next line");
        Console.WriteLine("Or press [Spacebar] to skip to the end of this scene\n");
        Console.ResetColor();
        foreach (string line in lines)
        {
            PrintLine(player, line);

            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;

            } while (key != ConsoleKey.Enter && key != ConsoleKey.Spacebar);

            if (key == ConsoleKey.Spacebar)
            {
                int index = Array.IndexOf(lines, line);
                for (int i = index + 1; i < lines.Length; i++)
                {
                    PrintLine(player, lines[i]);
                }
                Console.ReadKey(true);
                break;
            }
            }
    }

    public static void Intro(Player player)
    {
        Console.Clear();
        string[] lines =
        {
            "THORS (Father):  Goodmorning son, the sea's flat this morning. Come walk down to the water with me.",
            "THORS (Father):  There's something I'd rather say where your mother can't hear it.",
            "THORS (Father):  Go on ahead. Ocean. I'll catch you up."
        };

        PlayScene(player, lines);
    }

    public static void OceanBattle(Player player)
    {
        Console.Clear();
        string[] lines =
        {
            "[Thirty men on the shingle. Ships behind them. They were waiting.]",
            "ASKELADD:  Thors Snorresson. The Troll of Jom.",
            "ASKELADD:  Eleven years playing farmer, and not one of you thought somebody might eventually come looking.",
            "THORS (Father):     That's a lot of men for one farmer.",
            "ASKELADD:  Men are cheap. Your name isn't.",
            "THORS (Father):     Then let's keep it cheap. One fight. You and me, no blades drawn on anyone else.",
            "THORS (Father):     When it's finished your ships leave, the village stands, and my son walks home.",
            "ASKELADD:  And when you lose?",
            "THORS (Father):     Then you've still agreed to the terms.",
            "[He wins. It takes almost no time at all.]",
            "[Askeladd's sword is in the surf. Thors does not pick it up.]",
            "ASKELADD:  Finish it. That's what the thing is for.",
            "THORS (Father):     A sword is what's left when a man's run out of better ideas.",
            "THORS (Father):     I ran out for a long time. I'm not going back to it.",
            "[The archers do not need an order. Two arrows. Then a third.]",
            "ASKELADD:  ...I didn't call for that."
        };

        PlayScene(player, lines);
    }

    public static void ThorsDeath(Player player)
    {
        string[] lines =
        {
            $"THORS (Father):     {player.Name}. Don't look away. Look at me.",
            "THORS (Father):     You're going to want to pick up a sword after this. I know. I did.",
            "THORS (Father):     The man standing in front of you is never really your enemy.",
            "THORS (Father):     There was never anyone it would have been alright to cut down. Not once.",
            "THORS (Father):     ...You don't need one. Nobody does. Work out what that means.",

            "[He doesn't say anything else.]",

            $"PLAYER:    Dad?",
            $"PLAYER:    Dad. Get up.",
            $"PLAYER:    DAAAAAAAD!",

            $"PLAYER:    I'll kill him.",
            $"PLAYER:    I don't care how long it takes. I'll kill him myself.",

            "[The shore tilts. Then the sky. Then nothing.]"
        };

        PlayScene(player, lines);
    }

    public static void PlayerNPCBattle(Player player)
    {
        Console.Clear();
        string[] lines =
        {
            $"{player.Name} opens his eyes and is in awe.\nSeveral days have passed, they have been trailing the raiders inland",
            "[Six days inland. You have not eaten in two of them. You see a big sword and try to lift it..]",
            "ASKELADD:  Still breathing? You've been behind us since the coast, boy. My men had a wager on when you'd drop.",
            "PLAYER:    Draw your sword",
            "ASKELADD:  You're eleven years old.",
            "PLAYER:    DRAW IT.",
            "ASKELADD:  ...Alright. Come on then."
        };
        PlayScene(player, lines);
    }

    public static void PlayerLossNPC(Player player)
    {
        string[] lines =
        {
            "[You do not get close. You do not even make him step back.]",
            "ASKELADD:  There it is. That's the gap between wanting to kill a man, and being able to.",
            "ASKELADD:  Nobody's closing that for you. Come back when you have.",
            "[He walks off. He doesn't look back, and somehow that's the worst part.]",
            "[You pass out...]"
        };

        PlayScene(player, lines);
    }

    public static void LeifIntro(Player player)
    {
        Console.Clear();
        string[] lines =
        {
            "[You hear something echo'ing and open your eyes slowly after passing out]",
            $"LEIF:      Gods above — {player.Name}? {player.Name}!",
            "LEIF:      Six days I've been walking these woods. Six.",
            "LEIF:      Your mother hasn't slept since the shore. Your sister asks about you every single morning\nand I've run out of lies.",
            "PLAYER:    I'm not going back.",
            "LEIF:      You're bleeding through your shirt and you weigh less than my anchor rope.",
            "[You're still frustated and in anger so you don't speak]",
            "LEIF:      You're going as far as the town, and you're eating something, and then you can argue with me.",
            "LEIF:      ...Stay close. The wolves have been bold this winter and I'm no fighter."
        };
        PlayScene(player, lines);
    }
}
public static class Scenes
{
    static readonly Dictionary<string, ConsoleColor> speakerColors = new()
    {
        { "THORS", ConsoleColor.White },
        { "ASKELADD", ConsoleColor.DarkRed },
        { "THORKELL", ConsoleColor.DarkYellow },
        { "CANUTE", ConsoleColor.DarkCyan },
        { "LEIF", ConsoleColor.Green },
        { "PLAYER", ConsoleColor.Cyan }
    };

    public static void PrintLine(string line)
    {
        if (line.StartsWith("["))
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(line);
            Console.ResetColor();
            return;
        }

        int colonIndex = line.IndexOf(':');
        string speaker = colonIndex == -1 ? "" : line.Substring(0, colonIndex).Trim();

        if (speakerColors.TryGetValue(speaker, out ConsoleColor color))
        {
            Console.ForegroundColor = color;
            Console.Write(speaker);
            Console.ResetColor();
            Console.WriteLine(line.Substring(colonIndex));
        }
        else
        {
            Console.WriteLine(line);
        }
    }

    public static void PlayScene(string[] lines)
    {
        foreach (string line in lines)
        {
            Console.Clear();
            PrintLine(line);

            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
            }
            while (key != ConsoleKey.Enter);
        }
    }

    public static void Scene1(Player player)
    {
        string[] lines =
        {
            "THORS:  The sea's flat this morning. Come walk down to the water with me.",
            "THORS:  There's something I'd rather say where your mother can't hear it.",
            "THORS:  Go on ahead. Ocean. I'll catch you up."
        };

        PlayScene(lines);
    }

    public static void Scene2(Player player)
    {
        string[] lines =
        {
            "[Thirty men on the shingle. Ships behind them. They were waiting.]",
            "ASKELADD:  Thors Snorresson. The Troll of Jom.",
            "ASKELADD:  Eleven years playing farmer, and not one of you thought somebody might eventually come looking.",
            "THORS:     That's a lot of men for one farmer.",
            "ASKELADD:  Men are cheap. Your name isn't.",
            "THORS:     Then let's keep it cheap. One fight. You and me, no blades drawn on anyone else.",
            "THORS:     When it's finished your ships leave, the village stands, and my son walks home.",
            "ASKELADD:  And when you lose?",
            "THORS:     Then you've still agreed to the terms.",
            "[He wins. It takes almost no time at all.]",
            "[Askeladd's sword is in the surf. Thors does not pick it up.]",
            "ASKELADD:  Finish it. That's what the thing is for.",
            "THORS:     A sword is what's left when a man's run out of better ideas.",
            "THORS:     I ran out for a long time. I'm not going back to it.",
            "[The archers do not need an order. Two arrows. Then a third.]",
            "ASKELADD:  ...I didn't call for that."
        };

        PlayScene(lines);
    }
}
using System.Media;

public static class Scenes
{
    //all context stories (audio)
    public static SoundPlayer thorsIntro = new SoundPlayer("Utils/Audio/ThorsIntro.wav");
    public static SoundPlayer oceanBattle = new SoundPlayer("Utils/Audio/OceanBattle.wav");
    public static SoundPlayer oceanBattleMusic = new SoundPlayer("Utils/Audio/oceanBattleMusic.wav");
    public static SoundPlayer arrowsAndmusic = new SoundPlayer("Utils/Audio/ArrowsAndMusic.wav");

    static readonly Dictionary<string, ConsoleColor> speakerColors = new()
    {
        { "THORS (Father)", ConsoleColor.White },
        { "ASKELADD", ConsoleColor.DarkRed },
        { "THORKELL", ConsoleColor.DarkYellow },
        { "CANUTE", ConsoleColor.DarkCyan },
        { "LEIF", ConsoleColor.Green },
        { $"PLAYER", ConsoleColor.Cyan }
    };

    public static string PreparePrintLine(Player player, string line)
    {
        if (line.StartsWith("["))
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            return line;
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
            return line.Substring(index);
        }
        return line;
    }

    public static void PrintLine(Player player, string line)
    {
        string rest = PreparePrintLine(player, line);
        Console.WriteLine(rest);
        Console.ResetColor();
    }


    public static void PlayScene(Player player, string[] lines)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Press [Enter] to show the next line");
        Console.WriteLine("Press [Spacebar] to skip to the end of this scene\n");
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

    public static void TypeLine(string text, int delay)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
        Console.ResetColor();
        Console.WriteLine();
}

    public static void PlayIntro()
    {
        Console.Clear();
        Console.WriteLine("A small introduction to the game, open your ears and listen");
        thorsIntro.PlaySync();
        World.BlockInput();
    }

    public static void PlayOceanBattleContext()
    {
        Console.Clear();
        Console.WriteLine("A little context before entering the Ocean area, open your ears and listen");
        oceanBattle.PlaySync();
        World.BlockInput();
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
        oceanBattleMusic.Play();
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
            "THORS (Father):     Then you've still agreed to the terms.\n",
            "[BATTLING...]\n",
        };
        PlayScene(player, lines);
        oceanBattleMusic.Stop();
        ArrowsAndMusic(player);
    }

    public static void ArrowsAndMusic(Player player)
    {
        arrowsAndmusic.Play();
        World.BlockInput();
        string[] lines =
        {
            "[THE WINNER OF THIS DUAL...]",
            "[is Thors the Troll]",
            "[The archers do not need an order. One Arrow. Then two..]",
            "THORS (Father): Aughh....",
            "PLAYER: FATHERRR!!!",
            "PLAYER: father no..",
            // "[Askeladd's sword is in the surf. Thors does not pick it up.]",
            // "ASKELADD:  Finish it. That's what the thing is for.",
            "THORS (Father): Askeladd.. I bested u in our duel..",
            "THORS (Father): Don't cross the promise of a warrior",
            // "THORS (Father):     I ran out for a long time. I'm not going back to it.",
            "ASKELADD:  ...I wouldn't dare",
            "[Askeladd didn't call for the arrows..]",
        };
        foreach (string line in lines)
        {
            string lineLeft = PreparePrintLine(player, line);
            if (line == "PLAYER: FATHERRR!!!" || line == "PLAYER: father no..")
            {
                TypeLine(lineLeft, 800);
            }
            else if (line == "THORS (Father): Aughh....")
            {
                TypeLine(lineLeft, 400);
            }
            else if (line == "[The archers do not need an order. One Arrow. Then two..]")
            {
                TypeLine(lineLeft, 130);
            }
            else if (line == "THORS (Father): Don't cross the promise of a warrior" || line == "ASKELADD:  ...I wouldn't dare")
            {
                TypeLine(lineLeft, 120);
            }
            else
            {
                TypeLine(lineLeft, 130);
            }
        }
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
        Item rustySword = new Item("Sword", "Rusty sword", 5, true, 50);
        player.inventory.AddItem(player, rustySword);
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine($"\n{player.Name.ToUpper()} HAS RECEIVED: {rustySword.Name}");
        Console.ResetColor();
        Thread.Sleep(1500);
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

    public static void After_quest1_story1(Player player)
    {
        Console.Clear();
        string[] lines =
        {
            $"LEIF:      You really got stronger huh, {player.Name}",
            "LEIF:      Sit down. There's something of your father's I've been carrying, longer than you've been alive.",
            "[A small chest. Older than it looks. Inside: throwing knives, wrapped in oiled cloth.]",
            "LEIF:      He handed me these before you were born. Said he'd no use for them.",
            "LEIF:      Said that if he ever came asking for them back, I was to refuse him.",
            "PLAYER:    ...Did he ever ask?",
            "LEIF:      Not once. Eleven years, not once.",
            "PLAYER:    Then he won't mind me taking them.",
            $"LEIF:      {player.Name}—",
            "PLAYER:    He wasn't beaten, Leif. He was shot. There's a difference and everyone on that beach knows it."
        };
        PlayScene(player,lines);
        Item thorfinnsBlades = new("Thorfinns Dual Daggers", "Dad's Legacy: short, fast and lethal", 25, true, 0);
        player.inventory.AddItem(player, thorfinnsBlades);
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine($"\n{player.Name.ToUpper()} HAS RECEIVED: {thorfinnsBlades.Name}");
        Console.ResetColor();
        Thread.Sleep(1600);
    }

    public static void Story_your_mother_and_sister(Player player)
    {
        Console.Clear();
        string [] lines =
        {
            "[Your mother doesn't shout. Somehow that's worse.]",
            "[Your sister just holds onto your sleeve and won't let go of it.]",
            "[Mother has fed you and you recovered and are ready to go..]",
            "[But they aren't..]",

            "PLAYER:    I'm going after him.",
            "PLAYER:    He didn't beat father. He had archers in the treeline the whole time.",
            "PLAYER:    That wasn't a duel, it was a trap dressed up as one.",
            "PLAYER:    A duel means something. He took that from us as well.",
            "PLAYER:    I'll find him and I'll do it properly. Face to face, where people can see.",
            "PLAYER:    ...Tell her I'm coming back.",
            "LEIF:      Tell her yourself. Afterwards.",
            "LEIF:      There's a ship at dawn. Iceland first — that's where the news goes",
            "before it goes anywhere else."
        };
        PlayScene(player,lines);
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("*QUEST INCOMING* IN THE FAR DISTANCE YOU SEE A BIG LAUGHING MAN\nSTANDING IN YOU'RE WAY, TRY TO DEFEAT HIM");
        Thread.Sleep(1800);
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Player tip: Equip your strongest weapon");
        Console.ReadKey(true);
        Console.ResetColor();
    }

    public static void Story_Thorkell(Player player)
    {
    Console.Clear();
    string [] lines =
        {
            "[The biggest man you have ever seen is laughing at something that isn't funny.]",

            "THORKELL:  HA! Look at this one!",
            "THORKELL:  Half the size of my men and he's the only one who didn't step back.",
            "PLAYER:    I'm looking for Askeladd.",
            "THORKELL:  Everyone's looking for Askeladd. Slippery little Welsh liar, never where he says he'll be.",
            "THORKELL:  Tell you what, small one. You're going to die out there regardless.",
            "THORKELL:  So give me a proper fight first. If you're upright afterwards, I'll tell you exactly where he's sailing.",
            "PLAYER:    And if I'm not upright?",
            "THORKELL:  Then you were never getting near him anyway and I've saved you the trip!",
            "THORKELL:  Either way I win! COME ON!"
        };
        PlayScene(player,lines);
    }

    public static void Story_after_quest2(Player player)
    {
        Console.Clear();
        string [] lines =
        {
            "THORKELL:  ...HAHAHA! GOOD! That was GOOD!",
            "THORKELL:  You've got your father's footwork. Did you know that?",
            "I fought him once. Lost. Best day of my life.",
            "THORKELL:  Askeladd's sailing with the prince now. Canute. Pale little thing,",
            "prays more than he speaks.",
            "THORKELL:  Find the king, you'll find the prince. Find the prince,",
            "you'll find your Welshman.",
            "THORKELL:  And boy — the king's a dead man. He just hasn't been told."
        };
        PlayScene(player,lines);
    }

    public static void Story_the_throne(Player player)
    {
        Console.Clear();
        string [] lines =
        {
            "CANUTE:    You're not one of my father's men.",
            "PLAYER:    No.",
            "CANUTE:    Good. My father's men are the ones I'm frightened of.",
            "CANUTE:    Stay near me tonight. Please. There's no one else I can ask,",
            "and asking is all I have.",
        };
        PlayScene(player,lines);
    }

    public static void Story_after_quest3(Player player)
    {
        Console.Clear();
        string [] lines =
        {
        "[The hall doors open. You know the shape before you see the face.]",

        "ASKELADD:  Hello again. You got taller.",

        "[He doesn't come for you. He walks straight past — toward the throne.]",

        "ASKELADD:  Apologies, Your Majesty. Nothing personal in it.",
        "ASKELADD:  This one's for Wales.",

        "[The king falls. The guards are already moving.]",

        "ASKELADD:  CANUTE! BE A KING!",
        "ASKELADD:  BE A BETTER ONE THAN HE WAS!",

        "[Twenty blades. It takes far less time than you spent imagining it.]",
        };
        PlayScene(player,lines);
    }


    public static void Story_the_last_conversation(Player player)
    {
        Console.Clear();
        string [] lines =
        {
            "PLAYER:    No. No — get up. GET UP.",
            "PLAYER:    That was mine. That death was MINE.",
            "ASKELADD:  ...heh. Sorry, kid.",
            "PLAYER:    Eleven years. I followed you for eleven years.",
            "ASKELADD:  I know. I let you.",
            "PLAYER:    ...Why?",
            "ASKELADD:  Because your father asked me something on that beach, and I never came up with an answer.",
            "ASKELADD:  He said a real warrior's got no need of a sword.",
            "ASKELADD:  I've carried that around longer than you've carried me.",
            "ASKELADD:  Go and find out what he meant.",
            "ASKELADD:  I never managed it.",

            "[He's gone. You are still holding your father's knives.]",
            "[They have never felt heavier.]"
        };
        PlayScene(player,lines);
    }

    public static void Story_after(Player player)
    {
        Console.Clear();
        string [] lines =
        {
        "[The water here is warm. There are no walls and nobody is counting the dead.]",
        "[You keep reaching for a knife that you left behind on purpose,",
        "and each time it takes a little longer to notice.]",

        "You think about what your father tried to tell you on the shore.",
        "That the man standing in front of you is never really the enemy.",
        "That there was never anyone it would have been alright to cut down.",

        "You spent eleven years trying to prove him wrong.",
        "You never managed it. Not once. Not even with Askeladd.",

        "A real warrior has no need of a sword.",
        "You used to think that was a riddle.",
        "You are only now working out that it was an instruction.",

        "Dying was never the thing worth being afraid of.",
        "Reaching the end still holding the blade — that was the thing."
        };
        PlayScene(player,lines);
    }

    public static void Story_the_end_lapies(Player player)
    {
        Console.Clear();
        string [] lines =
        {
            "[LAPIS]",

            "No banners. No ships on the horizon. No one keeping score.",

            "Somewhere a long way behind you, a man is still walking down to the water",
            "on a flat calm morning, asking his son to come with him.",

            "This time you follow him for the right reason.",
            "             ~  THE END  ~"
        };
        PlayScene(player,lines);
    }
}
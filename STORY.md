# LAPIS — Story Script

All dialogue below is original writing in the spirit of the series, not lines lifted
from the manga or anime. Safe to use and submit as your own work.

`{player.Name}` marks where to interpolate the username from `CreatePlayer()`.
Lines in `[square brackets]` are narration / stage direction, not spoken — print them
in a dimmer colour (gray) so they read differently from dialogue.

---

## 1 — HOME — Thors, the morning

*Trigger: game start. Ends by unlocking movement north.*

```
THORS:  The sea's flat this morning. Come walk down to the water with me.
THORS:  There's something I'd rather say where your mother can't hear it.
THORS:  Go on ahead. North. I'll catch you up.
```

**Quest hook:** *Follow Thors to the Ocean.*

---

## 2 — OCEAN — Thors vs. Askeladd

*Trigger: arriving north. This is a cutscene, not a battle — the player cannot win
or lose it.*

```
[Thirty men on the shingle. Ships behind them. They were waiting.]

ASKELADD:  Thors Snorresson. The Troll of Jom.
ASKELADD:  Eleven years playing farmer, and not one of you thought
           somebody might eventually come looking.
THORS:     That's a lot of men for one farmer.
ASKELADD:  Men are cheap. Your name isn't.
THORS:     Then let's keep it cheap. One fight. You and me, no blades drawn on anyone else.
THORS:     When it's finished your ships leave, the village stands, and my son walks home.
ASKELADD:  And when you lose?
THORS:     Then you've still agreed to the terms.

[He wins. It takes almost no time at all.]
[Askeladd's sword is in the surf. Thors does not pick it up.]

ASKELADD:  Finish it. That's what the thing is for.
THORS:     A sword is what's left when a man's run out of better ideas.
THORS:     I ran out for a long time. I'm not going back to it.

[The archers do not need an order. Two arrows. Then a third.]

ASKELADD:  ...I didn't call for that.
```

### Thors' last words

```
THORS:     {player.Name}. Don't look away. Look at me.
THORS:     You're going to want to pick up a sword after this. I know. I did.
THORS:     The man standing in front of you is never really your enemy.
THORS:     There was never anyone it would have been alright to cut down. Not once.
THORS:     ...You don't need one. Nobody does. Work out what that means.

[He doesn't say anything else.]
```

### The player

```
PLAYER:    Dad?
PLAYER:    Dad. Get up.
PLAYER:    DAAAAAAAD!

PLAYER:    I'll kill him.
PLAYER:    I don't care how long it takes. I'll kill him myself.

[The shore tilts. Then the sky. Then nothing.]
```

---

## 3 — GRASSLAND — the first attempt

*Trigger: player wakes here. Several days have passed; they have been trailing the
raiders inland. Battle is scripted to be lost — Askeladd 1000 HP vs. the player's 10
damage.*

```
[Six days inland. You have not eaten in two of them.]

ASKELADD:  Still breathing? You've been behind us since the coast, boy.
           My men had a wager on when you'd drop.
PLAYER:    Draw your sword.
ASKELADD:  You're eleven years old.
PLAYER:    DRAW IT.
ASKELADD:  ...Alright. Come on then.
```

### After the loss

```
[You do not get close. You do not even make him step back.]

ASKELADD:  There it is. That's the gap between wanting to kill a man
           and being able to.
ASKELADD:  Nobody's closing that for you. Come back when you have.

[He walks off. He doesn't look back, and somehow that's the worst part.]
```

---

## 4 — FOREST — Leif finds you

*Trigger: player collapses after Grassland and wakes here.*

```
LEIF:      Gods above — {player.Name}? {player.Name}!
LEIF:      Six days I've been walking these woods. Six.
LEIF:      Your mother hasn't slept since the shore. Your sister asks about you
           every single morning and I've run out of lies.
PLAYER:    I'm not going back.
LEIF:      You're bleeding through your shirt and you weigh less than my anchor rope.
LEIF:      You're going as far as the town, and you're eating something, and then
           you can argue with me.
LEIF:      ...Stay close. The wolves have been bold this winter and I'm no fighter.
```

**QUEST 1 — Protect Leif.** *Wolves attack on the road to Town. Fail = retry.*

---

## 5 — TOWN — the chest

*Trigger: completing Quest 1.*

```
LEIF:      You really got stronger huh, {player.Name}
LEIF:      Sit down. There's something of your father's I've been carrying
           longer than you've been alive.

[A small chest. Older than it looks. Inside: throwing knives, wrapped in oiled cloth.]

LEIF:      He handed me these before you were born. Said he'd no use for them.
LEIF:      Said that if he ever came asking for them back, I was to refuse him.
PLAYER:    ...Did he ever ask?
LEIF:      Not once. Eleven years, not once.
PLAYER:    Then he won't mind me taking them.
LEIF:      {player.Name}—
PLAYER:    He wasn't beaten, Leif. He was shot. There's a difference and
           everyone on that beach knows it.
```

**Reward:** Thors' throwing knives (starting weapon).

---

## 6 — HOME — your mother and sister

*Trigger: travelling west/home from Town. No battle. Deliberately short and quiet.*

```
[Your mother doesn't shout. Somehow that's worse.]
[Your sister just holds onto your sleeve and won't let go of it.]

PLAYER:    I'm going after him.
PLAYER:    He didn't beat father. He had archers in the treeline the whole time.
PLAYER:    That wasn't a duel, it was a trap dressed up as one.
PLAYER:    A duel means something. He took that from us as well.
PLAYER:    I'll find him and I'll do it properly. Face to face, where people can see.
PLAYER:    ...Tell her I'm coming back.
LEIF:      Tell her yourself. Afterwards.
LEIF:      There's a ship at dawn. Iceland first — that's where the news goes
           before it goes anywhere else.
```

---

## 7 — ICELAND — Thorkell

*Trigger: travelling east from Home.*

```
[The biggest man you have ever seen is laughing at something that isn't funny.]

THORKELL:  HA! Look at this one!
THORKELL:  Half the size of my men and he's the only one who didn't step back.
PLAYER:    I'm looking for Askeladd.
THORKELL:  Everyone's looking for Askeladd. Slippery little Welsh liar,
           never where he says he'll be.
THORKELL:  Tell you what, small one. You're going to die out there regardless.
THORKELL:  So give me a proper fight first. If you're upright afterwards,
           I'll tell you exactly where he's sailing.
PLAYER:    And if I'm not upright?
THORKELL:  Then you were never getting near him anyway and I've saved you the trip!
THORKELL:  Either way I win! COME ON!
```

**QUEST 2 — Defeat Thorkell.**

### After the win

```
THORKELL:  ...HAHAHA! GOOD! That was GOOD!
THORKELL:  You've got your father's footwork. Did you know that?
           I fought him once. Lost. Best day of my life.
THORKELL:  Askeladd's sailing with the prince now. Canute. Pale little thing,
           prays more than he speaks.
THORKELL:  Find the king, you'll find the prince. Find the prince,
           you'll find your Welshman.
THORKELL:  And boy — the king's a dead man. He just hasn't been told.
```

---

## 8 — CANUTE'S KINGDOM — the throne room

*Trigger: travelling east from Iceland.*

```
CANUTE:    You're not one of my father's men.
PLAYER:    No.
CANUTE:    Good. My father's men are the ones I'm frightened of.
CANUTE:    Stay near me tonight. Please. There's no one else I can ask,
           and asking is all I have.
```

**QUEST 3 — Protect Canute.**

### Askeladd arrives

```
[The hall doors open. You know the shape before you see the face.]

ASKELADD:  Hello again. You got taller.

[He doesn't come for you. He walks straight past — toward the throne.]

ASKELADD:  Apologies, Your Majesty. Nothing personal in it.
ASKELADD:  This one's for Wales.

[The king falls. The guards are already moving.]

ASKELADD:  CANUTE! BE A KING!
ASKELADD:  BE A BETTER ONE THAN HE WAS!

[Twenty blades. It takes far less time than you spent imagining it.]
```

### The last conversation

```
PLAYER:    No. No — get up. GET UP.
PLAYER:    That was mine. That death was MINE.
ASKELADD:  ...heh. Sorry, kid.
PLAYER:    Eleven years. I followed you for eleven years.
ASKELADD:  I know. I let you.
PLAYER:    ...Why?
ASKELADD:  Because your father asked me something on that beach
           and I never came up with an answer.
ASKELADD:  He said a real warrior's got no need of a sword.
ASKELADD:  I've carried that around longer than you've carried me.
ASKELADD:  Go and find out what he meant.
ASKELADD:  I never managed it.

[He's gone. You are still holding your father's knives.]
[They have never felt heavier.]
```

---

## 9 — VINLAND — after

*Trigger: post-Askeladd. No enemies spawn here. Nothing to fight — that's the point.*

```
[The water here is warm. There are no walls and nobody is counting the dead.]
[You keep reaching for a knife that you left behind on purpose,
 and each time it takes a little longer to notice.]

You think about what your father tried to tell you on the shore.
That the man standing in front of you is never really the enemy.
That there was never anyone it would have been alright to cut down.

You spent eleven years trying to prove him wrong.
You never managed it. Not once. Not even with Askeladd.

A real warrior has no need of a sword.
You used to think that was a riddle.
You are only now working out that it was an instruction.

Dying was never the thing worth being afraid of.
Reaching the end still holding the blade — that was the thing.
```

---

## 10 — LAPIS — end game

```
[LAPIS]

No banners. No ships on the horizon. No one keeping score.

Somewhere a long way behind you, a man is still walking down to the water
on a flat calm morning, asking his son to come with him.

This time you follow him for the right reason.


                          ~  THE END  ~
```

*Play the ending audio here.*

---

## Implementation notes

**Storing lines.** Each scene is just an ordered list of strings — a `string[]` per
scene is enough, no class needed. One printer method that takes the array, writes each
line, and waits for a keypress between them covers every scene in the game.

**Speaker colours.** Consistency sells it more than the writing does:

| Speaker | Colour |
|---|---|
| Thors | `White` |
| Askeladd | `DarkRed` |
| Thorkell | `DarkYellow` |
| Canute | `DarkCyan` |
| Leif | `Green` |
| Player | `Cyan` |
| `[narration]` | `DarkGray` |

**Pacing.** The Ocean and Askeladd death scenes are the two that carry the game — let
those breathe with a keypress per line. Everything else can print two or three lines at
a time so it doesn't feel like clicking through a wall.

**Names.** Every `{player.Name}` needs interpolation. Thors, Leif and Askeladd use it;
Thorkell and Canute deliberately never do — neither of them knows who you are.

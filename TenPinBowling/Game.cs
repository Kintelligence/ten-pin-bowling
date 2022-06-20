using System.Text;

namespace TenPinBowling;

public class Game
{
    public int TotalScore { get; } = 0;
    public int Round { get; } = 0;
    public int Roll { get; } = 0;
    public Frame[] Frames { get; }
    public int?[] BonusRolls { get; }
    public int?[] Scores { get; private set; }

    public Game()
    {
        Frames = new Frame[10];

        for (int i = 0; i < 10; i++)
        {
            Frames[i] = new Frame();
        }

        BonusRolls = new int?[2];
        Scores = new int?[10];
    }

    public override string ToString()
    {
        return "";
    }
}
using System.Text;

namespace TenPinBowling;

public class Game
{
    public int TotalScore { get; } = 0;
    public int Round { get; } = 0;
    public int Roll { get; } = 0;
    public Frame InitialFrame { get; }

    public Game()
    {
        var lastFrame = new Frame { Id = 12 };

        for (int i = 11; i > 1; i--)
        {
            var frame = new Frame { Id = i, Next = lastFrame };
            lastFrame = frame;
        }

        InitialFrame = new Frame { Id = 1, Next = lastFrame };
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        Frame? frame = InitialFrame;
        var totalScore = 0;

        while (frame is not null)
        {
            if (frame.Score is not null)
            {
                totalScore += frame.Score.Value;
            }

            builder.AppendLine(frame.ToString());
            builder.AppendLine($"Total: {totalScore}");
            frame = frame.Next;
        }

        return builder.ToString();
    }
}
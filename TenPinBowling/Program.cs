namespace TenPinBowling;

public static class Program
{
    public static void Main()
    {
        var game = new Game();

        while (!game.IsOver)
        {
            Console.WriteLine();
            Console.WriteLine(game.ToAscii());
            Console.WriteLine();

            System.Console.WriteLine("Next roll:");
            var consoleInput = Console.ReadLine();
            if (!int.TryParse(consoleInput, out int input) || !game.InputRoll(input))
            {
                System.Console.WriteLine("Error: Provide a valid number input. Has to be a number betweeen 0 and 10 and total frame cannot excede 10");
            }
        }

        Console.WriteLine();
        Console.WriteLine(game.ToAscii());
        Console.WriteLine();

        System.Console.WriteLine($"Your final score was: {game.FinalScore}");
        if (game.FinalScore == 0)
        {
            System.Console.WriteLine("That's a gutter game...");
        }
        else if (game.FinalScore < 50)
        {
            System.Console.WriteLine("Better luck next time.");
        }
        else if (game.FinalScore < 150)
        {
            System.Console.WriteLine("Not bad.");
        }
        else if (game.FinalScore < 250)
        {
            System.Console.WriteLine("That's great.");
        }
        else if (game.FinalScore < 300)
        {
            System.Console.WriteLine("Almost a perfect game.");
        }
        else if (game.FinalScore == 300)
        {
            System.Console.WriteLine("That's a perfect game!");
        }
    }
}

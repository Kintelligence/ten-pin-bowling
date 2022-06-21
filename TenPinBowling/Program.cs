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
            System.Console.WriteLine("Input next roll");
            var consoleInput = Console.ReadLine();
            if (!int.TryParse(consoleInput, out int input) || !game.InputRoll(input))
            {
                System.Console.WriteLine("Error: Provide a valid number input. Has to be a number betweeen 0 and 10 and total frame cannot excede 10");
            }
        }

        Console.WriteLine(game.ToAscii());
        System.Console.WriteLine($"Your final score was {game.FinalScore}");
    }
}

namespace TenPinBowling.Tests.GameTests;

public class FinalScoreTests
{

    [Fact]
    public void FinalScore_WhenHittingNoPins_Returns0()
    {
        var game = new Game();

        for (int i = 0; i < 20; i++)
        {
            game.InputRoll(0);
        }

        game.FinalScore.Should().Be(0);
    }

    [Fact]
    public void FinalScore_WhenHittingOnlyStrikes_Returns300()
    {
        var game = new Game();

        for (int i = 0; i < 12; i++)
        {
            game.InputRoll(10);
        }

        game.FinalScore.Should().Be(300);
    }

    [Fact]
    public void FinalScore_WhenHittingOnlyFives_Returns150()
    {
        var game = new Game();

        for (int i = 0; i < 21; i++)
        {
            game.InputRoll(5);
        }

        game.FinalScore.Should().Be(150);
    }

    [Theory]
    [AutoData]
    public void FinalScore_WhenHittingRandomlyGeneratedGame_ReturnsFinalScore(Fixture fixture)
    {
        var rolls = fixture.CreateMany<int>(20);
        rolls = rolls.Select(c => Math.Abs(c) % 4);

        var game = new Game();

        foreach (var roll in rolls)
        {
            game.InputRoll(roll);
        }

        game.FinalScore.Should().Be(rolls.Sum());
    }

    [Fact]
    public void FinalScore_WhenPlayingExampleGame_Returns133()
    {
        var game = new Game();

        game.InputRoll(1);
        game.InputRoll(4);
        game.InputRoll(4);
        game.InputRoll(5);
        game.InputRoll(6);
        game.InputRoll(4);
        game.InputRoll(5);
        game.InputRoll(5);
        game.InputRoll(10);
        game.InputRoll(0);
        game.InputRoll(1);
        game.InputRoll(7);
        game.InputRoll(3);
        game.InputRoll(6);
        game.InputRoll(4);
        game.InputRoll(10);
        game.InputRoll(2);
        game.InputRoll(8);
        game.InputRoll(6);

        game.FinalScore.Should().Be(133);
    }
}
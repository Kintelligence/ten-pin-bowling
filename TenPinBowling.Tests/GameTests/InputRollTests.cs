namespace TenPinBowling.Tests.GameTests;

public class InputRollTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    public void WhenInputtingInvalidRoll_ReturnsFalse(int roll)
    {
        var game = new Game();

        game.InputRoll(roll).Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public void WhenInputtingValidRoll_ReturnsFalse([Range(0, 10)] int roll)
    {
        var game = new Game();

        game.InputRoll(roll).Should().BeTrue();
    }
}
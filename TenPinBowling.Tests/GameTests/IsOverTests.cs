namespace TenPinBowling.Tests.GameTests;

public class IsOverTests
{
    [Theory]
    [AutoData]
    public void IsOver_AfterHitting20RollsWithNoStrikesOrSpares_ReturnsTrue(Fixture fixture)
    {
        var rolls = fixture.CreateMany<int>(20);
        rolls = rolls.Select(c => Math.Abs(c) % 4);

        var game = new Game();

        foreach (var roll in rolls)
        {
            game.InputRoll(roll);
        }

        game.IsOver.Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void IsOver_AfterHitting19RollsWithNoStrikesOrSpares_ReturnsFalse(Fixture fixture)
    {
        var rolls = fixture.CreateMany<int>(19);
        rolls = rolls.Select(c => Math.Abs(c) % 4);

        var game = new Game();

        foreach (var roll in rolls)
        {
            game.InputRoll(roll);
        }

        game.IsOver.Should().BeFalse();
    }

    [Fact]
    public void IsOver_AfterEndingOnASpare_ReturnsFalse()
    {
        var game = new Game();

        for (int i = 0; i < 20; i++)
        {
            game.InputRoll(5);
        }

        game.IsOver.Should().BeFalse();
    }

    [Fact]
    public void IsOver_AfterEndingOnASpareAnd1BonusRoll_ReturnsTrue()
    {
        var game = new Game();

        for (int i = 0; i < 21; i++)
        {
            game.InputRoll(5);
        }

        game.IsOver.Should().BeTrue();
    }

    [Fact]
    public void IsOver_WhenEndingOnAStrike_ReturnsFalse()
    {
        var game = new Game();

        for (int i = 0; i < 10; i++)
        {
            game.InputRoll(10);
        }

        game.IsOver.Should().BeFalse();
    }

    [Fact]
    public void IsOver_WhenEndingOnAStrikeAnd1BonusRoll_ReturnsFalse()
    {
        var game = new Game();

        for (int i = 0; i < 11; i++)
        {
            game.InputRoll(10);
        }

        game.IsOver.Should().BeFalse();
    }

    [Fact]
    public void IsOver_WhenEndingOnAStrikeAnd2BonusRolls_ReturnsTrue()
    {
        var game = new Game();

        for (int i = 0; i < 12; i++)
        {
            game.InputRoll(10);
        }

        game.IsOver.Should().BeTrue();
    }
}
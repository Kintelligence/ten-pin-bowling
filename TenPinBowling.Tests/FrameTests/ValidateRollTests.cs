namespace TenPinBowling.Tests.FrameTests;

public class ValidateRollTests
{
    [Theory]
    [AutoData]
    public void WhenRollIsInRange_ReturnsTrue([Range(0, 10)] int roll)
    {
        var frame = new Frame();

        frame.ValidateRoll(roll).Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void WhenSecondRollIsInRange_ReturnsTrue([Range(0, 10)] int roll)
    {
        var frame = new Frame();
        frame.SetRoll(10 - roll);

        frame.ValidateRoll(roll).Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void WhenTotalIsOutOfRange_ReturnsFalse([Range(1, 10)] int roll)
    {
        var frame = new Frame();
        frame.SetRoll(11 - roll);

        frame.ValidateRoll(roll).Should().BeFalse();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    public void WhenSecondRollIsOutOfRange_ReturnsFalse(int roll)
    {
        var frame = new Frame();
        frame.SetRoll(0);

        frame.ValidateRoll(roll).Should().BeFalse();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    public void WhenRollIsOutOfRange_ReturnsFalse(int roll)
    {
        var frame = new Frame();

        frame.ValidateRoll(roll).Should().BeFalse();
    }

    [Fact]
    public void WhenRollingMoreThanTwice_ReturnsFalse()
    {
        var frame = new Frame();
        frame.SetRoll(0);
        frame.SetRoll(0);

        frame.ValidateRoll(0).Should().BeFalse();
    }
}
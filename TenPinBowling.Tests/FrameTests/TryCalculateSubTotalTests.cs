namespace TenPinBowling.Tests.FrameTests;

public class TryCalculateSubTotalTests
{
    [Theory]
    [AutoData]
    public void WhenSimpleRollsWithNoNext_ReturnsSubTotal([Range(0, 8)] int roll)
    {
        var frame = new Frame();
        frame.AddRoll(roll);
        frame.AddRoll(9 - roll);

        frame.TryCalculateSubTotal().Should().Be(9);
    }

    [Fact]
    public void WhenStrikeWithNoNext_ReturnsNull()
    {
        var frame = new Frame();
        frame.AddRoll(10);

        frame.TryCalculateSubTotal().Should().BeNull();
    }

    [Theory]
    [AutoData]
    public void WhenSpareWithNoNext_ReturnsNull([Range(0, 9)] int roll)
    {
        var frame = new Frame();
        frame.AddRoll(roll);
        frame.AddRoll(10 - roll);

        frame.TryCalculateSubTotal().Should().BeNull();
    }

    [Theory]
    [AutoData]
    public void WhenSpare_Returns10PlusNextRoll([Range(0, 9)] int roll)
    {
        var frame = new Frame();
        frame.AddRoll(0);
        frame.AddRoll(10);
        var next = new Frame();
        frame.Append(next);
        next.AddRoll(roll);
        next.AddRoll(1);

        frame.TryCalculateSubTotal().Should().Be(10 + roll);
    }

    [Theory]
    [AutoData]
    public void WhenStrike_Returns10PlusNext2Rolls([Range(0, 5)] int rollA, [Range(0, 5)] int rollB)
    {
        var frame = new Frame();
        frame.AddRoll(10);
        var next = new Frame();
        frame.Append(next);
        next.AddRoll(rollA);
        next.AddRoll(rollB);

        frame.TryCalculateSubTotal().Should().Be(10 + rollA + rollB);
    }

    [Fact]
    public void WhenThreeStrikesInARow_Returns30()
    {
        var frame = new Frame();
        frame.AddRoll(10);
        var next = new Frame();
        frame.Append(next);
        next.AddRoll(10);
        var last = new Frame();
        next.Append(last);
        last.AddRoll(10);

        frame.TryCalculateSubTotal().Should().Be(30);
    }

    [Theory]
    [AutoData]
    public void WhenSimpleRoll_ReturnsSubTotal([Range(0, 8)] int roll)
    {
        var frame = new Frame();
        frame.AddRoll(roll);
        frame.AddRoll(9 - roll);
        var next = new Frame();
        frame.Append(next);
        next.AddRoll(roll);

        frame.TryCalculateSubTotal().Should().Be(9);
    }

    [Theory]
    [AutoData]
    public void WhenRollIsRequiredToCalculatePreviousSubTotals_PreviousSubTotalsAreCalculated([Range(0, 4)] int roll)
    {
        var previous = new Frame();
        previous.AddRoll(5);
        previous.AddRoll(5);
        var current = new Frame();
        previous.Append(current);
        current.AddRoll(roll);
        current.AddRoll(roll);

        current.TryCalculateSubTotal();

        previous.SubTotal.Should().Be(10 + roll);
    }

    [Theory]
    [AutoData]
    public void WhenMultipleFrames_LastSubTotalIsTotal(IEnumerable<int> rolls)
    {
        rolls = rolls.Select(c => Math.Abs(c) % 4);
        IFrame? last = null;

        foreach (var roll in rolls)
        {
            var current = new Frame();
            current.AddRoll(roll);
            current.AddRoll(roll);
            last?.Append(current);
            last = current;
        }

        last?.TryCalculateSubTotal();

        last?.SubTotal.Should().Be(rolls.Sum() * 2);
    }
}
namespace TenPinBowling;

public class BonusFrame : IFrame
{
    public int?[] Rolls { get; } = new int?[1];
    public IFrame? Next { get; set; }
    public IFrame? Previous { get; set; }
    public bool IsFull => Rolls[0].HasValue;
    public int? SubTotal => Previous?.SubTotal;

    public bool IsBonus => true;

    public void AddRoll(int roll)
    {
        if (ValidateRoll(roll))
        {
            Rolls[0] = roll;
        }
    }

    public string Print()
    {
        return $"[{Rolls[0]?.ToString() ?? " "}]";
    }

    public int? TryCalculateSubTotal()
    {
        return Previous?.TryCalculateSubTotal();
    }

    public bool ValidateRoll(int roll)
    {
        return roll >= 0 && roll <= 10;
    }
}

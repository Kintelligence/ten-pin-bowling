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

    public string RollsAsAscii()
    {
        if (Previous is not null)
        {
            if (Previous.IsFull && Previous.SubTotal is null)
            {
                return $"[{Rolls[0]?.ToString() ?? " "}]";
            }

            if (Previous.IsBonus &&
                Previous.Previous is not null &&
                Previous.Previous.IsFull &&
                Previous.Previous.Rolls[0] == 10)
            {
                return $"[{Rolls[0]?.ToString() ?? " "}]";
            }
        }

        if (Rolls[0] is null)
        {
            return "";
        }

        return $"[{Rolls[0]}]";
    }

    public string SubTotalAsAscii()
    {
        return "";
    }

    public int? TryCalculateSubTotal()
    {
        return Previous?.TryCalculateSubTotal();
    }

    public bool ValidateRoll(int roll)
    {
        return roll >= 0 && roll <= 10;
    }

    public void Append(IFrame frame)
    {
        if (frame.Previous is not null)
        {
            throw new InvalidOperationException("Cannot append frame. Next frame already has a previous");
        }

        if (Next is not null)
        {
            throw new InvalidOperationException("Cannot append frame. Current frame already has a next");
        }

        frame.Previous = this;
        Next = frame;
    }
}

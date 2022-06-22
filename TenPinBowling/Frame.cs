namespace TenPinBowling;

public class Frame : IFrame
{
    public int?[] Rolls { get; } = new int?[2];
    public int? SubTotal { get; private set; }
    public bool IsBonus => false;
    public IFrame? Next { get; set; }
    public IFrame? Previous { get; set; }

    public bool IsFull
    {
        get
        {
            if (Rolls[0] is not null)
            {
                if (Rolls[0] >= 10)
                {
                    return true;
                }

                if (Rolls[1] is not null)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public bool ValidateRoll(int roll)
    {
        if (IsFull)
        {
            return false;
        }

        if (roll < 0 || roll > 10)
        {
            return false;
        }

        if (Rolls[0] is not null)
        {
            if (Rolls[1] is not null)
            {
                return false;
            }

            if (Rolls[0] + roll > 10)
            {
                return false;
            }
        }

        return true;
    }

    public void AddRoll(int roll)
    {
        if (!ValidateRoll(roll))
        {
            throw new ArgumentException("Invalid roll");
        }

        if (Rolls[0] is null)
        {
            Rolls[0] = roll;
        }
        else
        {
            Rolls[1] = roll;
        }
    }

    public string RollsAsAscii()
    {
        if (Rolls[0] is null)
        {
            return "[ , ]";
        }

        if (Rolls[0] == 10)
        {
            return "[x, ]";
        }

        if (Rolls[1] == null)
        {
            return $"[{Rolls[0]}, ]";
        }

        if (Rolls[1] + Rolls[0] == 10)
        {
            return $"[{Rolls[0]},/]";
        }

        return $"[{Rolls[0]},{Rolls[1]}]";
    }

    private int? TryCalculateStrike(int prev)
    {
        if (Next is null ||
            !Next.IsFull)
        {
            return null;
        }

        if (Next.IsBonus || Next.Rolls[1] is null)
        {
            if (Next.Next?.Rolls[0] is null)
            {
                return null;
            }

            SubTotal = prev + 10 + Next.Rolls[0] + Next.Next.Rolls[0];
            return SubTotal;
        }

        SubTotal = prev + 10 + Next.Rolls[0] + Next.Rolls[1];
        return SubTotal;
    }

    private int? TryCalculateSpare(int prev)
    {
        if (Next is null ||
            Next.Rolls[0] is null)
        {
            return null;
        }

        SubTotal = prev + 10 + Next.Rolls[0];
        return SubTotal;
    }

    private int? GetPreviousSubTotal()
    {
        if (Previous is null)
        {
            return 0;
        }

        Previous.TryCalculateSubTotal();

        return Previous.SubTotal;
    }

    public int? TryCalculateSubTotal()
    {
        if (SubTotal is not null)
        {
            return SubTotal;
        }

        if (!IsFull)
        {
            return null;
        }

        var prev = GetPreviousSubTotal();

        if (prev is null)
        {
            return null;
        }

        if (Rolls[0] == 10)
        {
            return TryCalculateStrike(prev.Value);
        }

        if (Rolls[0] + Rolls[1] == 10)
        {
            return TryCalculateSpare(prev.Value);
        }

        SubTotal = prev + Rolls[0] + Rolls[1];
        return SubTotal;
    }

    public string SubTotalAsAscii()
    {
        return $"[{SubTotal?.ToString() ?? "",3}]";
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

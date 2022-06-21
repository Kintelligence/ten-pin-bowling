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

    public string Print()
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
            return $"[{Rolls[1]}, ]";
        }

        if (Rolls[1] + Rolls[0] == 10)
        {
            return $"[{Rolls[1]},/]";
        }

        return $"[{Rolls[0]},{Rolls[1]}]";
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

        var prev = 0;

        if (Previous is not null)
        {
            Previous.TryCalculateSubTotal();

            if (Previous.SubTotal is null)
            {
                return null;
            }

            prev = Previous.SubTotal.Value;
        }

        if (Rolls[0] == 10)
        {
            if (Next is null ||
                !Next.IsFull)
            {
                return null;
            }

            if (Next.Rolls[1] is null)
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

        if (Rolls[0] + Rolls[1] == 10)
        {
            if (Next is null ||
                Next.Rolls[0] is null)
            {
                return null;
            }

            SubTotal = prev + 10 + Next.Rolls[0];
            return SubTotal;
        }

        SubTotal = prev + Rolls[0] + Rolls[1];
        return SubTotal;
    }
}

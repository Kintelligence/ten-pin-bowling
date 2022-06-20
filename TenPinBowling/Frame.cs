namespace TenPinBowling;

public class Frame
{
    public int?[] Rolls { get; } = new int?[2];

    public bool IsFull()
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

    public bool ValidateRoll(int roll)
    {
        if (IsFull())
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

    public override string ToString()
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
}

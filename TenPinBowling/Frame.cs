namespace TenPinBowling;

public class Frame
{
    public int Id { get; init; }
    public int? Score { get; }
    public int?[] Rolls { get; } = new int?[2];
    public Frame? Next { get; init; }

    public bool ValidateRoll(int roll)
    {
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

    public bool SetRoll(int roll)
    {
        if (!ValidateRoll(roll))
        {
            return false;
        }

        if (Rolls[0] is null)
        {
            Rolls[0] = roll;
        }
        else
        {
            Rolls[1] = roll;
        }

        return true;
    }

    public void CalculateScore()
    {
        if (Rolls[0] is null)
        {
            return;
        }

        if (Rolls[0] == 10)
        {

        }


    }

    public override string ToString()
    {
        return $"Round: {Id}.\t[{Rolls[0]?.ToString() ?? "*"},{Rolls[1]?.ToString() ?? "*"}].\tScore: {Score?.ToString() ?? "*"}";
    }
}

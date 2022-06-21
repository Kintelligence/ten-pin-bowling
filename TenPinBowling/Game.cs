using System.Text;

namespace TenPinBowling;

public class Game
{
    private IFrame _initialFrame;
    private IFrame _currentFrame;
    private IFrame _finalFrame;

    public int? FinalScore => _finalFrame.SubTotal;
    public bool IsOver => _currentFrame.SubTotal is not null && (_currentFrame.IsBonus || _currentFrame.Next?.IsBonus == true);

    public Game()
    {
        _initialFrame = new Frame();
        _currentFrame = _initialFrame;
        IFrame? previous = _initialFrame;

        for (int i = 0; i < 9; i++)
        {
            var current = new Frame();
            previous.Append(current);

            previous = current;
        }

        _finalFrame = previous;

        for (int i = 0; i < 2; i++)
        {
            var current = new BonusFrame();
            previous.Append(current);

            previous = current;
        }
    }

    public bool InputRoll(int roll)
    {
        if (!_currentFrame.ValidateRoll(roll))
        {
            return false;
        }

        _currentFrame.AddRoll(roll);

        if (_currentFrame.IsFull)
        {
            _currentFrame.TryCalculateSubTotal();

            if (!IsOver)
            {
                if (_currentFrame.Next is null)
                {
                    throw new Exception("Next frame is null but game isn't over");
                }

                _currentFrame = _currentFrame.Next;
            }
        }

        return true;
    }

    public string ToAscii()
    {
        var rollBuilder = new StringBuilder();
        var subTotalBuilder = new StringBuilder();

        var current = _initialFrame;
        while (current is not null)
        {
            rollBuilder.Append(current.RollsAsAscii());
            subTotalBuilder.Append(current.SubTotalAsAscii());
            current = current.Next;
        }

        rollBuilder.AppendLine();
        return rollBuilder.AppendLine(subTotalBuilder.ToString()).ToString();
    }
}
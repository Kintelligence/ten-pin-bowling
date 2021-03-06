namespace TenPinBowling;

public interface IFrame
{
    public int?[] Rolls { get; }
    public IFrame? Next { get; set; }
    public IFrame? Previous { get; set; }
    public bool IsFull { get; }
    public string RollsAsAscii();
    public string SubTotalAsAscii();
    public bool ValidateRoll(int roll);
    public void AddRoll(int roll);
    public int? SubTotal { get; }
    public bool IsBonus { get; }
    public int? TryCalculateSubTotal();
    public void Append(IFrame frame);
}
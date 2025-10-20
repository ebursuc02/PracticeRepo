namespace TimeCardsManagement_E20.Domain.Entities;

public sealed class AnnualLeaveRecord : TimeRecord
{
    public bool IsApproved { get; private set; }

    private AnnualLeaveRecord(DateOnly date) : base(date) { }

    public static AnnualLeaveRecord Create(DateOnly date) => new(date);

    public void Approve() => IsApproved = true;
    public void RevokeApproval() => IsApproved = false;
}

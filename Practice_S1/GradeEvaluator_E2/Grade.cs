namespace GradeEvaluator_E2;

public class Grade
{
    public required int Score { get; init; }
    public DateTime Date { get; } = DateTime.UtcNow;
    public required string Subject { get; init; } = string.Empty;
}

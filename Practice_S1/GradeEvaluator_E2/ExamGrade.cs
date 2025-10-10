namespace GradeEvaluator_E2;

public class ExamGrade : Grade
{
    public string ExamType { get; init; } = string.Empty;
    public bool Passed { get => Score >= 5; }
}

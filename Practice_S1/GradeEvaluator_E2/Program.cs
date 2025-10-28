using GradeEvaluator_E2;

var grades = new List<Grade>
{
    // Regular grades
    new Grade { Score = 94, Subject = "Machine Learning" },
    new Grade { Score = 76, Subject = "Artificial Intelligence" },

    new ExamGrade { Score = 88, Subject = "Machine Learning", ExamType = "Midterm" },
    new ExamGrade { Score = 91, Subject = "Machine Learning", ExamType = "Final" },

    new ExamGrade { Score = 72, Subject = "Artificial Intelligence", ExamType = "Midterm" },
    new ExamGrade { Score = 80, Subject = "Artificial Intelligence", ExamType = "Retake" }
};

var groupedGradesBySubject = grades.GroupBy(g => g.Subject);

foreach (var group in groupedGradesBySubject)
{
    var subject = group.First().Subject;
    Console.WriteLine($"{subject} Average Grade: {group.Average(g => g.Score)}");
}

Console.WriteLine($"Total average: {grades.Average(g => g.Score)}");

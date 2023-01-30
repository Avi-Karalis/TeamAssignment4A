using TeamAssignment4A.Models;

namespace TeamAssignment4A.Dtos
{
    public class ExamStemDto
    {
        public int Id { get; set; }
        public char SubmittedAnswer { get; set; }
        public int Score { get; set; }
        public int ExamId { get; set; }
        public  Exam Exam { get; set; }
        public Stem StemId { get; set; }
        public  Stem Stem { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace TeamAssignment4A.Models.JointTables {
    public class ExamStems {

        public char SubmitedAnswer { get; set; }
        [Key]
        public Stem Stems { get; set; }
        [Key]
        public Exam Exams { get; set; }
    }
}

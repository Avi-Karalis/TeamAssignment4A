using System.ComponentModel.DataAnnotations;

namespace TeamAssignment4A.Models.JointTables {
    public class ExamTopics {
        [Key]
        public Exam Exams { get; set; }

        [Key]
        public virtual Topic Topics { get; set; }
        public int NumberOfAwardedMarks { get; set; }

    }
}

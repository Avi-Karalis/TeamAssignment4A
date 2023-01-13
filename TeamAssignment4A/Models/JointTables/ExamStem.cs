using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamAssignment4A.Models.JointTables {
    public class ExamStem {
        public int Id { get; set; }

        //[Key, Column(Order = 0)]
        //public int ExamId { get; set; }

        //[Key, Column(Order = 1)]
        //public int StemId { get; set; }

        public virtual Exam Exams { get; set; }
        public virtual Stem Stems { get; set; }

        public char SubmittedAnswer { get; set; }
        public int Score { get; set; }

    }
}

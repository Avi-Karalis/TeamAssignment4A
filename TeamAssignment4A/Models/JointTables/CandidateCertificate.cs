using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamAssignment4A.Models.JointTables {
    public class CandidateCertificate {
        [Key]
        public Certificate Certificates { get; set; }
        [Key]
        public Candidate Candidates { get; set; }
        [ForeignKey("Exam")]
        public virtual Exam Exams { get; set; }
    }
}

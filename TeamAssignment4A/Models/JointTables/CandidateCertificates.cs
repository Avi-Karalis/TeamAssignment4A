using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamAssignment4A.Models.JointTables
{
    public class CandidateCertificates
    {

        //Navigation Property
        [Key]
        [ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        //Navigation Property
        [Key]
        [ForeignKey("Certificate")]
        public int CertificateId { get; set; }
        public Certificate Certificate { get; set; }

        //Navigation Property
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}
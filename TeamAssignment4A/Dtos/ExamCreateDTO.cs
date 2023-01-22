using TeamAssignment4A.Models;

namespace TeamAssignment4A.Dtos {
    public class ExamCreateDTO {
        public string assessmentTestCode { get; set; }
        public int CertificateId { get; set; }
        public int CandidateId { get; set; }
    }
}

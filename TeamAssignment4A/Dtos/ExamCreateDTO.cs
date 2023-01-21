using TeamAssignment4A.Models;

namespace TeamAssignment4A.Dtos {
    public class ExamCreateDTO {
        public string ExamDescription { get; set; }
        public int CertificateId { get; set; }
        public int Topic1Id { get; set; }
        public int Topic2Id { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Dtos {
    public class BuyCertificateDTO {
        public int CertificateId { get; set; }
        public int CandidateId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime ExaminationDate {get; set;}
    }
}

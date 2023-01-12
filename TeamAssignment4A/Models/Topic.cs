using System.ComponentModel.DataAnnotations.Schema;

namespace TeamAssignment4A.Models {
    public class Topic {
        public int Id { get; set; }
        public string Description { get; set; }
        public int NumberOfPossibleMarks { get; set; }
        public int CertificateID { get; set; }

        //Navigation Property
        [ForeignKey("Certificate")]
        public virtual ICollection<Certificate> Certificates { get; set; }
    }
}

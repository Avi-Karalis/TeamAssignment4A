using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TeamAssignment4A.Models.JointTables
{
    public class CertificateTopics
    {
        //Navigation Property
        [Key]
        [ForeignKey("Topic")]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        //Navigation Property
        [Key]
        [ForeignKey("Certificate")]
        public int CertificateId { get; set; }
        public Certificate Certificate { get; set; }
    }
}
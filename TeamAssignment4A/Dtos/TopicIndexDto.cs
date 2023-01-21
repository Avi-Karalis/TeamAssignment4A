using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Dtos {
    public class TopicIndexDto {

        [Required]
        [Display(Name = "Topic Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Number Of Possible Marks")]
        public int NumberOfPossibleMarks { get; set; }

        [Required]
        [Display(Name = "Certificate Title")]
        public string CertificateTitle { get; set; }

        public TopicIndexDto(Topic topic) {
            this.Id = topic.Id;
            this.Description = topic.Description;
            this.NumberOfPossibleMarks = topic.NumberOfPossibleMarks;
            this.CertificateTitle = topic.Certificate.TitleOfCertificate;
        }
    }
}

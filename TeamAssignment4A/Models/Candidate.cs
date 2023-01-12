using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TeamAssignment4A.Models {

    [Table("Candidates")]
    public class Candidate {
        //Basic properties for each Candidate
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Key]
        [Required]
        [Display(Name = "Candidate Number")]
        public int CandidateNumber { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Native Language")]
        public string NativeLanguage { get; set; }

        [Required]
        [Display(Name = "Country Of Residence")]
        public string CountryOfResidence { get; set; }

        [Required]
        [Display(Name = "Birthdate")]
        public DateTime Birthdate { get; set; }

        [Required]
        [Display(Name = "E-mail adress")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Land Line Phone Number")]
        public string LandLineNumber { get; set; }

        [Required]
        [Display(Name = "Mobile Phone Number")]
        public string MobileNumber { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string Address1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string? Address2 { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Town")]
        public string Town { get; set; }

        [Display(Name = "Province")]
        public string? Province { get; set; }

        [Required]
        [Display(Name = "Photo Id Type")]
        public string PhotoIdType { get; set; }

        [Required]
        [Display(Name = "Photo Id Number")]
        public string PhotoIdNumber { get; set; }

        [Required]
        [Display(Name = "Photo Id Date")]
        public DateTime PhotoIdDate { get; set; }

        [ForeignKey("Certificate")]
        public virtual ICollection<Certificate> Certificates { get; set; }
    }
}

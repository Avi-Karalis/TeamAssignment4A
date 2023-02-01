using System.ComponentModel.DataAnnotations;

namespace TeamAssignment4A.Models.JointTables 
{
    public class ExamStem 
    {
        [Display(Name = "Exam Stem Id")]
        public int Id { get; set; }

        
        // Navigation Properties
        public virtual Exam Exam { get; set; }
        public virtual Stem Stem { get; set; }

    }
}

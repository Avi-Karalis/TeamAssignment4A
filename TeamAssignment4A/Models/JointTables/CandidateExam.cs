using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamAssignment4A.Models.JointTables {
    public class CandidateExam 
    {
        public int Id { get; set; }     


        // Navigation Properties
        public virtual Candidate Candidate { get; set; }
        public virtual Exam Exam { get; set; }
        
    }
}

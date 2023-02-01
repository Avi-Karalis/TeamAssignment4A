using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Xml.Linq;
using TeamAssignment4A.Dtos;
using TeamAssignment4A.Models.JointTables;


namespace TeamAssignment4A.Models
{
    public class Exam 
    {
        public int Id { get; set; }        


        // Navigation Properties

        public virtual Certificate Certificate { get; set; }
        public virtual IEnumerable<CandidateExam>? CandidateExams { get; set; }        
        public virtual IEnumerable<ExamStem>? ExamStems { get; set; }
    }
   
}

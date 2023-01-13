using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Xml.Linq;
using TeamAssignment4A.Models.JointTables;
//using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Models
{
    public class Exam {
        public int Id { get; set; }

        public string ExamDescription { get; set; }


        //navigation property
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<CandidateExam> CandidateExams { get; set; }
        public virtual ICollection<ExamTopic> ExamTopics { get; set; }
        public virtual ICollection<ExamStem> ExamStems { get; set; }
    }
   
}

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using TeamAssignment4A.Models;
using TeamAssignment4A.Models.JointTables;

namespace TeamAssignment4A.Dtos
{
    public class ExamDto
    {
        public int Id { get; set; }    
        public string TitleOfCertificate { get; set; }
        public Certificate Certificate { get; set; }
        public IEnumerable<int> ExamStemIds { get; set; }
        public IEnumerable<ExamStem> ExamStems { get; set; }
    }
}

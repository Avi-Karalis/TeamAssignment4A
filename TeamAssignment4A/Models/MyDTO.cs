using Microsoft.AspNetCore.Mvc;
using TeamAssignment4A.Dtos;

namespace TeamAssignment4A.Models
{
    public class MyDTO
    {
        public string? View { get; set; }
        public string? Message { get; set; }
        public Certificate? Certificate { get; set; }
        public IEnumerable<Certificate>? Certificates { get; set;}
        public StemDto? StemDto { get; set; }
        public IEnumerable<StemDto>? StemDtos { get; set; }
        public Topic? TopicDto { get; set; }
        public IEnumerable<Topic>? TopicDtos { get;}
    }
}

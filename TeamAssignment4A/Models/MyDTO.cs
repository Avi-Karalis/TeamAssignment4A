using Microsoft.AspNetCore.Mvc;

namespace TeamAssignment4A.Models
{
    public class MyDTO
    {
        public string? View { get; set; }
        public string? Message { get; set; }
        public Certificate? Certificate { get; set; }
        public IEnumerable<Certificate>? Certificates { get; set;}
    }
}

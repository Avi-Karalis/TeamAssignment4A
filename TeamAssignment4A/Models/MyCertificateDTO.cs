using Microsoft.AspNetCore.Mvc;

namespace TeamAssignment4A.Models
{
    public class MyCertificateDTO
    {
        public string? View { get; set; }
        public string? Message { get; set; }
        public Certificate? Certificate { get; set; }
    }
}

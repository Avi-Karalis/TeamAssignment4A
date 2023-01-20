using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public interface ICertificateService
    {
        Task<MyCertificateDTO> GetCertificate(int id);
        Task<ICollection<Certificate>?> GetAllCertificates();
        Task<MyCertificateDTO> AddOrUpdate(int id, Certificate certificate);
        Task<MyCertificateDTO> Delete(int id);
    }
}
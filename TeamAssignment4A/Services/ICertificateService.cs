using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public interface ICertificateService
    {
        Task<MyCertificateDTO> GetCertificate(int id);
        Task<ICollection<Certificate>?> GetAllCertificates();
        Task<MyCertificateDTO> AddOrUpdateCertificate(int id, Certificate certificate);
        Task<MyCertificateDTO> Delete(int id);
        Task<MyCertificateDTO> GetForUpdate(int id);
        Task<MyCertificateDTO> GetForDelete(int id);
    }
}
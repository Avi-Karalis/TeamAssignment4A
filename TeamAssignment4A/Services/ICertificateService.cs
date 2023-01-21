using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public interface ICertificateService
    {
        Task<MyDTO> GetCertificate(int id);
        Task<ICollection<Certificate>?> GetAllCertificates();
        Task<MyDTO> AddOrUpdateCertificate(int id, Certificate certificate);
        Task<MyDTO> Delete(int id);
        Task<MyDTO> GetForUpdate(int id);
        Task<MyDTO> GetForDelete(int id);
    }
}
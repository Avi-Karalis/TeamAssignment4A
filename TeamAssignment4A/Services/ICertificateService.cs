using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public interface ICertificateService
    {
        Task<string> GetCertificate(int id);
        Task<string> GetAllCertificates();
        Task<string> AddOrUpdate(int id, Certificate certificate);
        Task<string> Delete(int id);
    }
}
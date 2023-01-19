using Microsoft.AspNetCore.Mvc;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public class CertificateService : ICertificateService
    {
        private UnitOfWork _unit;
        public CertificateService(UnitOfWork unit)
        {
            _unit= unit;
        }
        public async Task<string> GetCertificate(int id)
        {
            Task<Certificate> certificate = _unit.Certificate.GetAsync(id);
            if(certificate == null)
            {
                return "Index" ;
            }
            return "Details, certificate";
        }

        public async Task<string> GetAllCertificates()
        {
            throw new NotImplementedException();
        }

        public async Task<string> AddOrUpdate(int id, Certificate certificate)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

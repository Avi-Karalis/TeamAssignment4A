using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            Certificate? certificate = await _unit.Certificate.GetAsync(id);
            if(certificate == null)
            {
                return "Index" ;
            }
            return "Details, certificate";
        }

        public async Task<string> GetAllCertificates()
        {
            ICollection<Certificate>? certificates = await _unit.Certificate.GetAllAsync();
            if(certificates == null)
            {
                return "Index" ;
            }
            return "Details, certificates";            
        }

        public async Task<string> AddOrUpdate(Certificate certificate)
        {
            EntityState state =  _unit.Certificate.AddOrUpdate(certificate);
            await _unit.SaveAsync();
            if (state == EntityState.Added)
            {
                return "Index";
            }
            else if(state == EntityState.Modified)
            {
                return "Index";
            }
            return "";
        }

        public async Task<string> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

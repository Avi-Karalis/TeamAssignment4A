using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public class CertificateService : ControllerBase, ICertificateService
    {
        private UnitOfWork _unit;
        private WebAppDbContext _db;
        private MyCertificateDTO _myDTO;
        public CertificateService(UnitOfWork unit, WebAppDbContext db)
        {
            _unit= unit;
            _db = db;
            _myDTO = new MyCertificateDTO();
        }
        public async Task<MyCertificateDTO> GetCertificate(int id)
        {
            if((id == null || _db.Certificates == null) || await _unit.Certificate.GetAsync(id) == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested certificate was not found. Please try again.";
            }
            else
            {
                _myDTO.View = "Details";
                _myDTO.Certificate = await _unit.Certificate.GetAsync(id);
            }             
            return _myDTO;
        }

        public async Task<ICollection<Certificate>?> GetAllCertificates()
        {
            return await _unit.Certificate.GetAllAsync();                       
        }

        public async Task<MyCertificateDTO> AddOrUpdate(int id, [Bind("Id,TitleOfCertificate,PassingGrade,MaximumScore")] Certificate certificate)
        {            
            if(ModelState.IsValid)
            {
                EntityState state =  _unit.Certificate.AddOrUpdate(certificate);
                await _unit.SaveAsync();
                if (state == EntityState.Added || state == EntityState.Modified)
                {
                    _myDTO.View = "Index";
                }                
            }
            
            
                return _myDTO;
            
        }

        public Task<MyCertificateDTO> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }   
}

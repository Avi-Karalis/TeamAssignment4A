using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public class CertificateService : ControllerBase, ICertificateService
    {
        private UnitOfWork _unit;
        private WebAppDbContext _db;
        private MyDTO _myDTO;
        public CertificateService(UnitOfWork unit, WebAppDbContext db)
        {
            _unit= unit;
            _db = db;
            _myDTO = new MyDTO();
        }
        public async Task<MyDTO> GetCertificate(int id)
        {
            if((id == null || _db.Certificates == null) || await _unit.Certificate.GetAsync(id) == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested certificate could not be found. Please try again later.";
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

        public async Task<MyDTO> GetForUpdate(int id)
        {
            _myDTO.View = "Edit";
            if (id == null || _db.Certificates == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested certificate could not be found. Please try again later.";
                return _myDTO;
            }
            _myDTO.Certificate = await _unit.Certificate.GetAsync(id);
            if (_myDTO.Certificate == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested certificate could not be found. Please try again later.";
            }
            return _myDTO;
        }        

        public async Task<MyDTO> AddOrUpdateCertificate(int id, [Bind("Id,TitleOfCertificate,PassingGrade,MaximumScore")] Certificate certificate)
        {
            EntityState state = _unit.Certificate.AddOrUpdate(certificate);
            if (id != certificate.Id) 
            {
                if(state == EntityState.Added)
                {
                    _myDTO.View = "Create";
                }
                if (state == EntityState.Modified)
                {
                    _myDTO.View = "Edit";
                }
                _myDTO.Message = "The certificate Id was compromised. The request could not be completed for security reasons. Please try again later.";
                _myDTO.Certificate = certificate;
                return _myDTO;
            }
            if(ModelState.IsValid)
            {
                _myDTO.Message = "The requested certificate has been added successfully.";
                if (state == EntityState.Modified)
                {
                    _myDTO.Message = "The requested certificate has been updated successfully.";
                } 
                if (state == EntityState.Modified && !await _unit.Certificate.Exists(certificate.Id))
                {                   
                    _myDTO.Message = "The requested certificate could not be found. Please try again later.";                   
                }                
                await _unit.SaveAsync();
                _myDTO.View = "Index";
                _myDTO.Certificate = certificate;
                return _myDTO;
            }
            else
            {
                if (state == EntityState.Added)
                {
                    _myDTO.View = "Create";
                }
                if (state == EntityState.Modified)
                {
                    _myDTO.View = "Edit";
                }
                _myDTO.Message = "Invalid entries. Please try again later.";
                _myDTO.Certificate = certificate;
            }           
            return _myDTO;            
        }

        public async Task<MyDTO> GetForDelete(int id)
        {
            _myDTO.View = "Delete";
            if (id == null || _db.Certificates == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested certificate could not be found. Please try again later.";
                return _myDTO;
            }
            _myDTO.Certificate = await _unit.Certificate.GetAsync(id);
            if (_myDTO.Certificate == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested certificate could not be found. Please try again later.";
            }
            return _myDTO;
        }

        public async Task<MyDTO> Delete(int id)
        {
            _myDTO.View = "Index";
            _myDTO.Message = "The requested certificate has been deleted successfully.";
            if (!await _unit.Certificate.Exists(id))
            {                
                _myDTO.Message = "The requested certificate could not be found. Please try again later.";
                return _myDTO;
            }            
            _myDTO.Certificate = await _unit.Certificate.GetAsync(id);
            _unit.Certificate.Delete(_myDTO.Certificate);
            await _unit.SaveAsync();
            return _myDTO;
        }        
    }   
}

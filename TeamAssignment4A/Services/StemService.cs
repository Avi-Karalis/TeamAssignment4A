using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public class StemService : ControllerBase, IStemService
    {
        private UnitOfWork _unit;
        private WebAppDbContext _db;
        private MyDTO _myDTO;
        public StemService(UnitOfWork unit, WebAppDbContext db)
        {
            _unit = unit;
            _db = db;
            _myDTO = new MyDTO();
        }
        public async Task<MyDTO> GetStem(int id)
        {
            if ((id == null || _db.Stems == null) || await _unit.Stem.GetAsync(id) == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested stem could not be found. Please try again later.";
            }
            else
            {
                _myDTO.View = "Details";
                _myDTO.Stem = await _unit.Stem.GetAsync(id);
            }
            return _myDTO;
        }
        public async Task<ICollection<Stem>?> GetAllStems()
        {
            return await _unit.Stem.GetAllAsync();
        }

        public async Task<MyDTO> GetForUpdate(int id)
        {
            _myDTO.View = "Edit";
            if (id == null || _db.Stems == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested stem could not be found. Please try again later.";
                return _myDTO;
            }
            _myDTO.Stem = await _unit.Stem.GetAsync(id);
            if (_myDTO.Stem == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested Stem could not be found. Please try again later.";
            }
            return _myDTO;
        }

        public async Task<MyDTO> AddOrUpdateStem(int id, [Bind("Id,Question,OptionA,OptionB,OptionC,OptionD,CorrectAnswer,TopicDescription")] Stem stem)
        {
            EntityState state = _unit.Stem.AddOrUpdate(stem);
            if (id != stem.Id)
            {
                if (state == EntityState.Added)
                {
                    _myDTO.View = "Create";
                }
                if (state == EntityState.Modified)
                {
                    _myDTO.View = "Edit";
                }
                _myDTO.Message = "The stem Id was compromised. The request could not be completed due to security reasons. Please try again later.";
                _myDTO.Stem = stem;
                return _myDTO;
            }
            if (ModelState.IsValid)
            {
                _myDTO.Message = "The requested stem has been added successfully.";
                if (state == EntityState.Modified)
                {
                    _myDTO.Message = "The requested stem has been updated successfully.";
                }
                if (state == EntityState.Modified && !await _unit.Stem.Exists(stem.Id))
                {
                    _myDTO.Message = "The requested Stem could not be found. Please try again later.";
                }
                await _unit.SaveAsync();
                _myDTO.View = "Index";
                _myDTO.Stems = await _unit.Stem.GetAllAsync();
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
                _myDTO.Stem = stem;
            }
            return _myDTO;
        }

        public async Task<MyDTO> GetForDelete(int id)
        {
            _myDTO.View = "Delete";
            if (id == null || _db.Stems == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested stem could not be found. Please try again later.";
                return _myDTO;
            }
            _myDTO.Stem = await _unit.Stem.GetAsync(id);
            if (_myDTO.Stem == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested stem could not be found. Please try again later.";
            }
            return _myDTO;
        }        

        public async Task<MyDTO> Delete(int id)
        {
            _myDTO.View = "Index";
            _myDTO.Message = "The requested stem has been deleted successfully.";
            if (!await _unit.Stem.Exists(id))
            {
                _myDTO.Message = "The requested stem could not be found. Please try again later.";
                return _myDTO;
            }
            _myDTO.Stem = await _unit.Stem.GetAsync(id);
            _unit.Stem.Delete(_myDTO.Stem);
            await _unit.SaveAsync();
            _myDTO.Stems = await _unit.Stem.GetAllAsync();
            return _myDTO;
        }
    }
}

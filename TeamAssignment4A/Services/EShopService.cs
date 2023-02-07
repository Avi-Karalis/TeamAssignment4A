using AutoMapper;
using TeamAssignment4A.Data;
using TeamAssignment4A.Models;

namespace TeamAssignment4A.Services
{
    public class EShopService
    {
        private WebAppDbContext _db;
        private UnitOfWork _unit;
        private readonly IMapper _mapper;
        private MyDTO _myDTO;
        public EShopService(WebAppDbContext db, UnitOfWork unit, IMapper mapper)
        {
            _db = db;
            _unit = unit;
            _mapper = mapper;
            _myDTO = new MyDTO();
        }

        public async Task<MyDTO> Get(int id)
        {
            if (id == null || _db.Certificates == null || await _unit.Certificate.GetAsync(id) == null)
            {
                _myDTO.View = "Index";
                _myDTO.Message = "The requested certificate could not be found. Please try again later.";
                _myDTO.Certificates = await _unit.Certificate.GetAllAsync();
            }
            else
            {
                _myDTO.View = "Details";
                _myDTO.Certificate = await _unit.Certificate.GetAsync(id);
            }
            return _myDTO;
        }


        public async Task<IEnumerable<Certificate>?> GetAll()
        {
            return await _unit.Certificate.GetAllAsync();
        }
    }
}

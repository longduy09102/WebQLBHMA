using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//-------------------------------------
using QLBHMARepository.DTO;

namespace QLBHMARepository.BLL
{
    public interface IHoaDonRepository
    {
        Task<List<HoaDonOutput>> GetAll();
        Task<List<HoaDonOutput>> GetByKhachHang(string value);
        Task<List<HoaDonVaHoaDonChiTietOutput>> GetIncludeHoaDonChiTiet();
        //CRUD:(Creat,Read,Update,Delete)
        Task<HoaDonOutput> GetById(int id);
        Task<HoaDonInput> Creat(HoaDonInput input);
        Task Update(HoaDonInput input);
        Task Delete(int id);
    }
}

using QLBHMARepository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBHMARepository.BLL
{
    public interface IHoaDonChiTietRepository
    {
        Task<List<HoaDonChiTietOutput>> GetAll();
        Task<List<HoaDonChiTietOutput>> GetByHangHoaID(int id);
        //CRUD:(Creat,Read,Update,Delete)
        Task<List<HoaDonChiTietOutput>> GetByHoaDonID(int id);
        Task<HoaDonChiTietInput> Creat(HoaDonChiTietInput input);
        Task Update(HoaDonChiTietInput input);
        Task DeleteItem(int HoaDonID, int HangHoaID);
        Task DeleteAll(int id);
    }
}

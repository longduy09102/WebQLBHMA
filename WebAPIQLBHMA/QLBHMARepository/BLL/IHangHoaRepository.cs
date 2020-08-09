using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//----------------------------------------
using QLBHMARepository.DTO;

namespace QLBHMARepository.BLL
{
    public interface IHangHoaRepository
    {
        Task<List<HangHoaOutput>> GetAll();

        Task<List<HangHoaOutput>> GetByName(string value);

        Task<PagedOutput<HangHoaOutput>> GetOnePage(PagedInput input);

        Task<PagedOutput<HangHoaOutput>> GetOnePage(int pageSize, int pageIndex);

        Task<PagedOutput<HangHoaOutput>> GetOnePageByLoaiId(pagedInputByLoaiId input);

        Task<PagedOutput<HangHoaOutput>> GetOnePageByLoaiId(int pageSize, int pageIndex, int Id);

        //CRUD:(Creat,Read,Update,Delete)

        Task<HangHoaInput> GetById(int id);

        Task<HangHoaInput> Creat(HangHoaInput input);

        Task Update(HangHoaInput input);

        Task Delete(int id);
    }
}

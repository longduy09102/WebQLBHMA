using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//---------------------------------------
using QLBHMARepository.DTO;

namespace QLBHMARepository.BLL
{
    public interface ILoaiRepository
    {
        Task<List<LoaiOutput>> GetAll();
        Task<List<LoaiOutput>> GetByName(string value);
        Task<List<LoaiVaHangHoaOutput>> GetIncludeHangHoa();
        //CRUD:(Creat,Read,Update,Delete)
        Task<LoaiOutput> GetById(int id);
        Task<LoaiInput> Creat(LoaiInput input);
        Task Update(LoaiInput input);
        Task Delete(int id);
    }
}

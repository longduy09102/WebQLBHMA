using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//----------------------------------
using QLBHMARepository.DTO;

namespace QLBHMARepository.BLL
{
    public interface IChungLoaiRepository
    {
        Task<List<ChungLoaiOutput>> GetAll();

        Task<List<ChungLoaiOutput>> GetByName(string value);

        //CRUD:(Creat,Read,Update,Delete)

        Task<ChungLoaiOutput> GetById(int id);

        Task<ChungLoaiInput> Creat(ChungLoaiInput input);

        Task Update(ChungLoaiInput input);

        Task Delete(int id);
    }
}

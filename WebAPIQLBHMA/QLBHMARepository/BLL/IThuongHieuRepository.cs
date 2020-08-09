using QLBHMARepository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBHMARepository.BLL
{
    public interface IThuongHieuRepository
    {
        Task<List<ThuongHieuOutput>> GetAll();
        Task<List<ThuongHieuOutput>> GetByName(string value);
        //CRUD:(Creat,Read,Update,Delete)
        Task<ThuongHieuOutput> GetById(int id);
        Task<ThuongHieuInput> Creat(ThuongHieuInput input);
        Task Update(ThuongHieuInput input);
        Task Delete(int id);
    }
}

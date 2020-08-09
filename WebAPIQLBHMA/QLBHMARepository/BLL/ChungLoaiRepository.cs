using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//----------------------------------------
using QLBHMARepository.DTO;
using QLBHMARepository.DAL;
using System.Data.Entity;

namespace QLBHMARepository.BLL
{
    public class ChungLoaiRepository : IChungLoaiRepository
    {
        QLBHMADbContext _db;
        
        internal ChungLoaiRepository(QLBHMADbContext db)
        {
            _db = db;
        }

        public async Task<List<ChungLoaiOutput>> GetAll()
        {
            try
            {
                var items = await _db.ChungLoais
                    .Select(p => new ChungLoaiOutput
                    {
                        chungLoaiEntity=p
                    })
                    .ToListAsync();
                return items;
            }
            catch (Exception)
            {

                throw new Exception("Không truy cập được dữ liệu");
            }
        }

        public async Task<List<ChungLoaiOutput>> GetByName(string value)
        {
            try
            {
                var items = await _db.ChungLoais
                    .Where(p => p.Ten.Contains(value))
                    .Select(p => new ChungLoaiOutput
                    {
                        chungLoaiEntity=p
                    })
                    .ToListAsync();
                return items;
            }
            catch (Exception)
            {

                throw new Exception("Không truy cập được dữ liệu");
            }
        }

        public async Task<ChungLoaiOutput> GetById(int id)
        {
            try
            {
                var items = await _db.ChungLoais
                    .Where(p => p.Id == id)
                    .Select(p => new ChungLoaiOutput
                    {
                        chungLoaiEntity=p
                    })
                    .SingleOrDefaultAsync();
                return items;
            }
            catch (Exception ex)
            {

                throw new Exception($"Không truy cập được dữ liệu. Lý do:{ex.Message}");
            }
        }

        public async Task<ChungLoaiInput> Creat(ChungLoaiInput input)
        {
            try
            {
                int d1 = await _db.ChungLoais.CountAsync(p => p.Ten == input.Ten);
                if (d1 > 0) throw new Exception($"Tên ='{input.Ten}' đã có rồi.");
                var entity = new ChungLoai();
                ConvertDTOToEntity(input, entity);
                _db.ChungLoais.Add(entity);
                await _db.SaveChangesAsync();
                input.Id = entity.Id;
                return input;

            }
            catch (Exception ex)
            {

                throw new Exception($"Thêm mới không thành công. Lý do:{ex.Message}");
            }
        }

        public async Task Update(ChungLoaiInput input)
        {
            try
            {
                ChungLoai entity = await _db.ChungLoais.FindAsync(input.Id);
                if (entity == null) throw new Exception($"Chủng loại ID={input.Id} không tồn tại");
                string errMsg = "";
                int d = await _db.ChungLoais.CountAsync(p => p.Id != input.Id && p.Ten == input.Ten);
                if (d > 0) errMsg = $"Tên ='{input.Ten}' đã có rồi.";
                if (errMsg != "") throw new Exception(errMsg);
                ConvertDTOToEntity(input, entity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Thêm không thành công. {ex.Message}");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                ChungLoai entity = await _db.ChungLoais.FindAsync(id);
                if (entity == null) throw new Exception($"Chủng loại ID= {id} không tồn tại.");
                _db.ChungLoais.Remove(entity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                int d = await _db.Loais.CountAsync(p => p.ChungLoaiID == id);
                if (d > 0)
                    throw new Exception($"Không xóa được vì có {d} loại phụ thuộc");
                else
                    throw new Exception($"Không xóa được. Lý do:{ex.Message}");
            }
        }
        #region Phương thức sử dụng cục bộ
        private void ConvertDTOToEntity(ChungLoaiInput input,ChungLoai entity)
        {
            entity.Ten = input.Ten;
        }
        #endregion
    }
}

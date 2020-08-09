using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//-----------------------------------------------
using QLBHMARepository.DTO;
using QLBHMARepository.DAL;
using System.Data.Entity;

namespace QLBHMARepository.BLL
{
    public class ThuongHieuRepository : IThuongHieuRepository
    {
        QLBHMADbContext _db;
        internal ThuongHieuRepository(QLBHMADbContext db)
        {
            _db = db;
        }

        public async Task<List<ThuongHieuOutput>> GetAll()
        {
            try
            {
                var items = await _db.ThuongHieux
                    .Select(p => new ThuongHieuOutput
                    {
                        thuongHieuEntity = p
                    })
                    .ToListAsync();
                return items;
            }
            catch (Exception)
            {
                throw new Exception("Không truy cập được dữ liệu.");
            }
        }

        public async Task<List<ThuongHieuOutput>> GetByName(string value)
        {
            try
            {
                var items = await _db.ThuongHieux
                    .Where(p => p.Ten.Contains(value))
                    .Select(p => new ThuongHieuOutput
                    {
                        thuongHieuEntity = p
                    })
                    .ToListAsync();
                return items;
            }
            catch (Exception)
            { 
                throw new Exception("Không truy cập được dữ liệu.");
            }
        }

        public async Task<ThuongHieuOutput> GetById(int id)
        {
            try
            {
                var items = await _db.ThuongHieux
                    .Where(p => p.ID == id)
                    .Select(p => new ThuongHieuOutput
                    {
                        thuongHieuEntity = p
                    })
                    .SingleOrDefaultAsync();
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception($"Không truy cập được dữ liệu. Lý do:{ex.Message}");
            };
        }

        public async Task<ThuongHieuInput> Creat(ThuongHieuInput input)
        {
            try
            {
                int d1 = await _db.ThuongHieux.CountAsync(p => p.Ten == input.Ten);
                if (d1 > 0) throw new Exception($"Tên ='{input.Ten}' đã có rồi.");
                var entity = new ThuongHieu();
                ConvertDTOToEntity(input, entity);
                _db.ThuongHieux.Add(entity);
                await _db.SaveChangesAsync();
                input.ID = entity.ID;
                return input;
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Thêm mới không thành công. Lý do:{ex.Message}");
            }
        }

        public async Task Update(ThuongHieuInput input)
        {
            try
            {
                ThuongHieu entity = await _db.ThuongHieux.FindAsync(input.ID);
                if (entity == null) throw new Exception($"Thương hiệu ID={input.ID} không tồn tại.");
                string errMsg = "";
                int d = await _db.ThuongHieux.CountAsync(p => p.ID != input.ID && p.Ten == input.Ten);
                if (d > 0) errMsg = $"Tên ='{input.Ten}' đã có rồi.";
                if (errMsg != "") throw new Exception(errMsg);
                ConvertDTOToEntity(input, entity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Thêm không thành công. Lý do:{ex.Message}");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                ThuongHieu entity = await _db.ThuongHieux.FindAsync(id);
                if (entity == null) throw new Exception($"Thương hiệu ID={id} không tồn tại.");
                _db.ThuongHieux.Remove(entity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                int d = await _db.HangHoas.CountAsync(p => p.ThuongHieuID == id);
                if (d > 0)
                    throw new Exception($"Không xóa được vì có {d} hàng hóa phụ thuộc.");
                else
                    throw new Exception($"Không xóa được. Lý do:{ex.Message}");
                throw;
            }
        }

        #region Phương thức sử dụng cục bộ
        private void ConvertDTOToEntity(ThuongHieuInput input,ThuongHieu entity)
        {
            entity.Ten = input.Ten;
            entity.MoTa = input.MoTa;
            entity.TenHinh = input.TenHinh;
        }
        #endregion
    }
}

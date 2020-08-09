using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//-------------------------------------------
using QLBHMARepository.DTO;
using QLBHMARepository.DAL;
using System.Data.Entity;

namespace QLBHMARepository.BLL
{
    public class LoaiRepository : ILoaiRepository
    {
        QLBHMADbContext _db;
        internal LoaiRepository(QLBHMADbContext db)
        {
            _db = db;
        }

        public async Task<List<LoaiOutput>> GetAll()
        {
            try
            {
                var items = await _db.Loais
                    .Select(p => new LoaiOutput
                    {
                        loaiEntity=p
                    })
                    .ToListAsync();
                return items;
            }
            catch (Exception)
            {

                throw new Exception("Không truy cập được dữ liệu.");
            }
        }

        public async Task<List<LoaiOutput>> GetByName(string value)
        {
            try
            {
                var items = await _db.Loais
                    .Where(p => p.Ten.Contains(value))
                    .Select(p => new LoaiOutput
                    {
                        loaiEntity=p
                    })
                    .ToListAsync();
                return items;
            }
            catch (Exception)
            {

                throw new Exception("Không truy cập được dữ liệu.");
            }
        }

        public async Task<List<LoaiVaHangHoaOutput>>GetIncludeHangHoa()
        {
            try
            {
                var items = await _db.Loais
                    .Select(p => new LoaiVaHangHoaOutput
                    { 
                        loaiEntity=p,
                        TongSoMatHang = p.HangHoas.Count,
                        HangHoas = p.HangHoas.Select(h => new LoaiVaHangHoaOutput.HangHoa
                        {
                            ID = h.ID,
                            MaSo = h.MaSo,
                            Ten = h.Ten,
                            GiaBan = h.GiaBan
                        })
                        .ToList()
                    })
                    .ToListAsync();
                return items;
            }
            catch (Exception ex)
            {

                throw new Exception($"Không truy cập được dữ liệu. Lý do:{ex.Message}");
            }
        }

        public async Task<LoaiOutput> GetById(int id)
        {
            try
            {
                var items = await _db.Loais
                    .Where(p => p.ID == id)
                    .Select(p => new LoaiOutput
                    {
                        loaiEntity=p
                    })
                    .SingleOrDefaultAsync();
                return items;
            }
            catch (Exception ex)
            {

                throw new Exception($"Không truy cập được dữ liệu. Lý do:{ex.Message}");
            }
        }

        public async Task<LoaiInput> Creat(LoaiInput input)
        {
            try
            {
                int d1 = await _db.Loais.CountAsync(p => p.MaSo == input.MaSo);
                if (d1 > 0) throw new Exception($"Mã số ='{input.MaSo}' đã có rồi.");
                var entity = new Loai();
                ConvertDTOToEntity(input, entity);
                _db.Loais.Add(entity);
                await _db.SaveChangesAsync();
                input.ID = entity.ID;
                return input; 
            }
            catch (Exception ex)
            {

                throw new Exception($"Thêm mới không thành công. Lý do:{ex.Message}");
            }
        }

        public async Task Update(LoaiInput input)
        {
            try
            {
                Loai entity = await _db.Loais.FindAsync(input.ID);
                if (entity == null) throw new Exception($"Loại ID={input.ID} không tồn tại.");
                string errMsg = "";
                int d = await _db.Loais.CountAsync(p => p.ID != input.ID && p.MaSo == input.MaSo);
                if (d > 0) errMsg = $"Mã số ='{input.MaSo}' đã có rồi.";
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
                Loai entity = await _db.Loais.FindAsync(id);
                if (entity == null) throw new Exception($"Loại ID={id} không tồn tại.");
                _db.Loais.Remove(entity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                int d = await _db.HangHoas.CountAsync(p => p.LoaiID == id);
                if (d > 0)
                    throw new Exception($"Không xóa được vì có {d} hàng hóa phụ thuộc");
                else
                    throw new Exception($"Không xóa được. Lý do:{ex.Message}");
            }
        }

        #region Phương thức sử dụng cục bộ
        private void ConvertDTOToEntity(LoaiInput input,Loai entity)
        {
            entity.MaSo = input.MaSo;
            entity.Ten = input.Ten;
            entity.ChungLoaiID = input.ChungLoaiID;
        }
        #endregion
    }
}

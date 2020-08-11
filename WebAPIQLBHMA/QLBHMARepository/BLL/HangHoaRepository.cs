using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//---------------------------------------------------
using QLBHMARepository.DAL;
using QLBHMARepository.DTO;
using System.Data.Entity;
using System.Web.UI;

namespace QLBHMARepository.BLL
{
    public class HangHoaRepository:IHangHoaRepository
    {
        QLBHMADbContext _db;
        
        internal HangHoaRepository(QLBHMADbContext db)
        {
            _db = db;
        }

        public async Task<List<HangHoaOutput>> GetAll()
        {
            try
            {
                var items = await _db.HangHoas
                    .Include(p => p.Loai)
                    .Select(p => new HangHoaOutput
                    {
                        hangHoaEntity = p,
                        loaiEntity = p.Loai,
                        thuongHieuEntity=p.ThuongHieu
                    })
                    .ToListAsync();
                return items;
            }
            catch (Exception)
            {

                throw new Exception("Không truy cập dữ được dữ liệu");
            }
        }

        public async Task<List<HangHoaOutput>> GetByName(string Value)
        {
            try
            {
                var items = await _db.HangHoas
                    .Where(p => p.Ten.Contains(Value))
                    .Include(p => p.Loai)
                    .Select(p => new HangHoaOutput
                    {
                        hangHoaEntity = p,
                        loaiEntity = p.Loai,
                        thuongHieuEntity=p.ThuongHieu
                    })
                    .ToListAsync();
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi không truy cập được dữ liệu! {ex.Message}");
            }
        }

        public async Task<PagedOutput<HangHoaOutput>> GetOnePage(PagedInput input)
        {
            var onePageOfData = await GetOnePage(input.PageSize, input.PageIndex);
            return onePageOfData;
        }

        public async Task<PagedOutput<HangHoaOutput>> GetOnePage(int pageSize, int pageIndex)
        {
            try
            {
                int n = (pageIndex - 1) * pageSize;//bỏ qua bao nhiêu sản phẩm
                int totalItems = await _db.HangHoas.CountAsync();

                var hangHoaitems = await _db.HangHoas
                    .OrderByDescending(p => p.ID)
                    .Skip(n)
                    .Take(pageSize)
                    .Include(p => p.Loai)
                    .Select(p => new HangHoaOutput
                    {
                        hangHoaEntity = p,
                        loaiEntity = p.Loai,
                        thuongHieuEntity=p.ThuongHieu
                    })
                    .ToListAsync();
                var onePageOfData = new PagedOutput<HangHoaOutput>
                {
                    Items = hangHoaitems,
                    TotalItemCount = totalItems
                };
                return onePageOfData;
            }
            catch (Exception ex)
            {
                throw new Exception($"Loi khong truy cap du lieu! {ex.Message}");
            }
        }

        public async Task<PagedOutput<HangHoaOutput>> GetOnePageByLoaiId(pagedInputByLoaiId input)
        {
            var onePageOfData = await GetOnePageByLoaiId(input.PageSize, input.PageIndex, input.Id);
            return onePageOfData;
        }

        public async Task<PagedOutput<HangHoaOutput>> GetOnePageByLoaiId(int pageSize, int pageIndex, int Id)
        {
            try
            {
                int n = (pageIndex - 1) * pageSize;
                var hangHoaItems = await _db.HangHoas
                    .Where(p => p.LoaiID == Id)
                    .OrderByDescending(p => p.ID)
                    .Skip(n)
                    .Take(pageSize)
                    .Select(p => new HangHoaOutput
                    {
                        hangHoaEntity = p,
                        loaiEntity = p.Loai,
                        thuongHieuEntity = p.ThuongHieu
                    })
                    .ToListAsync();
                int totalItems = hangHoaItems.Count();
                var onePageOfData = new PagedOutput<HangHoaOutput>
                {
                    Items = hangHoaItems,
                    TotalItemCount = totalItems
                };
                return onePageOfData;
            }
            catch (Exception ex)
            {

                throw new Exception($"Lỗi không truy cập dữ liệu! {ex.Message}");
            }
        }

        public async Task<HangHoaInput> GetById(int id)
        {
            try
            {
                var item = await _db.HangHoas
                    .Where(p => p.ID == id)
                    .Select(p => new HangHoaInput
                    {
                        ID = p.ID,
                        MaSo = p.MaSo,
                        Ten = p.Ten,
                        DonViTinh = p.DonViTinh,
                        GiaBan = p.GiaBan,
                        MoTa = p.MoTa,
                        ThongSoKyThuat = p.ThongSoKyThuat,
                        LoaiID = p.LoaiID.Value,
                    })
                    .SingleOrDefaultAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"Loi khong truy cap du lieu! {ex.Message}");
            }
        }

        public async Task<HangHoaInput> Creat(HangHoaInput input)
        {
            try
            {
                int d1 = await _db.HangHoas.CountAsync(p => p.MaSo == input.MaSo);
                if (d1 > 0) throw new Exception($"Mã số='{input.MaSo}' đã có rồi.");
                var entity = new HangHoa();
                ConvertDTOToEntity(input, entity);
                entity.NgayTao = DateTime.Now;
                _db.HangHoas.Add(entity);
                await _db.SaveChangesAsync();
                input.ID = entity.ID;
                return input;
            }
            catch (Exception ex)
            {

                throw new Exception($"Thêm mới không thành công. Lý do:{ex.Message}");
            }
        }

        public async Task Update(HangHoaInput input)
        {
            try
            {
                HangHoa entity = await _db.HangHoas.FindAsync(input.ID);
                if (entity == null) throw new Exception($"Hàng hóa ID={input.ID} không tồn tại.");
                string errMsg = "";
                int d = await _db.HangHoas.CountAsync(p => p.ID != input.ID && p.MaSo == input.MaSo);
                if (d > 0) errMsg = $"Mã số ='{input.MaSo}' đã có rồi.";
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
                HangHoa entity = await _db.HangHoas.FindAsync(id);
                if (entity == null) throw new Exception($"Hàng hóa ID={id} không tồn tại.");
                _db.HangHoas.Remove(entity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Xóa không thành công. {ex.Message}"); ;
            }
        }

        #region phương thức sử dụng cục bộ
        private void ConvertDTOToEntity(HangHoaInput input, HangHoa entity)
        {
            entity.MaSo = input.MaSo;
            entity.Ten = input.Ten;
            entity.DonViTinh = input.DonViTinh;
            entity.MoTa = input.MoTa;
            entity.ThongSoKyThuat = input.ThongSoKyThuat;
            entity.GiaBan = input.GiaBan;
            entity.LoaiID = input.LoaiID;
            entity.ThuongHieuID = input.ThuongHieuID;
            entity.NgayCapNhat = DateTime.Now;
        }
        #endregion
    }
}

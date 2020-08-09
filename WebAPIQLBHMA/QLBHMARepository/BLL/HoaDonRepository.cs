using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//--------------------------------------
using QLBHMARepository.DAL;
using QLBHMARepository.DTO;
using System.Data.Entity;

namespace QLBHMARepository.BLL
{
    public class HoaDonRepository:IHoaDonRepository
    {
        QLBHMADbContext _db;
        internal HoaDonRepository(QLBHMADbContext db)
        {
            _db = db;
        }

        public async Task<List<HoaDonOutput>> GetAll()
        {
            try
            {
                var items = await _db.HoaDons
                    .Select(p => new HoaDonOutput
                    {
                        hoaDonEntity = p
                    })
                    .ToListAsync();
                return items;
            }
            catch (Exception)
            {

                throw new Exception("Không truy cập được dữ liệu.");
            }
        }

        public async Task<List<HoaDonOutput>> GetByKhachHang(string value)
        {
            try
            {
                var items = await _db.HoaDons
                    .Where(p => p.HoTenKhach.Contains(value))
                    .Select(p => new HoaDonOutput
                    {
                        hoaDonEntity = p
                    })
                    .ToListAsync();
                return items;
            }
            catch (Exception)
            {

                throw new Exception("Không truy cập được dữ liệu.");
            }
        }

        public async Task<List<HoaDonVaHoaDonChiTietOutput>> GetIncludeHoaDonChiTiet()
        {
            try
            {
                var items = await _db.HoaDons
                    .Select(p => new HoaDonVaHoaDonChiTietOutput
                    {
                        hoaDonEntity = p,
                        TongSoHoaDonChiTiet = p.HoaDonChiTiets.Count,
                        HoaDonChiTiets = p.HoaDonChiTiets.Select(h => new HoaDonVaHoaDonChiTietOutput.HoaDonChiTiet
                        {
                            HangHoaID=h.HangHoaID,
                            SoLuong=h.SoLuong,
                            DonGia=h.DonGia,
                            ThanhTien=h.ThanhTien
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

        public async Task<HoaDonOutput> GetById(int id)
        {
            try
            {
                var items = await _db.HoaDons
                    .Where(p => p.ID == id)
                    .Select(p => new HoaDonOutput
                    {
                        hoaDonEntity = p
                    })
                    .SingleOrDefaultAsync();
                return items;
            }
            catch (Exception ex)
            {

                throw new Exception($"Không truy cập được dữ liệu. Lý do:{ex.Message}");
            }
        }

        public async Task<HoaDonInput> Creat(HoaDonInput input)
        {
            try
            {
                int d1 = await _db.HoaDons.CountAsync(p => p.ID == input.ID);
                if (d1 > 0) throw new Exception($"Hóa đơn ID ='{input.ID}' đã có rồi.");
                var entity = new HoaDon();
                ConvertDTOToEntity(input, entity);
                entity.NgayDatHang = DateTime.Now;
                _db.HoaDons.Add(entity);
                await _db.SaveChangesAsync();
                input.ID = entity.ID;
                return input;
            }
            catch (Exception ex)
            {

                throw new Exception($"Thêm mới không thành công. Lý do:{ex.Message}");
            }
        }

        public async Task Update(HoaDonInput input)
        {
            try
            {
                HoaDon entity = await _db.HoaDons.FindAsync(input.ID);
                if (entity == null) throw new Exception($"Hóa đơn ID={input.ID} không tồn tại.");
                ConvertDTOToEntity(input, entity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Hiệu chỉnh không thành công. Lý do:{ex.Message}");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                HoaDon entity = await _db.HoaDons.FindAsync(id);
                if (entity == null) throw new Exception($"Hóa đơn ID={id} không tồn tại.");
                _db.HoaDons.Remove(entity);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                int d = await _db.HoaDonChiTiets.CountAsync(p => p.HoaDonID == id);
                if (d > 0)
                    throw new Exception($"Không xóa được vì có {d} hàng hóa phụ thuộc");
                else
                    throw new Exception($"Không xóa được. Lý do:{ex.Message}");
            }
        }

        #region Phương thức sử dụng cục bộ
        private void ConvertDTOToEntity(HoaDonInput input, HoaDon entity)
        {
            entity.HoTenKhach = input.HoTenKhach;
            entity.DiaChi = input.DiaChi;
            entity.DienThoai = input.DienThoai;
            entity.Email = input.Email;
            entity.TongTien = input.TongTien;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//------------------------------------------
using QLBHMARepository.DAL;
using QLBHMARepository.DTO;
using System.Data.Entity;

namespace QLBHMARepository.BLL
{
    class HoaDonChiTietRepository:IHoaDonChiTietRepository
    {
        QLBHMADbContext _db;
        internal HoaDonChiTietRepository(QLBHMADbContext db)
        {
            _db = db;
        }

        public async Task<List<HoaDonChiTietOutput>> GetAll()
        {
            try
            {
                var items = await _db.HoaDonChiTiets
                    .Select(p => new HoaDonChiTietOutput
                    {
                        hoaDonChiTietEntity = p,
                        hangHoaEntity=p.HangHoa,
                        hoaDonEntity=p.HoaDon
                    })
                    .ToListAsync();
                return items;
            }
            catch (Exception)
            {
                throw new Exception("Không truy cập được dữ liệu.");
            }
        }

        public async Task<List<HoaDonChiTietOutput>> GetByHangHoaID(int id)
        {
            try
            {
                var items = await _db.HoaDonChiTiets
                    .Where(p => p.HangHoaID==id)
                    .Select(p => new HoaDonChiTietOutput
                    {
                        hoaDonChiTietEntity = p,
                        hangHoaEntity=p.HangHoa,
                        hoaDonEntity=p.HoaDon
                    })
                    .ToListAsync();
                return items;
            }
            catch (Exception)
            {
                throw new Exception("Không truy cập được dữ liệu.");
            }
        }

        public async Task<List<HoaDonChiTietOutput>> GetByHoaDonID(int id)
        {
            try
            {
                var items = await _db.HoaDonChiTiets
                    .Where(p => p.HoaDonID == id)
                    .Select(p => new HoaDonChiTietOutput
                    {
                        hoaDonChiTietEntity = p,
                        hangHoaEntity=p.HangHoa,
                        hoaDonEntity=p.HoaDon
                    })
                    .ToListAsync();
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception($"Không truy cập được dữ liệu. Lý do:{ex.Message}");
            };
        }

        public async Task<HoaDonChiTietInput> Creat(HoaDonChiTietInput input)
        {
            try
            {
                var entity = new HoaDonChiTiet();
                ConvertDTOToEntity(input, entity);
                entity.HoaDonID = input.HoaDonID;
                entity.HangHoaID = input.HangHoaID;
                entity.ThanhTien = input.DonGia * input.SoLuong;
                _db.HoaDonChiTiets.Add(entity);
                await _db.SaveChangesAsync();
                
                return input;

            }
            catch (Exception ex)
            {
                throw new Exception($"Thêm mới không thành công. Lý do:{ex.Message}");
            }
        }

        public async Task Update(HoaDonChiTietInput input)
        {
            try
            {
                HoaDonChiTiet entity = await _db.HoaDonChiTiets
                    .Where(p => p.HangHoaID == input.HangHoaID && p.HoaDonID == input.HoaDonID)
                    .SingleOrDefaultAsync();
                if (entity==null) throw new Exception($"Hàng hóa ID={input.HangHoaID} và hóa đơn ID={input.HoaDonID} không tồn tại.");
                else
                {
                    ConvertDTOToEntity(input, entity);
                    await _db.SaveChangesAsync();
                }    
            }
            catch (Exception ex)
            {

                throw new Exception($"Hiệu chỉnh không thành công. Lý do:{ex.Message}");
            }
        }

        public async Task DeleteItem(int HoaDonID,int HangHoaID)
        {
            try
            {
                HoaDonChiTiet entity = await _db.HoaDonChiTiets
                    .Where(p => p.HoaDonID == HoaDonID && p.HangHoaID == HangHoaID)
                    .SingleOrDefaultAsync();
                if (entity == null) throw new Exception($"Hàng hóa ID={HangHoaID} và hóa đơn ID={HoaDonID} không tồn tại.");
                else
                {
                    _db.HoaDonChiTiets.Remove(entity);
                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Xóa không thành công. Lý do:{ex.Message}");
            }
        }

        public async Task DeleteAll(int id)
        {
            try
            {
                List<HoaDonChiTiet> entity = await _db.HoaDonChiTiets
                    .Where(p=>p.HoaDonID==id)
                    .ToListAsync();
                if (entity == null) throw new Exception($"Thương hiệu ID={id} không tồn tại.");
                foreach (var item in entity)
                {
                    _db.HoaDonChiTiets.Remove(item);
                }
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Xóa không thành công. Lý do:{ex.Message}");
            }
        }

        #region Phương thức sử dụng cục bộ
        private void ConvertDTOToEntity(HoaDonChiTietInput input, HoaDonChiTiet entity)
        {
            entity.HoaDonID = input.HoaDonID;
            entity.HangHoaID = input.HangHoaID;
            entity.SoLuong = input.SoLuong;
            entity.DonGia = input.DonGia;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//-----------------------------------------
using QLBHMARepository.DAL;

namespace QLBHMARepository.DTO
{
    public class HoaDonChiTietOutput
    {
        internal HoaDonChiTietOutput()
        {
            hoaDonChiTietEntity = new HoaDonChiTiet();
            hangHoaEntity = new HangHoa();
            hoaDonEntity = new HoaDon();
        }

        internal HoaDonChiTiet hoaDonChiTietEntity { private get; set; }
        internal HangHoa hangHoaEntity { private get; set; }
        internal HoaDon hoaDonEntity { private get; set; }

        public int HoaDonID => hoaDonChiTietEntity.HoaDonID;

        public int HangHoaID => hoaDonChiTietEntity.HangHoaID;

        public int SoLuong => hoaDonChiTietEntity.SoLuong;

        public int DonGia => hoaDonChiTietEntity.DonGia;

        public int ThanhTien => hoaDonChiTietEntity.ThanhTien;

        public string TenHangHoa => hangHoaEntity.Ten;

        public int GiaBan => hangHoaEntity.GiaBan;

        public HoaDonOutput HoaDon => new HoaDonOutput
        {
            hoaDonEntity = this.hoaDonEntity
        };
    }
    public class HoaDonChiTietInput
    {
        [Required(ErrorMessage = "{0} không được để trống")]
        [Display(Name ="Hóa đơn ID")]
        public int HoaDonID { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [Display(Name ="Hàng hóa ID")]
        public int HangHoaID { get; set; }

        [Display(Name ="Số lượng")]
        public int SoLuong { get; set; }

        [Display(Name ="Đơn giá")]
        public int DonGia { get; set; }

        [Display(Name ="Thành tiền")]
        public int ThanhTien { get; set; }
    }
}

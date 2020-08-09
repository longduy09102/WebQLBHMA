using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//-----------------------------
using QLBHMARepository.DAL;
using System.ComponentModel.DataAnnotations;

namespace QLBHMARepository.DTO
{
    public class HoaDonOutput
    {
        internal HoaDonOutput()
        {
            hoaDonEntity = new HoaDon();
        }
        internal HoaDon hoaDonEntity { private get; set; }

        public int ID => hoaDonEntity.ID;

        public System.String NgayDatHang => hoaDonEntity.NgayDatHang.ToString("yyyy-MM-dd");

        public string HoTenKhach => hoaDonEntity.HoTenKhach;

        public string DiaChi => hoaDonEntity.DiaChi;

        public string DienThoai => hoaDonEntity.DienThoai;

        public string Email => hoaDonEntity.Email;

        public int TongTien => hoaDonEntity.TongTien;
    }

    public class HoaDonVaHoaDonChiTietOutput:HoaDonOutput
    {
        public int TongSoHoaDonChiTiet { get; set; }
        public List<HoaDonChiTiet> HoaDonChiTiets { get; set; }
        public class HoaDonChiTiet
        {
            public int HangHoaID { get; set; }

            public int SoLuong { get; set; }

            public int DonGia { get; set; }

            public int ThanhTien { get; set; }
        }
    }

    public class HoaDonInput
    {
        [Display(Name ="ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [MaxLength(50, ErrorMessage = "{0} tối đa là {1} ký tự.")]
        [Display(Name = "Họ tên khách hàng")]
        public string HoTenKhach { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [MaxLength(150, ErrorMessage = "{0} tối đa là {1} ký tự.")]
        [Display(Name = "Địa chỉ khách hàng")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [MaxLength(30, ErrorMessage = "{0} tối đa là {1} ký tự.")]
        [Display(Name = "Số điện thoại")]
        public string DienThoai { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [MaxLength(50, ErrorMessage = "{0} tối đa là {1} ký tự.")]
        [Display(Name = "Địa chỉ Email")]
        public string Email { get; set; }

        [Display(Name = "Tổng tiền")]
        public int TongTien { get; set; }
    }
}

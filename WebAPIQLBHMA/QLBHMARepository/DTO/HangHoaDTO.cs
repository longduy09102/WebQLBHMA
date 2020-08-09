using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//---------------------------------------
using QLBHMARepository.DAL;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QLBHMARepository.DTO
{
    public class HangHoaOutput
    {
        internal HangHoaOutput()
        {
            hangHoaEntity = new HangHoa();
            loaiEntity = new Loai();
            thuongHieuEntity = new ThuongHieu();
        }
        internal HangHoa hangHoaEntity { private get; set; }
        internal Loai loaiEntity { private get; set; }
        internal ThuongHieu thuongHieuEntity { private get; set; }

        public int ID => hangHoaEntity.ID;

        public string MaSo => hangHoaEntity.MaSo;

        public string Ten => hangHoaEntity.Ten;

        public string DonViTinh => hangHoaEntity.DonViTinh;

        public string MoTa => hangHoaEntity.MoTa;

        public string ThongSoKyThuat => hangHoaEntity.ThongSoKyThuat;

        public int? ThuongHieuID => hangHoaEntity.ThuongHieuID;

        public int GiaBan => hangHoaEntity.GiaBan;

        public int? LoaiID => hangHoaEntity.LoaiID;

        public System.String NgayTao => hangHoaEntity.NgayTao.ToString("yyyy-MM-dd");

        public System.String NgayCapNhat => hangHoaEntity.NgayCapNhat.ToString("yyyy-MM-dd");

        public LoaiOutput Loai => new LoaiOutput
        {
            loaiEntity=this.loaiEntity
        };

        public ThuongHieuOutput ThuongHieu => new ThuongHieuOutput
        {
            thuongHieuEntity = this.thuongHieuEntity
        };
     
        public List<string> HinhURLs
        {
            get
            {
                List<string> urls = new List<string>();
                if (!string.IsNullOrEmpty(hangHoaEntity.TenHinh))
                {
                    string Authority = HttpContext.Current.Request.Url.Authority;
                    string ApplicationPath = HttpContext.Current.Request.ApplicationPath;
                    if (ApplicationPath.Length > 1) ApplicationPath += "/";

                    var arrTenHnh = hangHoaEntity.TenHinh.Split(',');

                    if(arrTenHnh.Length>0)
                    {
                        foreach(var tenHinh in arrTenHnh)
                        {
                            urls.Add($"http://{Authority}{ApplicationPath}Photos/{tenHinh}");
                        }    
                    }    
                }
                return urls;
            }
        }
    }
    public class HangHoaInput
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [MaxLength(50, ErrorMessage = "{0} tối đa là {1} ký tự.")]
        [Display(Name = "Mã số")]
        public string MaSo { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [MaxLength(200, ErrorMessage = "{0} tối đa là {1} ký tự.")]
        [Display(Name = "Tên hàng")]
        public string Ten { get; set; }

        [MaxLength(50, ErrorMessage = "{0} tối đa là {1} ký tự.")]
        [Display(Name = "Đơn vị tính")]
        public string DonViTinh { get; set; }

        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        [Display(Name = "Thông số kỹ thuật")]
        public string ThongSoKyThuat { get; set; }

        [Display(Name ="Thương hiệu ID")]
        public int? ThuongHieuID { get; set; }

        [Display(Name ="Tên hình hàng hóa")]
        public string TenHinh { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "{0} phải từ {1} đến {2}")]
        [RegularExpression(@"\d*", ErrorMessage = "{0} phải là số nguyên")]
        [Display(Name = "Giá bán")]
        public int GiaBan { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} phải >=1")]
        [RegularExpression(@"\d*", ErrorMessage = "{0} phải là số nguyên")]
        [Display(Name = "Chủng loại")]
        public int? LoaiID { get; set; }
    }
}

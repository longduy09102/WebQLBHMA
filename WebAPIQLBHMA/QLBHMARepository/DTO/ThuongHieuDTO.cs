using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//--------------------------------------------
using QLBHMARepository.DAL;

namespace QLBHMARepository.DTO
{
    public class ThuongHieuOutput
    {
        internal ThuongHieuOutput()
        {
            thuongHieuEntity = new ThuongHieu();
        } 

        internal ThuongHieu thuongHieuEntity { private get; set; }

        public int ID => thuongHieuEntity.ID;

        public string Ten => thuongHieuEntity.Ten;

        public string MoTa => thuongHieuEntity.MoTa;

        public List<string> HinhURLs
        {
            get
            {
                List<string> urls = new List<string>();
                if (!string.IsNullOrEmpty(thuongHieuEntity.TenHinh))
                {
                    string Authority = HttpContext.Current.Request.Url.Authority;
                    string ApplicationPath = HttpContext.Current.Request.ApplicationPath;
                    if (ApplicationPath.Length > 1) ApplicationPath += "/";

                    var arrTenHnh = thuongHieuEntity.TenHinh.Split(',');

                    if (arrTenHnh.Length > 0)
                    {
                        foreach (var tenHinh in arrTenHnh)
                        {
                            urls.Add($"http://{Authority}{ApplicationPath}Photos/{tenHinh}");
                        }
                    }
                }
                return urls;
            }
        }
    }
    public class ThuongHieuInput
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [MaxLength(50, ErrorMessage = "{0} tối đa là {1} ký tự.")]
        [Display(Name = "Tên thương hiệu")]
        public string Ten { get; set; }

        [Display(Name = "Mô tả thương hiệu")]
        public string MoTa { get; set; }

        [Display(Name = "Tên hình thương hiệu")]
        public string TenHinh { get; set; }
    }
}

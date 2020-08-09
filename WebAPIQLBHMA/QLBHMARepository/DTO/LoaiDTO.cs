using QLBHMARepository.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBHMARepository.DTO
{
    public class LoaiOutput
    {
        internal LoaiOutput()
        {
            loaiEntity = new Loai();
            chungLoaiEntity = new ChungLoai();
        }

        internal Loai loaiEntity { private get; set; }
        internal ChungLoai chungLoaiEntity { private get; set; }

        public int ID => loaiEntity.ID;

        public string MaSo => loaiEntity.MaSo;

        public string Ten => loaiEntity.Ten;

        public int? ChungLoaiID => loaiEntity.ChungLoaiID;

        public ChungLoaiOutput ChungLoai => new ChungLoaiOutput
        {
            chungLoaiEntity = this.chungLoaiEntity
        };
    }
    public class LoaiVaHangHoaOutput:LoaiOutput
    {
        public int TongSoMatHang { get; set; }
        public List<HangHoa> HangHoas { get; set; }
        public class HangHoa
        {
            public int ID { get; set; }
            public string MaSo { get; set; }
            public string Ten { get; set; }
            public int GiaBan { get; set; }
        }
    }
    public class LoaiInput
    {
        [Display(Name ="ID")]
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [MaxLength(10, ErrorMessage = "{0} tối đa là {1} ký tự.")]
        [Display(Name = "Mã số")]
        public string MaSo { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [MaxLength(100, ErrorMessage = "{0} tối đa là {1} ký tự.")]
        [Display(Name = "Tên loại")]
        public string Ten { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} phải >=1")]
        [RegularExpression(@"\d*", ErrorMessage = "{0} phải là số nguyên")]
        [Display(Name = "Chủng loại ID")]
        public int? ChungLoaiID { get; set; }
    }
}

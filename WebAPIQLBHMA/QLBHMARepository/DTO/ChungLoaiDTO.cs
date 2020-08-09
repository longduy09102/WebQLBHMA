using QLBHMARepository.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBHMARepository.DTO
{
    public class ChungLoaiOutput
    {
        internal ChungLoaiOutput()
        {
            chungLoaiEntity = new ChungLoai();
        }

        internal ChungLoai chungLoaiEntity { private get; set; }

        public int Id => chungLoaiEntity.Id;

        public string Ten => chungLoaiEntity.Ten;
    }
    public class ChungLoaiInput
    {
        [Display(Name ="ID")]
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} không được để trống.")]
        [MaxLength(50,ErrorMessage ="{0} tối đa là {1} ký tự.")]
        [Display(Name ="Tên chủng loại")]
        public string Ten { get; set; }
    }
}

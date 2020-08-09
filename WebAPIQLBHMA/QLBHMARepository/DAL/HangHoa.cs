namespace QLBHMARepository.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HangHoa")]
    internal partial class HangHoa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HangHoa()
        {
            HoaDonChiTiets = new HashSet<HoaDonChiTiet>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string MaSo { get; set; }

        [Required]
        [StringLength(200)]
        public string Ten { get; set; }

        [StringLength(50)]
        public string DonViTinh { get; set; }

        public string MoTa { get; set; }

        public string ThongSoKyThuat { get; set; }

        public int? ThuongHieuID { get; set; }

        public string TenHinh { get; set; }

        public int GiaBan { get; set; }

        public int? LoaiID { get; set; }

        public DateTime NgayTao { get; set; }

        public DateTime NgayCapNhat { get; set; }

        public virtual Loai Loai { get; set; }

        public virtual ThuongHieu ThuongHieu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; }
    }
}

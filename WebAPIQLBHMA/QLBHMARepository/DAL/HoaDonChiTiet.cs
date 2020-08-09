namespace QLBHMARepository.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonChiTiet")]
    internal partial class HoaDonChiTiet
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HoaDonID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HangHoaID { get; set; }

        public int SoLuong { get; set; }

        public int DonGia { get; set; }

        public int ThanhTien { get; set; }

        public virtual HangHoa HangHoa { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}

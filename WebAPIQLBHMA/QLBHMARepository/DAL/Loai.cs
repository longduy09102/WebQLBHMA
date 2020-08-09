namespace QLBHMARepository.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Loai")]
    internal partial class Loai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Loai()
        {
            HangHoas = new HashSet<HangHoa>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string MaSo { get; set; }

        [Required]
        [StringLength(100)]
        public string Ten { get; set; }

        public int? ChungLoaiID { get; set; }

        public virtual ChungLoai ChungLoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HangHoa> HangHoas { get; set; }
    }
}

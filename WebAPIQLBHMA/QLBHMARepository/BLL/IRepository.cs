using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBHMARepository.BLL
{
    public interface IRepository : IDisposable
    {
        IChungLoaiRepository ChungLoai { get; }
        ILoaiRepository Loai { get; }
        IHangHoaRepository HangHoa { get; }
        IThuongHieuRepository ThuongHieu { get; }
        IHoaDonRepository HoaDon { get; }
        IHoaDonChiTietRepository HoaDonChiTiet {get;}
    }
}

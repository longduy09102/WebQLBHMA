using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//---------------------------------------
using QLBHMARepository.DAL;

namespace QLBHMARepository.BLL
{
    public class Repository:IRepository
    {
        QLBHMADbContext _db;
        public Repository()
        {
            _db = new QLBHMADbContext();
        }

        public IChungLoaiRepository ChungLoai => new ChungLoaiRepository(_db);
        public ILoaiRepository Loai => new LoaiRepository(_db);
        public IHangHoaRepository HangHoa => new HangHoaRepository(_db);
        public IThuongHieuRepository ThuongHieu => new ThuongHieuRepository(_db);
        public IHoaDonRepository HoaDon => new HoaDonRepository(_db);
        public IHoaDonChiTietRepository HoaDonChiTiet => new HoaDonChiTietRepository(_db);

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

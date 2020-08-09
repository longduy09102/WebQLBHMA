using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//------------------------------------
using QLBHMARepository.BLL;
using QLBHMARepository.DTO;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace QLBHMAWebAPI.Controllers
{
    [RoutePrefix("api/loai")]
    public class LoaiApiController : ApiController
    {
        private readonly ILoaiRepository _repository;

        public LoaiApiController(IRepository repository)
        {
            _repository = repository.Loai;
        }

        #region Đọc tất cả
        //GET: api/loai/doc-danh-sach
        [HttpGet]
        [Route("doc-danh-sach")]
        [ResponseType(typeof(List<LoaiOutput>))]
        public async Task<IHttpActionResult> DocDanhSach()
        {
            try
            {
                var result = await _repository.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Đọc theo tên
        //GET: api/loai/doc-theo-ten
        [HttpGet]
        [Route("doc-theo-ten")]
        [ResponseType(typeof(List<LoaiOutput>))]
        public async Task<IHttpActionResult> DocTheoTen(string value)
        {
            try
            {
                var result = await _repository.GetByName(value);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Đọc hàng hóa theo loại
        //GET: api/loai/doc-hang-hoa-thuoc-loai
        [HttpGet]
        [Route("doc-hang-hoa-thuoc-loai")]
        [ResponseType(typeof(List<LoaiVaHangHoaOutput>))]
        public async Task<IHttpActionResult> DocHangHoaThuocLoai()
        {
            try
            {
                var result = await _repository.GetIncludeHangHoa();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Đọc theo Id
        //POST: api/loai/doc-theo-id
        [HttpPost]
        [Route("doc-theo-id")]
        [ResponseType(typeof(LoaiOutput))]
        public async Task<IHttpActionResult> DocTheoId(int value)
        {
            try
            {
                var result = await _repository.GetById(value);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Thêm
        //POST:api/loai/them-moi
        [Route("them-moi")]
        [ResponseType(typeof(LoaiInput))]
        public async Task<IHttpActionResult> ThemMoi(LoaiInput input)
        {
            try
            {
                var result = await _repository.Creat(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Hiệu chỉnh
        //POST:api/loai/hieu-chinh
        [Route("hieu-chinh")]
        [HttpPost]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> HieuChinh(LoaiInput input)
        {
            try
            {
                await _repository.Update(input);
                return Ok("Hiệu chỉnh thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Xóa
        //POST:api/loai/xoa
        [Route("xoa")]
        [HttpPost]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> Xoa(int id)
        {
            try
            {
                await _repository.Delete(id);
                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}

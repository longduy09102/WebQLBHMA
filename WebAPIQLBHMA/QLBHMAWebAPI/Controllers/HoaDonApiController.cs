using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//--------------------------------
using QLBHMARepository.BLL;
using QLBHMARepository.DTO;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace QLBHMAWebAPI.Controllers
{
    [RoutePrefix("api/hoa-don")]
    public class HoaDonApiController : ApiController
    {
        private readonly IHoaDonRepository _repository;

        public HoaDonApiController(IRepository repository)
        {
            _repository = repository.HoaDon;
        }

        #region Đọc tất cả
        //GET: api/hoa-don/doc-danh-sach
        [HttpGet]
        [Route("doc-danh-sach")]
        [ResponseType(typeof(List<HoaDonOutput>))]
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
        //GET: api/hoa-don/doc-theo-ten-khach-hang
        [HttpGet]
        [Route("doc-theo-ten-khach-hang")]
        [ResponseType(typeof(List<HoaDonOutput>))]
        public async Task<IHttpActionResult> DocTheoTen(string value)
        {
            try
            {
                var result = await _repository.GetByKhachHang(value);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Đọc hàng hóa theo loại
        //GET: api/hoa-don/doc-hoa-don-chi-tiet-thuoc-hoa-don
        [HttpGet]
        [Route("doc-hoa-don-chi-tiet-thuoc-hoa-don")]
        [ResponseType(typeof(List<HoaDonVaHoaDonChiTietOutput>))]
        public async Task<IHttpActionResult> DocHangHoaThuocHoaDon()
        {
            try
            {
                var result = await _repository.GetIncludeHoaDonChiTiet();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Đọc theo Id
        //POST: api/hoa-don/doc-theo-id
        [HttpPost]
        [Route("doc-theo-id")]
        [ResponseType(typeof(HoaDonOutput))]
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
        //POST:api/hoa-don/them-moi
        [Route("them-moi")]
        [ResponseType(typeof(HoaDonInput))]
        public async Task<IHttpActionResult> ThemMoi(HoaDonInput input)
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
        //POST:api/hoa-don/hieu-chinh
        [Route("hieu-chinh")]
        [HttpPost]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> HieuChinh(HoaDonInput input)
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
        //POST:api/hoa-don/xoa
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

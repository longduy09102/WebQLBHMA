using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//---------------------------------------
using QLBHMARepository.BLL;
using QLBHMARepository.DTO;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace QLBHMAWebAPI.Controllers
{
    [RoutePrefix("api/hoa-don-chi-tiet")]
    public class HoaDonChiTietApiController : ApiController
    {
        private readonly IHoaDonChiTietRepository _repository;

        public HoaDonChiTietApiController(IRepository repository)
        {
            _repository = repository.HoaDonChiTiet;
        }

        #region Đọc tất cả
        //GET: api/hoa-don-chi-tiet/doc-danh-sach
        [HttpGet]
        [Route("doc-danh-sach")]
        [ResponseType(typeof(List<HoaDonChiTietOutput>))]
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

        #region Đọc theo hàng hóa id
        //GET: api/loai/doc-theo-hang-hoa-id
        [HttpGet]
        [Route("doc-theo-hang-hoa-id")]
        [ResponseType(typeof(List<HoaDonChiTietOutput>))]
        public async Task<IHttpActionResult> DocTheoHangHoaID(int id)
        {
            try
            {
                var result = await _repository.GetByHangHoaID(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Đọc theo hóa đơn id
        //GET: api/loai/doc-theo-hoa-don-id
        [HttpGet]
        [Route("doc-theo-hoa-don-id")]
        [ResponseType(typeof(List<HoaDonChiTietOutput>))]
        public async Task<IHttpActionResult> DocTheoHoaDonID(int id)
        {
            try
            {
                var result = await _repository.GetByHoaDonID(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Thêm
        //POST:api/hoa-don-chi-tiet/them-moi
        [HttpPost]
        [Route("them-moi")]
        [ResponseType(typeof(HoaDonChiTietInput))]
        public async Task<IHttpActionResult> ThemMoi(HoaDonChiTietInput input)
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
        //POST:api/hoa-don-chi-tiet/hieu-chinh
        [HttpPost]
        [Route("hieu-chinh")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> HieuChinh(HoaDonChiTietInput input)
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

        #region Xóa item
        //POST:api/hoa-don-chi-tiet/xoa-item
        [HttpPost]
        [Route("xoa-item")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> XoaItem(int HoaDonID,int HangHoaID)
        {
            try
            {
                await _repository.DeleteItem(HoaDonID, HangHoaID);
                return Ok("Xóa item thành công");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Xóa tất cả
        //POST:api/hoa-don-chi-tiet/xoa-tat-ca
        [HttpPost]
        [Route("xoa-tat-ca")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> Xoa(int id)
        {
            try
            {
                await _repository.DeleteAll(id);
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

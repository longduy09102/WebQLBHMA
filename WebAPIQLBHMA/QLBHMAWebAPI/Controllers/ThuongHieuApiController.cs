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
    [RoutePrefix("api/thuong-hieu")]
    public class ThuongHieuApiController : ApiController
    {
        private readonly IThuongHieuRepository _repository;

        public ThuongHieuApiController(IRepository repository)
        {
            _repository = repository.ThuongHieu;
        }

        #region Đọc tất cả
        //GET: api/thuong-hieu/doc-danh-sach
        [HttpGet]
        [Route("doc-danh-sach")]
        [ResponseType(typeof(List<ThuongHieuOutput>))]
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
        //GET:api/thuong-hieu/doc-theo-ten
        [HttpGet]
        [Route("doc-theo-ten")]
        [ResponseType(typeof(List<ThuongHieuOutput>))]
        public async Task<IHttpActionResult> DocTheoTen(string Value)
        {
            try
            {
                var result = await _repository.GetByName(Value);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Đọc theo id
        //POST: api/thuong-hieu/doc-theo-id
        [HttpPost]
        [Route("doc-theo-id")]
        [ResponseType(typeof(ThuongHieuOutput))]
        public async Task<IHttpActionResult> DocTheoId(int id)
        {
            try
            {
                var result = await _repository.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Thêm
        //POST: api/hang-hao/them-moi
        [HttpPost]
        [Route("them-moi")]
        [ResponseType(typeof(ThuongHieuInput))]
        public async Task<IHttpActionResult> ThemMoi(ThuongHieuInput input)
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
        //POST: api/thuong-hieu/hieu-chinh
        [HttpPost]
        [Route("hieu-chinh")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> HieuChinh(ThuongHieuInput input)
        {
            try
            {
                await _repository.Update(input);
                return Ok("Hiệu chỉnh thành công");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Xóa
        //POST: api/thuong-hieu/xoa
        [HttpPost]
        [Route("xoa")]
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
                throw;
            }
        }
        #endregion
    }
}

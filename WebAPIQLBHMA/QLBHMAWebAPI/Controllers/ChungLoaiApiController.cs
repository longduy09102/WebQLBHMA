using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//-----------------------------
using QLBHMARepository.DTO;
using QLBHMARepository.BLL;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace QLBHMAWebAPI.Controllers
{
    [RoutePrefix("api/chung-loai")]
    public class ChungLoaiApiController : ApiController
    {
        private readonly IChungLoaiRepository _repository;

        public ChungLoaiApiController(IRepository repository)
        {
            _repository = repository.ChungLoai;
        }

        #region Đọc tất cả
        //GET: api/chung-loai/doc-danh-sach
        [HttpGet]
        [Route("doc-danh-sach")]
        [ResponseType(typeof(List<ChungLoaiOutput>))]
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
        //GET: api/chung-loai/doc-theo-ten
        [Route("doc-theo-ten")]
        [HttpGet]
        [ResponseType(typeof(List<ChungLoaiOutput>))]
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

        #region Đọc theo Id
        //POST: api/chung-loai/doc-theo-id
        [Route("doc-theo-id")]
        [HttpPost]
        [ResponseType(typeof(ChungLoaiOutput))]
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

        #region thêm
        //POST:api/chung-loai/them-moi
        [Route("them-moi")]
        [ResponseType(typeof(ChungLoaiInput))]
        public async Task<IHttpActionResult> ThemMoi(ChungLoaiInput input)
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

        #region hiệu chỉnh
        //POST:api/chung-loai/hieu-chinh
        [Route("hieu-chinh")]
        [HttpPost]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> HieuChinh(ChungLoaiInput input)
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

        #region xóa
        //POST:api/chung-loai/xoa
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

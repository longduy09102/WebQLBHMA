using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//------------------------------------------
using QLBHMARepository.BLL;
using QLBHMARepository.DTO;
using System.Web.Http.Description;
using System.Threading.Tasks;



namespace QLBHMAWebAPI.Controllers
{
    [RoutePrefix("api/hang-hoa")]
    public class HangHoaApiController : ApiController
    {
        private readonly IHangHoaRepository _repository;

        public HangHoaApiController(IRepository repository)
        {
            _repository = repository.HangHoa;
        }

        #region Đọc tất cả
        //GET: api/hang-hoa/doc-danh-sach
        [HttpGet]
        [Route("doc-danh-sach")]
        [ResponseType(typeof(List<HangHoaOutput>))]
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
        //GET:api/hang-hoa/doc-theo-ten
        [HttpGet]
        [Route("doc-theo-ten")]
        [ResponseType(typeof(List<HangHoaOutput>))]
        public async Task<IHttpActionResult>DocTheoTen(string Value)
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

        #region Đọc một trang
        //POST:api/hang-hoa/doc-mot-trang
        [HttpPost]
        [Route("doc-mot-trang")]
        [ResponseType(typeof(PagedOutput<HangHoaOutput>))]
        public async Task<IHttpActionResult>DocMotTrang(PagedInput input)
        {
            try
            {
                var result = await _repository.GetOnePage(input);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        public async Task<IHttpActionResult> DocMotTrang(int pageSize, int pageIndex)
        {
            try
            {
                var result = await _repository.GetOnePage(pageSize, pageIndex);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Đọc một trang theo chung loại id
        //POST:api/hang-hoa/doc-mot-trang-theo-loai-id
        [HttpPost]
        [Route("doc-mot-trang-theo-loai-id")]
        [ResponseType(typeof(PagedOutput<HangHoaOutput>))]
        public async Task<IHttpActionResult>DocMotTrangTheoLoaiId(pagedInputByLoaiId input)
        {
            try
            {
                var result = await _repository.GetOnePageByLoaiId(input);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        public async Task<IHttpActionResult> DocMotTrangTheoLoaiId(int pageSize, int pageIndex, int Id)
        {
            try
            {
                var result = await _repository.GetOnePageByLoaiId(pageSize,pageIndex,Id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Đọc theo id
        //POST: api/hang-hoa/doc-theo-id
        [HttpPost]
        [Route("doc-theo-id")]
        [ResponseType(typeof(HangHoaOutput))]
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
        [ResponseType(typeof(HangHoaInput))]
        public async Task<IHttpActionResult> ThemMoi(HangHoaInput input)
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
        //POST: api/hang-hoa/hieu-chinh
        [HttpPost]
        [Route("hieu-chinh")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> HieuChinh(HangHoaInput input)
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
        //POST: api/hang-hoa/xoa
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

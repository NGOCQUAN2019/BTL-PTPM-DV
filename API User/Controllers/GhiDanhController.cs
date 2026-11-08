using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using BLL.Interfaces;
using System.Security.Claims;

namespace API_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "HocVien")] // Cố định toàn bộ Controller này chỉ dành riêng cho Học viên
    public class GhiDanhController : ControllerBase
    {
        private readonly IGhiDanhBusiness _ghiDanhBusiness;

        public GhiDanhController(IGhiDanhBusiness ghiDanhBusiness)
        {
            _ghiDanhBusiness = ghiDanhBusiness;
        }

        // Học viên xem danh sách các phiếu ghi danh của chính mình
        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            // Tùy chọn nâng cao: Bạn có thể lấy ID học viên từ Token để lọc dữ liệu chỉ hiện của cá nhân đó
            return Ok(_ghiDanhBusiness.GetAll());
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var data = _ghiDanhBusiness.GetById(id);
            if (data == null)
                return NotFound(new { message = "Không tìm thấy thông tin ghi danh!" });
            return Ok(data);
        }

        // Chức năng cốt lõi: Học viên tự bấm nút đăng ký một lớp học mới
        [HttpPost("create")]
        public IActionResult Create([FromBody] GhiDanhModel model)
        {
            try
            {
                if (_ghiDanhBusiness.Create(model))
                    return Ok(new { message = "Đăng ký khóa học thành công! Hệ thống đã tự động tạo hóa đơn chờ thanh toán." });

                return BadRequest(new { message = "Đăng ký thất bại hoặc lớp đã đầy!" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
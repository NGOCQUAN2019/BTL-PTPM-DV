using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using BLL.Interfaces;
using System.Security.Claims;
using System;

namespace API_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "HocVien")]
    public class GhiDanhController : ControllerBase
    {
        private readonly IGhiDanhBusiness _ghiDanhBusiness;

        public GhiDanhController(IGhiDanhBusiness ghiDanhBusiness)
        {
            _ghiDanhBusiness = ghiDanhBusiness;
        }

        // ... (các hàm Get giữ nguyên) ...

        [HttpPost("create")]
        public IActionResult Create([FromBody] GhiDanhCreateRequest request)
        {
            try
            {
                var model = new GhiDanhModel();
                model.LopHocId = request.LopHocId;

                // BẮT BUỘC PHẢI CÓ TOKEN HỢP LỆ MỚI CHO ĐI TIẾP
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                    return Unauthorized(new { message = "Token không hợp lệ hoặc thiếu thông tin ID học viên!" });

                model.HocVienId = int.Parse(userIdClaim.Value);

                // Chặn thêm 1 lớp an toàn: ID phải lớn hơn 0
                if (model.HocVienId <= 0)
                    return BadRequest(new { message = $"HocVienId ({model.HocVienId}) từ Token không hợp lệ!" });

                model.TrangThai = "ChoDuyet";

                if (_ghiDanhBusiness.Create(model))
                    return Ok(new { message = "Đăng ký khóa học thành công! Hệ thống đã tự động tạo hóa đơn chờ thanh toán." });

                return BadRequest(new { message = "Đăng ký thất bại hoặc lớp đã đầy!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    // NÉM NÓ XUỐNG CUỐI CÙNG CỦA FILE NÀY (Bên trong namespace nhưng bên ngoài class Controller)
    public class GhiDanhCreateRequest
    {
        public int LopHocId { get; set; }
    }
}
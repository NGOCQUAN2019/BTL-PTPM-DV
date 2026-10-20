using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using BLL.Interfaces;
using System;

namespace APIUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "HocVien")] // Chỉ Học viên đăng nhập mới gọi được
    public class UGhiDanhController : ControllerBase
    {
        private readonly IGhiDanhBusiness _ghiDanhBusiness;

        public UGhiDanhController(IGhiDanhBusiness ghiDanhBusiness)
        {
            _ghiDanhBusiness = ghiDanhBusiness;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] GhiDanhRequest request)
        {
            // Chuyển đổi dữ liệu từ Request sang Model chuẩn
            var model = new GhiDanhModel
            {
                MaHocVien = request.MaHocVien,
                LopHocId = request.LopHocId,
            };

            if (_ghiDanhBusiness.Create(model))
            {
                return Ok(new { message = "Đăng ký thành công! Vui lòng kiểm tra hóa đơn và thanh toán trong vòng 7 ngày." });
            }

            return BadRequest(new { message = "Đăng ký thất bại! Lớp đã đủ sĩ số hoặc bạn đã ghi danh lớp này." });
        }
    }

    public class GhiDanhRequest
    {
        public string MaHocVien { get; set; }
        public int LopHocId { get; set; }
    }
}
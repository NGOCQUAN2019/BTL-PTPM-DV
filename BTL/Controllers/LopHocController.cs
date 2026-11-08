using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using BLL.Interfaces;
using System;

namespace API_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Chốt chặn vòng ngoài: Bắt buộc đăng nhập hệ thống
    public class LopHocController : ControllerBase
    {
        private readonly ILopHocBusiness _classBusiness;

        public LopHocController(ILopHocBusiness classBusiness)
        {
            _classBusiness = classBusiness;
        }

        // Cả Admin và Giáo viên đều được xem danh sách lớp học
        [HttpGet("get-all")]
        [Authorize(Roles = "QuanTri,GiaoVien")]
        public IActionResult GetAll()
        {
            return Ok(_classBusiness.GetAll());
        }

        [HttpGet("get-by-id/{id}")]
        [Authorize(Roles = "QuanTri,GiaoVien")]
        public IActionResult GetById(int id)
        {
            var classData = _classBusiness.GetById(id);
            if (classData == null) return NotFound(new { message = "Không tìm thấy lớp học" });
            return Ok(classData);
        }

        // ĐỘC QUYỀN ADMIN: Chỉ Quản trị mới được phép tạo, xếp giáo viên, sửa, xóa lớp
        [HttpPost("create")]
        [Authorize(Roles = "QuanTri")]
        public IActionResult Create([FromBody] LopHocModel model)
        {
            try
            {
                if (_classBusiness.Create(model))
                    return Ok(new { message = "Thêm lớp học thành công!" });

                return BadRequest(new { message = "Thêm thất bại! Vui lòng kiểm tra lại KhoaHocId và GiaoVienId." });
            }
            catch (Exception ex)
            {
                // Hứng lỗi SQL (ví dụ nhập sai ID khóa học hoặc ID giáo viên không tồn tại)
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "QuanTri")]
        public IActionResult Update([FromBody] LopHocModel model)
        {
            try
            {
                if (_classBusiness.Update(model))
                    return Ok(new { message = "Cập nhật thành công!" });

                return BadRequest(new { message = "Cập nhật thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "QuanTri")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_classBusiness.Delete(id))
                    return Ok(new { message = "Xóa lớp học thành công!" });

                return BadRequest(new { message = "Xóa thất bại! Lớp này có thể đang có học viên ghi danh." });
            }
            catch (Exception ex)
            {
                // Bắt lỗi khóa ngoại nếu cố xóa lớp đã có học viên đăng ký hoặc đã có điểm
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
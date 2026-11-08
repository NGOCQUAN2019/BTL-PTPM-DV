using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;

namespace API_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Chốt chặn vòng ngoài: Bắt buộc đăng nhập hệ thống nội bộ
    public class DiemSoController : ControllerBase
    {
        private readonly IDiemSoBusiness _diemSoBusiness;

        public DiemSoController(IDiemSoBusiness diemSoBusiness)
        {
            _diemSoBusiness = diemSoBusiness;
        }

        // --- NHÓM QUYỀN XEM: Dành cho cả Admin và Giáo viên ---

        [HttpGet("get-all")]
        [Authorize(Roles = "QuanTri,GiaoVien")]
        public IActionResult GetAll()
        {
            return Ok(_diemSoBusiness.GetAll());
        }

        [HttpGet("get-by-id/{id}")]
        [Authorize(Roles = "QuanTri,GiaoVien")]
        public IActionResult GetById(int id)
        {
            var data = _diemSoBusiness.GetById(id);
            if (data == null)
                return NotFound(new { message = "Không tìm thấy dữ liệu điểm này!" });

            return Ok(data);
        }

        // --- NHÓM QUYỀN NHẬP/SỬA: Dành cho cả Admin và Giáo viên ---

        [HttpPost("create")]
        [Authorize(Roles = "QuanTri,GiaoVien")]
        public IActionResult Create([FromBody] DiemSoModel model)
        {
            try
            {
                if (_diemSoBusiness.Create(model))
                    return Ok(new { message = "Nhập điểm thành công!" });

                return BadRequest(new { message = "Nhập điểm thất bại." });
            }
            catch (Exception ex)
            {
                // Hứng lỗi SQL như: trùng điểm của học viên trong cùng kỳ thi, sai mã học viên...
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "QuanTri,GiaoVien")]
        public IActionResult Update([FromBody] DiemSoModel model)
        {
            try
            {
                if (_diemSoBusiness.Update(model))
                    return Ok(new { message = "Cập nhật điểm thành công!" });

                return BadRequest(new { message = "Cập nhật điểm thất bại." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // --- NHÓM QUYỀN XÓA: Độc quyền của Admin ---

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "QuanTri")] // Giáo viên có thể nhập sai và sửa, nhưng XÓA hẳn thì phải do Admin quyết định
        public IActionResult Delete(int id)
        {
            try
            {
                if (_diemSoBusiness.Delete(id))
                    return Ok(new { message = "Xóa điểm thành công!" });

                return BadRequest(new { message = "Xóa điểm thất bại." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Security.Claims;

namespace APIUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "HocVien")] // Cố định chỉ cho phép Học viên truy cập vào các API ở đây
    public class DiemSoController : ControllerBase
    {
        private readonly IDiemSoBusiness _diemSoBusiness;

        public DiemSoController(IDiemSoBusiness diemSoBusiness)
        {
            _diemSoBusiness = diemSoBusiness;
        }

        // Học viên xem bảng điểm cá nhân
        [HttpGet("get-diem-cua-toi")]
        public IActionResult GetDiemCuaToi()
        {
            try
            {

                // Gọi hàm lấy dữ liệu điểm 
                return Ok(_diemSoBusiness.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var data = _diemSoBusiness.GetById(id);
            if (data == null)
                return NotFound(new { message = "Không tìm thấy dữ liệu điểm này!" });

            return Ok(data);
        }
    }
}
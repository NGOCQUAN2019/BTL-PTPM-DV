using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Model;
using System;

namespace APIAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThanhToanController : ControllerBase
    {
        private readonly IThanhToanBusiness _thanhToanBusiness;
        public ThanhToanController(IThanhToanBusiness thanhToanBusiness) => _thanhToanBusiness = thanhToanBusiness;

        [HttpPost("thanh-toan")]
        public IActionResult Create([FromBody] ThanhToanModel model)
        {
            try
            {
                if (_thanhToanBusiness.Create(model))
                    return Ok(new { message = "Thanh toán thành công! Trạng thái Hóa đơn và Ghi danh đã được cập nhật." });

                return BadRequest(new { message = "Thanh toán thất bại." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using System;

namespace API_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "QuanTri")]
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeBusiness _thongKeBusiness;

        public ThongKeController(IThongKeBusiness thongKeBusiness)
        {
            _thongKeBusiness = thongKeBusiness;
        }

        // Dashboard: Thống kê tổng quan về lớp học (số lượng, sĩ số)
        [HttpGet("lop-hoc")]
        public IActionResult GetThongKeLopHoc()
        {
            try
            {
                var data = _thongKeBusiness.GetThongKeLopHoc();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
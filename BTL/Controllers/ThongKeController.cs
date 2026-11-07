using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;

namespace APIAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "QuanTri")] // Chỉ Admin mới được xem báo cáo doanh thu
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeBusiness _thongKeBusiness;

        public ThongKeController(IThongKeBusiness thongKeBusiness)
        {
            _thongKeBusiness = thongKeBusiness;
        }

        [HttpGet("lop-hoc")]
        public IActionResult GetThongKeLopHoc()
        {
            var data = _thongKeBusiness.GetThongKeLopHoc();
            return Ok(data);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BLL.Interfaces;

namespace APIUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "HocVien")] // Cố định chỉ cho phép Học viên
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonBusiness _hoaDonBusiness;

        public HoaDonController(IHoaDonBusiness hoaDonBusiness)
        {
            _hoaDonBusiness = hoaDonBusiness;
        }

        // Học viên truyền ID của mình vào URL để xem hóa đơn
        [HttpGet("get-by-hoc-vien/{hocVienId}")]
        public IActionResult GetByHocVienId(int hocVienId)
        {
            return Ok(_hoaDonBusiness.GetByHocVienId(hocVienId));
        }
    }
}
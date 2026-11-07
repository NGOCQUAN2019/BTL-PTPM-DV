using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;

namespace APIAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonBusiness _hoaDonBusiness;
        public HoaDonController(IHoaDonBusiness hoaDonBusiness) => _hoaDonBusiness = hoaDonBusiness;

        [HttpGet("get-all")]
        public IActionResult GetAll() => Ok(_hoaDonBusiness.GetAll());

        [HttpGet("get-by-hoc-vien/{hocVienId}")]
        public IActionResult GetByHocVienId(int hocVienId) => Ok(_hoaDonBusiness.GetByHocVienId(hocVienId));
    }
}
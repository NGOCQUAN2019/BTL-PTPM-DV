using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class HoaDonBusiness : IHoaDonBusiness
    {
        private readonly IHoaDonRepository _hoaDonRepository;

        public HoaDonBusiness(IHoaDonRepository hoaDonRepository)
        {
            _hoaDonRepository = hoaDonRepository;
        }

        public IEnumerable<HoaDonModel> GetAll()
        {
            return _hoaDonRepository.GetAll();
        }

        public IEnumerable<HoaDonModel> GetByHocVienId(int hocVienId)
        {
            return _hoaDonRepository.GetByHocVienId(hocVienId);
        }
    }
}
using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class HoaDonBusiness : IHoaDonBusiness
    {
        private readonly IHoaDonRepository _res;
        public HoaDonBusiness(IHoaDonRepository res) { _res = res; }
        public IEnumerable<HoaDonModel> GetAll() => _res.GetAll();
        public IEnumerable<HoaDonModel> GetByHocVien(int hocVienId) => _res.GetByHocVien(hocVienId);
    }
}
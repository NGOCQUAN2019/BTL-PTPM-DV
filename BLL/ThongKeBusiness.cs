using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class ThongKeBusiness : IThongKeBusiness
    {
        private readonly IThongKeRepository _thongKeRepository;

        public ThongKeBusiness(IThongKeRepository thongKeRepository)
        {
            _thongKeRepository = thongKeRepository;
        }

        public IEnumerable<ThongKeLopHocModel> GetThongKeLopHoc()
        {
            return _thongKeRepository.GetThongKeLopHoc();
        }
    }
}
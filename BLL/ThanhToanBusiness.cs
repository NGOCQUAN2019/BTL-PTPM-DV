using BLL.Interfaces;
using DAL.Interfaces;
using Model;

namespace BLL
{
    public class ThanhToanBusiness : IThanhToanBusiness
    {
        private readonly IThanhToanRepository _res;

        public ThanhToanBusiness(IThanhToanRepository res)
        {
            _res = res;
        }

        public bool ThemThanhToan(ThanhToanRequest request)
        {
            return _res.Create(request);
        }
    }
}
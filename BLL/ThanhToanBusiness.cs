using BLL.Interfaces;
using DAL.Interfaces;
using Model;

namespace BLL
{
    public class ThanhToanBusiness : IThanhToanBusiness
    {
        private readonly IThanhToanRepository _thanhToanRepository;

        public ThanhToanBusiness(IThanhToanRepository thanhToanRepository)
        {
            _thanhToanRepository = thanhToanRepository;
        }

        public bool Create(ThanhToanModel model)
        {
            return _thanhToanRepository.Create(model);
        }
    }
}
using Model;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IHoaDonBusiness
    {
        IEnumerable<HoaDonModel> GetAll();
        IEnumerable<HoaDonModel> GetByHocVienId(int hocVienId);
    }
}
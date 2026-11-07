using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IHoaDonRepository
    {
        IEnumerable<HoaDonModel> GetAll();
        IEnumerable<HoaDonModel> GetByHocVienId(int hocVienId);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL.Interfaces
{
    public interface IHoaDonRepository
    {
        IEnumerable<HoaDonModel> GetAll();
        IEnumerable<HoaDonModel> GetByHocVien(int hocVienId);

    }
}

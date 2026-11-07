using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IThongKeRepository
    {
        IEnumerable<ThongKeLopHocModel> GetThongKeLopHoc();
    }
}
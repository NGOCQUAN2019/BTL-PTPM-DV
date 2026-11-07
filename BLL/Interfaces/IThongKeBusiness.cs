using Model;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IThongKeBusiness
    {
        IEnumerable<ThongKeLopHocModel> GetThongKeLopHoc();
    }
}
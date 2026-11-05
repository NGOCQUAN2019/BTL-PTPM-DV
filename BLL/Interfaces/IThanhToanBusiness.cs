using Model;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IThanhToanBusiness
    {
        bool ThemThanhToan(ThanhToanRequest request);
    }
}
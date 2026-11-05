using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IThanhToanRepository
    {
        bool Create(ThanhToanRequest request);
    }
}
    
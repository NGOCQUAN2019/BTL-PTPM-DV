using Model;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IDiemSoBusiness
    {
        IEnumerable<DiemSoModel> GetAll();
        DiemSoModel GetById(int id);
        bool Create(DiemSoModel model);
        bool Update(DiemSoModel model);
        bool Delete(int id);
    }
}
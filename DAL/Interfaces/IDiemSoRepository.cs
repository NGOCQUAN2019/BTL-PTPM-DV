using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDiemSoRepository
    {
        IEnumerable<DiemSoModel> GetAll();
        DiemSoModel GetById(int id);
        bool Create(DiemSoModel model);
        bool Update(DiemSoModel model);
        bool Delete(int id);
    }
}
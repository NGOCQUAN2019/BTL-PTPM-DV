using System.Collections.Generic;
using Model;

namespace BLL.Interfaces
{
    public interface IGhiDanhBusiness
    {
        IEnumerable<GhiDanhModel> GetAll();
        GhiDanhModel GetById(int id);
        bool Create(GhiDanhModel model);
        bool Update(GhiDanhModel model);
        bool Delete(int id);
    }
}
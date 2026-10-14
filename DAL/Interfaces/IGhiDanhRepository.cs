using System.Collections.Generic;
using Model;

namespace DAL.Interfaces
{
    public interface IGhiDanhRepository
    {
        IEnumerable<GhiDanhModel> GetAll();
        GhiDanhModel GetById(int id);
        bool Create(GhiDanhModel model);
        bool Update(GhiDanhModel model);
        bool Delete(int id);
    }
}
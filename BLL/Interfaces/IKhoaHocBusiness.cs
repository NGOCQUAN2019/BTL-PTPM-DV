using System.Collections.Generic;
using Model;

namespace BLL.Interfaces
{
    public interface IKhoaHocBusiness
    {
        IEnumerable<KhoaHocModel> GetAll();
        KhoaHocModel GetById(int id);
        bool Create(KhoaHocModel model);
        bool Update(KhoaHocModel model);
        bool Delete(int id);
    }
}
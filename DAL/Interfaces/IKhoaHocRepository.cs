using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL.Interfaces
{
    public interface IKhoaHocRepository
    {
        IEnumerable<KhoaHocModel> GetAll();
        KhoaHocModel GetById(int id);
        bool Create(KhoaHocModel model);
        bool Update(KhoaHocModel model);
        bool Delete(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ILopHocRepository
    {
        IEnumerable<LopHocModel> GetAll();
        LopHocModel GetById(int id);
        bool Create(LopHocModel model);
        bool Update(LopHocModel model);
        bool Delete(int id);
    }
}
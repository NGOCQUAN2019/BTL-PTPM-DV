using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL.Interfaces
{
    public interface ICourseBusiness
    {
        IEnumerable<CourseModel> GetAll();
        CourseModel GetById(int id);
        bool Create(CourseModel model);
        bool Update(CourseModel model);
        bool Delete(int id);
    }
}

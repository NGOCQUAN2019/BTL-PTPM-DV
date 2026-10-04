using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ITeacherBusiness
    {
        bool CreateTeacher(TeacherModel model);
        List<TeacherModel> SearchTeachers(string keyword);
    }
}
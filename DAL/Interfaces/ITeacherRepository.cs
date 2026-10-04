using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ITeacherRepository
    {
        bool CreateTeacher(TeacherModel model);
        List<TeacherModel> SearchTeachers(string keyword);
    }
}
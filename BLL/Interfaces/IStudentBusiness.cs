using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL.Interfaces
{
    public interface IStudentBusiness
    {
        bool CreateStudent(StudentModel model);
        bool UpdateStudent(StudentModel model);
        bool DeleteStudent(string studentCode);
        StudentModel GetStudentById(string studentCode);
        List<StudentModel> SearchStudents(string keyword);
    }
}

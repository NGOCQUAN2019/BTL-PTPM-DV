using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IStudentRepository
    {   
        bool CreateStudent(StudentModel model);
        bool UpdateStudent(StudentModel model);
        bool DeleteStudent(string studentCode);
        StudentModel GetStudentById(string studentCode);
        List<StudentModel> SearchStudents(string keyword);
    }
}

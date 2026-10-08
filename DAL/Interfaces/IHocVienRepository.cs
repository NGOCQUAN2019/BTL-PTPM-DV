using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IHocVienRepository
    {   
        bool CreateStudent(HocVienModel model);
        bool UpdateStudent(HocVienModel model);
        bool DeleteStudent(string studentCode);
        HocVienModel GetStudentById(string studentCode);
        List<HocVienModel> SearchStudents(string keyword);
    }
}

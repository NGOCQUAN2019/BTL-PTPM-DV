using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL.Interfaces
{
    public interface IHocVienBusiness
    {
        bool CreateStudent(HocVienModel model);
        bool UpdateStudent(HocVienModel model);
        bool DeleteStudent(string studentCode);
        HocVienModel GetStudentById(string studentCode);
        List<HocVienModel> SearchStudents(string keyword);
    }
}

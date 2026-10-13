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
        bool DeleteStudent(string maHocVien);
        HocVienModel GetStudentById(string maHocVien);
        List<HocVienModel> SearchStudents(string tuKhoa);
    }
}
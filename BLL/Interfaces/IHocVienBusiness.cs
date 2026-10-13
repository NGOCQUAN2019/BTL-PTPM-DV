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
        // Đổi studentCode thành maHocVien
        bool DeleteStudent(string maHocVien);
        // Đổi studentCode thành maHocVien
        HocVienModel GetStudentById(string maHocVien);
        // Đổi keyword thành tuKhoa
        List<HocVienModel> SearchStudents(string tuKhoa);
    }
}
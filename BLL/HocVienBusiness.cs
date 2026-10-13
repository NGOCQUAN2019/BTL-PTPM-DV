using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class HocVienBusiness : IHocVienBusiness
    {
        private readonly IHocVienRepository _studentRepository;

        public HocVienBusiness(IHocVienRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public bool CreateStudent(HocVienModel model)
        {
            return _studentRepository.CreateStudent(model);
        }

        public bool UpdateStudent(HocVienModel model)
        {
            return _studentRepository.UpdateStudent(model);
        }

        public bool DeleteStudent(string maHocVien)
        {
            return _studentRepository.DeleteStudent(maHocVien);
        }

        public HocVienModel GetStudentById(string maHocVien)
        {
            return _studentRepository.GetStudentById(maHocVien);
        }

        public List<HocVienModel> SearchStudents(string tuKhoa)
        {
            return _studentRepository.SearchStudents(tuKhoa);
        }
    }
}
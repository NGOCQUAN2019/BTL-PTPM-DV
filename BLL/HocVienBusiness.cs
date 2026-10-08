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

        public bool DeleteStudent(string studentCode)
        {
            return _studentRepository.DeleteStudent(studentCode);
        }

        public HocVienModel GetStudentById(string studentCode)
        {
            return _studentRepository.GetStudentById(studentCode);
        }

        public List<HocVienModel> SearchStudents(string keyword)
        {
            return _studentRepository.SearchStudents(keyword);
        }
    }
}
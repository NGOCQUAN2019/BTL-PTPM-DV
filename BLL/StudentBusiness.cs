using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class StudentBusiness : IStudentBusiness
    {
        private readonly IStudentRepository _studentRepository;

        public StudentBusiness(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public bool CreateStudent(StudentModel model)
        {
            return _studentRepository.CreateStudent(model);
        }

        public bool UpdateStudent(StudentModel model)
        {
            return _studentRepository.UpdateStudent(model);
        }

        public bool DeleteStudent(string studentCode)
        {
            return _studentRepository.DeleteStudent(studentCode);
        }

        public StudentModel GetStudentById(string studentCode)
        {
            return _studentRepository.GetStudentById(studentCode);
        }

        public List<StudentModel> SearchStudents(string keyword)
        {
            return _studentRepository.SearchStudents(keyword);
        }
    }
}
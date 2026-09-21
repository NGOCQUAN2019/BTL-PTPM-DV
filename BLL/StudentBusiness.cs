using DAL;
using Model;
using System;

namespace BLL
{
    // Interface
    public interface IStudentBusiness
    {
        bool CreateStudent(StudentModel model);
    }

    // Class thực thi
    public class StudentBusiness : IStudentBusiness
    {
        private readonly IStudentRepository _studentRepository;

        public StudentBusiness(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public bool CreateStudent(StudentModel model)
        {
            // Kiểm tra tính hợp lệ của dữ liệu trước khi gọi DAL
            if (string.IsNullOrWhiteSpace(model.StudentCode))
                throw new Exception("Mã học viên không được để trống!");

            if (string.IsNullOrWhiteSpace(model.FullName))
                throw new Exception("Họ tên không được để trống!");

            return _studentRepository.CreateStudent(model);
        }
    }
}
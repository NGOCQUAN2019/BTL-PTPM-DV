using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class TeacherBusiness : ITeacherBusiness
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherBusiness(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public bool CreateTeacher(TeacherModel model)
        {
            return _teacherRepository.CreateTeacher(model);
        }

        public List<TeacherModel> SearchTeachers(string keyword)
        {
            return _teacherRepository.SearchTeachers(keyword);
        }
    }
}
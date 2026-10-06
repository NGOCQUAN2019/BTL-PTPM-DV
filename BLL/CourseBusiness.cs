using DAL.Interfaces;
using Model;
using BLL.Interfaces;
using System.Collections.Generic;

namespace BLL
{
    public class CourseBusiness : ICourseBusiness
    {
        private readonly ICourseRepository _repo;

        public CourseBusiness(ICourseRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<CourseModel> GetAll() => _repo.GetAll();
        public CourseModel GetById(int id) => _repo.GetById(id);
        public bool Create(CourseModel model) => _repo.Create(model);
        public bool Update(CourseModel model) => _repo.Update(model);
        public bool Delete(int id) => _repo.Delete(id);
    }
}
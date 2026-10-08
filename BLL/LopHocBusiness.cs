using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Model;
using BLL.Interfaces;

namespace BLL
{
    public class LopHocBusiness : ILopHocBusiness
    {
        private readonly ILopHocRepository _repo;

        public LopHocBusiness(ILopHocRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<LopHocModel> GetAll() => _repo.GetAll();
        public LopHocModel GetById(int id) => _repo.GetById(id);
        public bool Create(LopHocModel model) => _repo.Create(model);
        public bool Update(LopHocModel model) => _repo.Update(model);
        public bool Delete(int id) => _repo.Delete(id);
    }
}
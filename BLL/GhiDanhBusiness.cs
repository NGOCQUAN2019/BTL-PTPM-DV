using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class GhiDanhBusiness : IGhiDanhBusiness
    {
        private readonly IGhiDanhRepository _repo;

        public GhiDanhBusiness(IGhiDanhRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<GhiDanhModel> GetAll() => _repo.GetAll();
        public GhiDanhModel GetById(int id) => _repo.GetById(id);
        public bool Create(GhiDanhModel model) => _repo.Create(model);
        public bool Update(GhiDanhModel model) => _repo.Update(model);
        public bool Delete(int id) => _repo.Delete(id);
    }
}
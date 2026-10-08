using DAL.Interfaces;
using Model;
using BLL.Interfaces;
using System.Collections.Generic;

namespace BLL
{
    public class KhoaHocBusiness : IKhoaHocBusiness
    {
        private readonly IKhoaHocRepository _repo;

        public KhoaHocBusiness(IKhoaHocRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<KhoaHocModel> GetAll() => _repo.GetAll();
        public KhoaHocModel GetById(int id) => _repo.GetById(id);
        public bool Create(KhoaHocModel model) => _repo.Create(model);
        public bool Update(KhoaHocModel model) => _repo.Update(model);
        public bool Delete(int id) => _repo.Delete(id);
    }
}
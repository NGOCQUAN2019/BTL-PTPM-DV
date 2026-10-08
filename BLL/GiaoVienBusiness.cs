using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class GiaoVienBusiness : IGiaoVienBusiness
    {
        private readonly IGiaoVienRepository _repo;

        public GiaoVienBusiness(IGiaoVienRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<GiaoVienModel> GetAll() => _repo.GetAll();
        public GiaoVienModel GetById(int id) => _repo.GetById(id);
        public bool Create(GiaoVienModel model) => _repo.Create(model);
        public bool Update(GiaoVienModel model) => _repo.Update(model);
        public bool Delete(int id) => _repo.Delete(id);
    }
}
using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class DiemSoBusiness : IDiemSoBusiness
    {
        private readonly IDiemSoRepository _diemSoRepository;

        public DiemSoBusiness(IDiemSoRepository diemSoRepository)
        {
            _diemSoRepository = diemSoRepository;
        }

        public IEnumerable<DiemSoModel> GetAll()
        {
            return _diemSoRepository.GetAll();
        }

        public DiemSoModel GetById(int id)
        {
            return _diemSoRepository.GetById(id);
        }

        public bool Create(DiemSoModel model)
        {
            return _diemSoRepository.Create(model);
        }

        public bool Update(DiemSoModel model)
        {
            return _diemSoRepository.Update(model);
        }

        public bool Delete(int id)
        {
            return _diemSoRepository.Delete(id);
        }
    }
}
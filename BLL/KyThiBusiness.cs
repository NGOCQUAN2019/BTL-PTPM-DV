using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class KyThiBusiness : IKyThiBusiness
    {
        private readonly IKyThiRepository _kyThiRepository;

        public KyThiBusiness(IKyThiRepository kyThiRepository)
        {
            _kyThiRepository = kyThiRepository;
        }

        public IEnumerable<KyThiModel> GetAll()
        {
            return _kyThiRepository.GetAll();
        }

        public KyThiModel GetById(int id)
        {
            return _kyThiRepository.GetById(id);
        }

        public bool Create(KyThiModel model)
        {
            return _kyThiRepository.Create(model);
        }

        public bool Update(KyThiModel model)
        {
            return _kyThiRepository.Update(model);
        }

        public bool Delete(int id)
        {
            return _kyThiRepository.Delete(id);
        }
    }
}
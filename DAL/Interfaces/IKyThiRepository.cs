using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IKyThiRepository
    {
        IEnumerable<KyThiModel> GetAll();
        KyThiModel GetById(int id);
        bool Create(KyThiModel model);
        bool Update(KyThiModel model);
        bool Delete(int id);
    }
}
using Model;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IKyThiBusiness
    {
        IEnumerable<KyThiModel> GetAll();
        KyThiModel GetById(int id);
        bool Create(KyThiModel model);
        bool Update(KyThiModel model);
        bool Delete(int id);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IGiaoVienBusiness
    {
        IEnumerable<GiaoVienModel> GetAll();
        GiaoVienModel GetById(int id);
        bool Create(GiaoVienModel model);
        bool Update(GiaoVienModel model);
        bool Delete(int id);
    }
}
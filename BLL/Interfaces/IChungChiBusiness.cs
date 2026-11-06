using Model;
using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IChungChiBusiness
    {
        IEnumerable<ChungChiModel> GetAll();
        ChungChiModel GetById(int id);
        bool CapChungChi(int ghiDanhId);
        bool UpdatePdf(int id, string duongDanPdf);
        bool Delete(int id);
    }
}
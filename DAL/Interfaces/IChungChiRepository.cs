using Model;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IChungChiRepository
    {
        IEnumerable<ChungChiModel> GetAll();
        ChungChiModel GetById(int id);
        bool CapChungChi(int ghiDanhId); // Gọi Stored Procedure
        bool UpdatePdf(int id, string duongDanPdf); // Chỉ cho phép cập nhật file đính kèm
        bool Delete(int id);
    }
}
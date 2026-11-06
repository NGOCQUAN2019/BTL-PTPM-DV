using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System.Collections.Generic;

namespace BLL
{
    public class ChungChiBusiness : IChungChiBusiness
    {
        private readonly IChungChiRepository _chungChiRepository;

        public ChungChiBusiness(IChungChiRepository chungChiRepository)
        {
            _chungChiRepository = chungChiRepository;
        }

        public IEnumerable<ChungChiModel> GetAll() => _chungChiRepository.GetAll();
        public ChungChiModel GetById(int id) => _chungChiRepository.GetById(id);
        public bool CapChungChi(int ghiDanhId) => _chungChiRepository.CapChungChi(ghiDanhId);
        public bool UpdatePdf(int id, string duongDanPdf) => _chungChiRepository.UpdatePdf(id, duongDanPdf);
        public bool Delete(int id) => _chungChiRepository.Delete(id);
    }
}
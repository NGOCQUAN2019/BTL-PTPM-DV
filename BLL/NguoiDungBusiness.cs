using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL
{
    public class NguoiDungBusiness : INguoiDungBusiness
    {
        // Khai báo biến _userRepository
        private readonly INguoiDungRepository _userRepository;

        // Constructor: Đây là đoạn cốt lõi để sửa lỗi không tìm thấy _userRepository. 
        // Hệ thống sẽ tự động tiêm đối tượng UserRepository vào đây.
        public NguoiDungBusiness(INguoiDungRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Thực thi gọi hàm từ DAL lên
        public LoggedInUser ValidateUser(string username, string password)
        {
            return _userRepository.ValidateUser(username, password);
        }


    }
}

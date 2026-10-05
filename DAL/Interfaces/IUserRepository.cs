using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        // Hàm mới thêm vào cho chức năng Đăng nhập
        LoggedInUser ValidateUser(string username, string password);

    }
}

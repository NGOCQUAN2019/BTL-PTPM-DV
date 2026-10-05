using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL.Interfaces
{
    public interface IUserBusiness
    {
        // Hàm mới thêm vào
        LoggedInUser ValidateUser(string username, string password);

    }
}

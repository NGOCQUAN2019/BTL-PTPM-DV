using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class AuthenticateModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string MatKhau { get; set; }
    }

    // Đổi tên các biến hứng dữ liệu cho khớp với cột trong bảng NguoiDung
    public record LoggedInUser(int Id, string TenDangNhap, string VaiTro);
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using 2 thư viện thiết kế metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebSiteBanSach.Models
{
    [MetadataTypeAttribute(typeof(KhachHangMetadata))]
    public partial class KhachHang
    {
        //chỉ dùng ở đây và không cho kế thừa
        internal sealed class KhachHangMetadata
        {
            [Display(Name = "Mã khách hàng")]  //thuộc tính display dùng để đặt tên lại cho cột
            [Required(ErrorMessage = "{0} Không được để trống")] //kiểm tra rỗng
            public int MaKH { get; set; }
            [Display(Name = "Họ tên")]  //thuộc tính display dùng để đặt tên lại cho cột
            [Required(ErrorMessage = "{0} Không được để trống")] //kiểm tra rỗng
            public string HoTen { get; set; }
            [Display(Name = "Tài khoản")]  //thuộc tính display dùng để đặt tên lại cho cột
            [Required(ErrorMessage = "{0} Không được để trống")] //kiểm tra rỗng
            public string TaiKhoan { get; set; }
            [Display(Name = "Mật khẩu")]  //thuộc tính display dùng để đặt tên lại cho cột
            [Required(ErrorMessage = "{0} Không được để trống")] //kiểm tra rỗng
            public string MatKhau { get; set; }
            [Display(Name = "Email")]  //thuộc tính display dùng để đặt tên lại cho cột
            [Required(ErrorMessage = "{0} Không được để trống")] //kiểm tra rỗng
            public string Email { get; set; }
            [Display(Name = "Địa chỉ")]  //thuộc tính display dùng để đặt tên lại cho cột
            [Required(ErrorMessage = "{0} Không được để trống")] //kiểm tra rỗng
            public string DiaChi { get; set; }
            [Display(Name = "Điện thoại")]  //thuộc tính display dùng để đặt tên lại cho cột
            [Required(ErrorMessage = "{0} Không được để trống")] //kiểm tra rỗng
            public string DienThoai { get; set; }
            [Display(Name = "Giới tính")]  //thuộc tính display dùng để đặt tên lại cho cột
            [Required(ErrorMessage = "{0} Không được để trống")] //kiểm tra rỗng
            public string GioiTinh { get; set; }
        }
    }
}
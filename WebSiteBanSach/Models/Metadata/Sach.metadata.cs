using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using 2 thư viện thiết kế metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebSiteBanSach.Models
{
    [MetadataTypeAttribute(typeof(SachMetadata))]
    public partial class Sach
    {
        internal sealed class SachMetadata
        {
            [Required(ErrorMessage ="Vui lòng nhập dữ liệu cho mục này.")] //kiểm tra rỗng
            [Display(Name ="Mã sách")] //thuộc tính display dùng để đặt tên lại cho cột
            public int MaSach { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho mục này.")] //kiểm tra rỗng
            [Display(Name = "Tên sách")] //thuộc tính display dùng để đặt tên lại cho cột
            public string TenSach { get; set; }
            [Display(Name = "Giá bán")] //thuộc tính display dùng để đặt tên lại cho cột
            public Nullable<decimal> GiaBan { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho mục này.")] //kiểm tra rỗng
            [Display(Name = "Mô tả")] //thuộc tính display dùng để đặt tên lại cho cột
            public string MoTa { get; set; }
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString ="{0:dd/mm/yyyy}",ApplyFormatInEditMode =true)] //định dạng ngày sinh
            [Display(Name ="Ngày cập nhật")] //thuộc tính display dùng để đặt tên lại cho cột
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho mục này.")] //kiểm tra rỗng
            public Nullable<System.DateTime> NgayCapNhat { get; set; }
            
            [Display(Name = "Ảnh bìa")] //thuộc tính display dùng để đặt tên lại cho cột
            public string AnhBia { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho mục này.")] //kiểm tra rỗng
            [Display(Name = "Số lượng tồn")] //thuộc tính display dùng để đặt tên lại cho cột
            public Nullable<int> SoLuongTon { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho mục này.")] //kiểm tra rỗng
            [Display(Name = "Chủ đề")] //thuộc tính display dùng để đặt tên lại cho cột
            public Nullable<int> MaChuDe { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho mục này.")] //kiểm tra rỗng
            [Display(Name = "Nhà xuất bản")] //thuộc tính display dùng để đặt tên lại cho cột
            public Nullable<int> MaNXB { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho mục này.")] //kiểm tra rỗng
            [Display(Name = "Mới")] //thuộc tính display dùng để đặt tên lại cho cột
            public Nullable<int> Moi{ get; set; }
        }
    }
}
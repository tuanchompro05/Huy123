  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class NguoiDungController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(KhachHang kh)
        {
            //if này để kiểm tra khi các validation hợp lệ thì sẽ save
            if(ModelState.IsValid)
            //chèn dữ liệu vào bảng khách hàng
            db.KhachHangs.Add(kh);
            //lưu vào csdl
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f["txtMatKhau"].ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);

            if(kh != null)
            {
                ViewBag.ThongBao = "Chúc mừng bạn đã đăng nhập thành công!";
                Session["TaiKhoan"] = kh; //session để lưu đối tượng khách hàng
                return View();
            }
            ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không đúng!";
            return View();
        }
    }
}
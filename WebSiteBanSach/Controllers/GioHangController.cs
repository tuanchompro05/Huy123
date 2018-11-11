using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: GioHang
        //lấy giỏ hàng
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //nếu giỏ hàng chưa tồn tại thì tiến hành tạo list giỏ hàng (sessionGioHang)
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //thêm giỏ hàng
        public ActionResult ThemGioHang(int iMaSach,string strURL)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy ra session giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra sách này đã tồn tại trong session[GioHang] chưa
            GioHang sanpham = lstGioHang.Find(n => n.iMaSach == iMaSach);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSach);
                //add sản phẩm  mới  thêm vào list
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        //cập nhật giỏ hàng
        public ActionResult CapNhatGioHang(int iMaSP,FormCollection f)
        {
            //kiểm tra masp
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            //nếu get sai masp sẽ trả về trang 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng từ session
            List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra sp có tồn tại trong session["GioHang"]
            GioHang sanpham = lstGioHang.SingleOrDefault(n=>n.iMaSach == iMaSP );
            //nếu mà tồn tại ta cho sửa số lượng
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        //xóa giỏ hàng
        public ActionResult XoaGioHang(int iMaSP)
        {
            //kiểm tra masp
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            //nếu get sai masp sẽ trả về trang 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng từ session
            List<GioHang> lstGioHang = LayGioHang();
            //kiểm tra sp có tồn tại trong session["GioHang"]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            //nếu mà tồn tại ta cho sửa số lượng
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSach == iMaSP);
            }
            //nếu session giỏ hàng xóa hết r thì sẽ trả về trang chủ
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        //xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        //tính tổng số lượng và tổng tiền
        //tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //tính tổng tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return dTongTien;
        }
        //tạo partial giỏ hàng
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            return PartialView();
        }
        //xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        //xây dựng chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            //kiểm tra đăng nhập chưa
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            //kiểm tra xem có giỏ hàng chưa
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            //thêm đơn đặt hàng
            //đầu tiên ta tạo đối tượng
            DonHang ddh = new DonHang();
            //ép kiểu về khách hàng
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            //phương thức này để lấy giỏ hàng từ session giỏ hàng ra
            List<GioHang> gh = LayGioHang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            //add đơn hàng vào csdl
            db.DonHangs.Add(ddh);
            db.SaveChanges();
            //thêm chi tiết đơn hàng
            foreach(var item in gh)
            {
                ChiTietDonHang ctDH = new ChiTietDonHang();
                ctDH.MaDonHang = ddh.MaDonHang;
                ctDH.MaSach = item.iMaSach;
                ctDH.SoLuong = item.iSoLuong;
                ctDH.DonGia = (decimal)item.dDonGia;
                //add chi tiết đơn hàng vào csdl
                db.ChiTietDonHangs.Add(ctDH);
            }
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}
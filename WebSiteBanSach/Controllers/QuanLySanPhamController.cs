using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace WebSiteBanSach.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        // GET: QuanLySanPham
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();

        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 3;

            return View(db.Saches.ToList().OrderBy(n => n.MaSach).ToPagedList(pageNumber, pageSize));
        }
        //thêm mới
        [HttpGet]
        public ActionResult ThemMoi()
        {
            //đưa dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemMoi(Sach sach, HttpPostedFileBase fileUpload)
        {

            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            //kiểm tra đường dẫn ảnh bìa
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Yêu cầu nhập ảnh";
                return View();
            }

            //thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                //lưu tên file
                var fileName = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/HinhAnhSP"), fileName);
                //kiểm tra hình ảnh đã tồn tại chưa
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                sach.AnhBia = fileUpload.FileName;
                db.Saches.Add(sach);
                db.SaveChanges();
            }
            return View();
        }
        //chỉnh sửa sản phẩm
        [HttpGet]
        public ActionResult ChinhSua(int MaSach)
        {
            //lấy ra đối tượng sách theo mã sách
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //đưa dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe",sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB",sach.MaNXB);
            return View(sach);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(Sach sach,FormCollection f)
        {

            //thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                //thực hiện cập nhật trong model
                
                db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe",sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB",sach.MaNXB);
            return RedirectToAction("Index");
        }
        //hiển thị sản phẩm
        public ActionResult HienThi(int MaSach)
        {
            //lấy ra đối tượng sách theo mã sách
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        //xóa sản phẩm
        [HttpGet]
        public ActionResult Xoa(int MaSach)
        {
            //lấy ra đối tượng sách theo mã sách
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost,ActionName("Xoa")]
        public ActionResult XacNhanXoa(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
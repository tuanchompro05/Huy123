using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using PagedList.Mvc;
using PagedList;

namespace WebSiteBanSach.Controllers
{
    public class TimKiemController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        [HttpPost]
        // GET: TimKiem
        //truyền vào FormCollection để có thể lấy đc giá trị value khi ng dùng nhập vào textbox
        public ActionResult KetQuaTimKiem(FormCollection f, int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            List<Sach> lstKQTK = db.Saches.Where(n => n.TenSach.Contains(sTuKhoa)).ToList();
            //phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            if (lstKQTK.Count==0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber,pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count + " kết quả!";
            return View(lstKQTK.OrderBy(n=>n.TenSach).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        // GET: TimKiem
        //truyền vào FormCollection để có thể lấy đc giá trị value khi ng dùng nhập vào textbox
        public ActionResult KetQuaTimKiem(string sTuKhoa, int? page)
        {
            ViewBag.TuKhoa = sTuKhoa;
            List<Sach> lstKQTK = db.Saches.Where(n => n.TenSach.Contains(sTuKhoa)).ToList();
            //phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy" + lstKQTK.Count + "kết quả!";
            return View(lstKQTK.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        }
    }
}
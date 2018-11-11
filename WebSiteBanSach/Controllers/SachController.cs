using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class SachController : Controller
    {
        // GET: SachMoiPartial/
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public PartialViewResult SachMoiPartial()
        {
            var lstSachMoi = db.Saches.Take(3).ToList();
            return PartialView(lstSachMoi);
        }
        public ActionResult XemChiTiet(int MaSach=0)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if(sach == null)
            {
                //trả về trang báo lỗi
                Response.StatusCode = 404;
                return null;
            }
            //hiển thị ra tên chủ đề và tên nhà xuất bản từ mã chủ đề vsf mã nxb
            ViewBag.TenChuDe = db.ChuDes.Single(n => n.MaChuDe == sach.MaChuDe).TenChuDe;
            ViewBag.TenNXB = db.NhaXuatBans.Single(n => n.MaNXB == sach.MaNXB).TenNXB;
            return View(sach);
        }
    }
}
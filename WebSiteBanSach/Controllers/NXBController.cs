using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class NXBController : Controller
    {
        // GET: NXB
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult NhaXuatBanPartial()
        {
            return PartialView(db.NhaXuatBans.Take(5).OrderBy(n=>n.TenNXB).ToList());
        }
        //hiển thị sách theo NXB
        public ActionResult SachTheoNXB(int MaNXB)
        {
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //truy xuất danh sách các quyển sách theo chủ đề
            List<Sach> lstSach = db.Saches.Where(n => n.MaNXB == MaNXB).OrderBy(n => n.GiaBan).ToList();
            if (lstSach.Count == 0)
            {
                ViewBag.Sach = "";
            }
            return View(lstSach);
        }
        //hiển thị các nhà xuất bản
        public ActionResult DanhMucNXB()
        {
            return View(db.NhaXuatBans.ToList());
        }
    }
}
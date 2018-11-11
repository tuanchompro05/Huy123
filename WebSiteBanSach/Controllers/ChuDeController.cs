using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    public class ChuDeController : Controller
    {
        // GET: ChuDe
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult ChuDePartial()
        {
            return PartialView(db.ChuDes.Take(5).ToList());
        }
        //sách theo chủ đề
        public ActionResult SachTheoChuDe(int MaChuDe = 0)
        {
            //kiểm tra chủ đề tồn tại không
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaChuDe);
            if(cd ==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //truy xuất danh sách các quyển sách theo chủ đề
            List<Sach> lstSach = db.Saches.Where(n => n.MaChuDe == MaChuDe).OrderBy(n => n.GiaBan).ToList();
            if (lstSach.Count == 0)
            {
                ViewBag.Sach = "";
            }
            return View(lstSach);
        }
        //hiển thị các chủ đề
        public ActionResult DanhMucChuDe()
        {
            return View(db.ChuDes.ToList());
        }
    }
}
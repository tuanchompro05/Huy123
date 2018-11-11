using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanSach.Models;
using PagedList;
using PagedList.Mvc;

namespace WebSiteBanSach.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index(int? page)
        {
            //tạo ra biến sô sp trên trang
            int pageSize = 3;
            //tạo biến số trang
            int pageNumber = (page ?? 1);
            return View(db.Saches.Where(n => n.Moi == 1).OrderBy(n => n.GiaBan).ToPagedList(pageNumber,pageSize));
        }
    }
}
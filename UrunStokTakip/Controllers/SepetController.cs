using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
    public class SepetController : Controller
    {
        // GET: Sepet
        stokEntities db = new stokEntities();

        public ActionResult Index(int?Adet)
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciadi = User.Identity.Name;
                var kullanici= db.Kullanicilar.FirstOrDefault(x=> x.Email== kullaniciadi);
                var model = db.Sepet.Where(x=> x.KullaniciId == kullanici.ID).ToList();
                var kid= db.Sepet.FirstOrDefault(x=> x.KullaniciId==kullanici.ID);
                if (model!=null)
                {
                    if(kid==null)
                    {
                        ViewBag.Adet = "Sepetinizde Ürün Bulunmamaktadır";
                    }
                    else if(kid!=null)
                    {
                        Adet = db.Sepet.Where(x=> x.KullaniciId== kid.KullaniciId).Sum(x=> x.Adet);
                        ViewBag.Adet = "Toplam Adet" + Adet;
                    }
                    return View(model);
                }
            }
            return HttpNotFound();
        }




        public ActionResult Index()
        {
            return View();
        }
    }
}
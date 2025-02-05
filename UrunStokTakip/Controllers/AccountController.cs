using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        stokEntities db = new stokEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOut()
        {
         FormsAuthentication.SignOut();
            return RedirectToAction("Login","Account");
         }
        [HttpPost]
        public ActionResult Login(Kullanicilar p)
        {
            var bilgiler = db.Kullanicilar.FirstOrDefault(x => x.KullaniciAdi == p.KullaniciAdi && x.Sifre == p.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAdi, false);
                Session["text"]=bilgiler.KullaniciAdi.ToString();
                Session["Ad"]=bilgiler.Ad.ToString();
                Session["Soyad"]=bilgiler.Soyad.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.hata = "Kullanıcı Adı veya Şifre Hatalı";
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Kullanicilar data)
        {
            db.Kullanicilar.Add(data);
            data.Rol = "U";
            db.SaveChanges();
            return RedirectToAction("Login", "Account");
        }
    }
            
}
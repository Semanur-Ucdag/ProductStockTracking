using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrunStokTakip.Models;

namespace UrunStokTakip.Controllers
{
    [Authorize(Roles = "A")]
    public class KategoriController : Controller
    {
        // GET: Kategori
        stokEntities db = new stokEntities();
        

        public ActionResult Index()
        {
            return View(db.Kategori.Where(x => x.Durum == true).ToList());//True olanları burada çağırdım
        }
      
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
       
        public ActionResult Ekle(Kategori data)
        {
            db.Kategori.Add(data);
            data.Durum = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
        public ActionResult Sil(int id)
        {
            var kategori = db.Kategori.Where(x => x.ID == id).FirstOrDefault();

            if (kategori != null)
            {
                // Eğer kayıt bulunduysa, sadece durumunu güncelle
                kategori.Durum = false;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(Kategori data)
        {
            var guncelle = db.Kategori.Where(x=>x.ID == data.ID).FirstOrDefault();
            guncelle.Aciklama = data.Aciklama;
            guncelle.Ad = data.Ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
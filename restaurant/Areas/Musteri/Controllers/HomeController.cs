using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NToastNotify;
using restaurant.Data;
using restaurant.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.Areas.Musteri.Controllers
{
    [Area("Musteri")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IToastNotification _toast;
        private readonly IWebHostEnvironment _whe;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IToastNotification toast, IWebHostEnvironment whe)
        {
            _logger = logger;
            _db = db;
            _toast = toast;
            _whe = whe;
        }

        public IActionResult Index()
        {
            //ozel menulerı ana sayfa getırme ıslemı
            var menu = _db.Menus.Where(i => i.OzelMenu).ToList();
            return View(menu);
        }
        public IActionResult CategoryDetails(int? id)
        {
            //kategorilere ait menuleri getırme
            var menu= _db.Menus.Where(i => i.CategoryId == id).ToList();
            ViewBag.KategoriId = id;
            return View(menu);
        }
        public IActionResult Menu()
            //menu sayfasına tüm menüleri getirme işlemi
        {
            var menu = _db.Menus.ToList();
            return View(menu);
        }
       
        public IActionResult Galeri()
        {
            var galeri = _db.Galeris.ToList();
            return View(galeri);
        }
        public IActionResult Rezervasyon()
        {

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rezervasyon(int id, [Bind("Id,Name,Email,TelefonNo,Sayi,Saat,Tarih")] Rezervasyon Rezervasyon)
        {
            if (ModelState.IsValid)
            {
                _db.Add(Rezervasyon);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Rezervasyon işlemi başarıyla gerçekleşti.Teşekkür ederiz...");
                return RedirectToAction("Index");


            }
            return View(Rezervasyon);

        }
        public IActionResult About()
        {
            var hakkinda=_db.Hakkındas.ToList();
            return View(hakkinda);
        }
        public IActionResult Blog()
        {
            return View();
        }

        // POST: Yonetici/Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Blog( Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.Tarih = DateTime.Now;
                //yorum tarıhını musterı gırmeyecek sistemden biz çekeceğiz
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(_whe.WebRootPath, @"WebSite\menu");
                    var extn = Path.GetExtension(files[0].FileName);

                    if (blog.Image != null)
                    {
                        var ImagePath = Path.Combine(_whe.WebRootPath, blog.Image.TrimStart('\\'));

                        if (System.IO.File.Exists(ImagePath))
                        {
                            System.IO.File.Delete(ImagePath);
                        }
                    }
                    using (var filesStream = new FileStream(Path.Combine(uploads, fileName + extn), FileMode.Create))
                    {
                        files[0].CopyTo(filesStream);
                    }
                    blog.Image = @"\WebSite\menu\" + fileName + extn;
                }


                _db.Add(blog);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Yorumunuz iletildi.Onaylandığında yorumlar sayfamızdan görebilirsiniz.Teşekkür Ederiz");
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }
        // GET: Yonetici/İletisim/Create
        public IActionResult Contact()
        {
            return View();
        }

        // POST: Yonetici/İletisim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> İletisim([Bind("Id,Name,Email,Telefon,Mesaj")] İletisim İletisim)
        {
            if (ModelState.IsValid)
            {
                İletisim.Tarih = DateTime.Now;
                _db.Add(İletisim);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Mesajınız başarıyla iletildi");
                return RedirectToAction(nameof(Index));
            }
            return View(İletisim);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

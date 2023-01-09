using Microsoft.AspNetCore.Mvc;
using restaurant.Data;
using System.Linq;

namespace restaurant.ViewComponents
{
    public class İletisimim:ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public İletisimim(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var iletisim = _db.İletisimims.ToList();
            return View(iletisim);
        }
    }
}

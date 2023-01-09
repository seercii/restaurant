using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using restaurant.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace restaurant.ViewComponents
{
	public class CategoryList: ViewComponent //using için ctrl. bas
	{
		//veritabanına bağlama işlemleri
		private readonly ApplicationDbContext _db;

		public CategoryList(ApplicationDbContext db)//yapıcı metot ctor tab yap
		{
			_db = db;	
		}

		public IViewComponentResult Invoke()
		{
			//ınvoke bir IViewComponentResult donduren zaman uyumlu yontem
			//ınvoke : çağırmak
			var category = _db.Categories.ToList();
			return View(category);
        }
    }
}

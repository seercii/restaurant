using restaurant.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace restaurant.ViewComponents
{
	public class Comments: ViewComponent
	{
		private readonly ApplicationDbContext _db;

		public Comments(ApplicationDbContext db)
		{
			_db = db;
		}
	public IViewComponentResult Invoke()
		{
			/* var comment=_db.Blogs.ToList(); yönetici onayı olmadan tüm yorumları blog sayfasında gösterir*/
			var comment = _db.Blogs.Where(i => i.Onay).ToList(); //sadece onaylı yorumları listeledim
			return View(comment);
		}
		
	}
}

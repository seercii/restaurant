using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restaurant.Models
{
	public class AppUser:IdentityUser //class user tablosuna ekleme yapabilmesi için identityuser miras almalı
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
		[NotMapped]
		public string Role { get; set; } //orm araçlarında karşılığı olmayan kolon belirttim

	}
}

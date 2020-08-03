using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Identity
{
	public class AspNetUser : IdentityUser
	{
		public string Token { get; set; }
		public DateTime Expiry { get; set; }

		[NotMapped]
		public IList<Claim> Claims { get; set; } = new List<Claim>();

		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}
}

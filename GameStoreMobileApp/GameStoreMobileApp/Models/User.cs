using System;

namespace GameStoreMobileApp
{
	public class User
	{
		public int UserId { get; set; }
		public string URL { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public Roles Role { get; set; }
	}
}


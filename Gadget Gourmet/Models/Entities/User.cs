using System.ComponentModel.DataAnnotations;

namespace Gadget_Gourmet.Models.Entities
{
	public class User
	{
		[Required(ErrorMessage ="Invalid ID Entered!")]
		public int Id { get; set; }

		public string? Name { get; set; } = string.Empty;

		[Required(ErrorMessage ="Username can't be Empty!")]
		public string? UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password Not Set!")]
		public string? Password { get; set; } = string.Empty;

		[Required(ErrorMessage ="Emaili not Set!")]

		public string? Email { get; set; } = string.Empty;
		public string? Address { get; set; } = "None";
		public string? Phone { get; set; } = string.Empty;
		public string? Gender { get; set; } = string.Empty;
		public DateTime DateOfBirth { get; set; } = DateTime.MinValue;

		// Parameterless constructor
		public User()
		{
			Id = -1;
		}
		public User(string? name, string? username, string? password, string? email, string? address, string? phone, string? gender, DateTime dateofbirth)
		{
			Name = name ?? "Not Set";
			UserName = username;
			Password = password;
			Email = email;
			Address = address ?? "None";
			Phone = phone ?? "None";
			Gender = gender ?? "Not Specified";
			DateOfBirth = dateofbirth;
		}
		public User(string? username, string? password, string? email)
		{
			UserName = username;
			Password = password;
			Email = email;
		}
	}
}

namespace Gadget_Gourmet.Models.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string? Name { get; set; } = "None";
		public string? UserName { get; set; } = "None";
		public string? Password { get; set; } = "None";
		public string? Email { get; set; } = "None";
		public string? Address { get; set; } = "None";
		public string? Phone { get; set; } = "None";
		public string? Gender { get; set; } = "None";
		public DateOnly DateOfBirth { get; set; } = DateOnly.MinValue;

		// Parameterless constructor
		public User()
		{
			Id = -1;
		}
		public User(string? name, string? username, string? password, string? email, string? address, string? phone, string? gender, DateOnly dateofbirth)
		{
			Name = name;
			UserName = username;
			Password = password;
			Email = email;
			Address = address;
			Phone = phone;
			Gender = gender;
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

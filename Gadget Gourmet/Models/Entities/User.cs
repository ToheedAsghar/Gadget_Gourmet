namespace Gadget_Gourmet.Models.Entities
{
    public class User
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }

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
    }
}

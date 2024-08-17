using Gadget_Gourmet.Models.Entities;

namespace Gadget_Gourmet.Models.Interface
{
    // pure abstract class
    public interface IUser
    {
        bool Login(User user);
        bool Signup(User user);
        bool PersonalInfo(User user);
        User GetUserByUserName(string? un);

        // Utility Functions
        public bool IdExists(User user);
        public User? GetUserByEmail(string? email);
        public User GetUserByEmailOrUsername(string? Query);

	}
}

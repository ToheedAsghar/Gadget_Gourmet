using Gadget_Gourmet.Models.Entities;
namespace Gadget_Gourmet.Models.Interface
{
    public interface IUser
    {
        bool Login(User user);
        bool Signup(User user);
        bool PersonalInfo(User user);
        User GetUserByUserName(string? un);
    }
}

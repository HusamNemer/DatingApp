using System.Threading.Tasks;
using DatingApp.API.Model;

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
         Task<Users> RegisterAsync(Users user,string password);
         Task<Users> Login(string username,string password);

         Task<bool> UserExists(string username);
    }
}
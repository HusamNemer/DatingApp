using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            this._context = context;

        }
        public async Task<Users> Login(string username, string password)
        {
            var userInfo =await _context.User.FirstOrDefaultAsync(x=> x.Username==username);
               if(userInfo==null)
               return null;

            if (!VerifyPasswordHash(password, userInfo.PasswordHash , userInfo.PasswordSalt)==true)
            {
                 return null; 

            }

            return userInfo;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
             using(var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt)){
            
             
            var passwordHash2=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

           for(int i = 0 ; i < passwordHash.Length; i++){
             if( passwordHash2[i] != passwordHash[i])
             return false;

           }
             }
           return true;
        }

        public async Task<Users> RegisterAsync(Users user, string password)
        {
            byte[] passwordHash , passwordSalt;
           CreatePasswordHash(password, out passwordHash, out passwordSalt);
           user.PasswordHash=passwordHash;
           user.PasswordSalt=passwordSalt;
           await _context.User.AddAsync(user);
           await _context.SaveChangesAsync();
           return user;
        }

        private void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512()){
            
             passwordSalt=hmac.Key;
             passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
            
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.User.AnyAsync(x=>x.Username==username)){
                return true;
            }
            return false;
        }
    }
}
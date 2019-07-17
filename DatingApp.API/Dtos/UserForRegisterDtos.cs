using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDtos
    {
        [Required(ErrorMessage="username is required field")]
        public string username { get; set; }
        [Required(ErrorMessage="Password is required field")]
        public string password { get; set; }
    }
}
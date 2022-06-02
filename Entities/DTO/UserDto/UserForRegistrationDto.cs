using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.UserDto
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage ="UserName field is required")]
        public string UserName { get; set; }
        public string FullName { get; set; }
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }
}

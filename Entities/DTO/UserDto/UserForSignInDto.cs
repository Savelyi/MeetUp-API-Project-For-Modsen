using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.UserDto
{
    public class UserForSignInDto
    {
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(20)]
        [MinLength(6)]
        public string Password { get; set; }
        [Required(ErrorMessage = "UserName field is required")]
        [MaxLength(20)]
        
        public string UserName { get; set; }

    }
}

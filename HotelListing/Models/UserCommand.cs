using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Models
{
    public class UserCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(15,ErrorMessage ="Your Password is limited to {2} to {1} character",MinimumLength =3)]
        public string Password { get; set; }

    }

    public class UserDTO:UserCommand
    {
        public int Id { get; set; }

    }
}

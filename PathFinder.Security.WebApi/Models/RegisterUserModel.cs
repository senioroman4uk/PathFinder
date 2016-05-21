using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Security.WebApi.Models
{
    [NotMapped]
    public class RegisterUserModel
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Security.WebApi.Models
{
    [NotMapped]
    public class UpdateUserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}

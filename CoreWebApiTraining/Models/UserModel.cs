using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiTraining.Models
{
    public class UserModel
    {
        [Required(ErrorMessage ="Name is required.")]
        public string name { get; set; }
        [Required(ErrorMessage = "Surname is required.")]
        public string surname { get; set; }
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address.")]
        public string email { get; set; }
        public string address { get; set; }
    }
}

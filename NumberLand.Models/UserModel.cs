using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.Models
{
    public class UserModel
    {
        public string phoneNumber { get; set; }
        public string password { get; set; }

    }
}

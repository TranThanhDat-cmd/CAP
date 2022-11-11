using CAP_Backend_Source.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAP_Backend_Source.Services.User.Request
{
    public class CreateAccountRequest
    {
        public string? Email { get; set; }

        public int RoleId { get; set; }
    }

}
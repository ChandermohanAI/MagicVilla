using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicVilla.Model.DTO
{
    public class LoginRequestDTO
    {
        public string UserName {get; set; }
        public string password { get; set; }
    }
}
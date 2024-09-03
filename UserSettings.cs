using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_03_09
{
    internal class UserSettings
    {
        public int Id { get; set; }
        
        public string Country { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}

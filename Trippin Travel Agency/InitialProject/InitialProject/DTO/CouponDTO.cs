using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class CouponDTO
    {
        public string name { get; set; }

        public DateTime expiresOn { get; set; } 
        public CouponDTO() { }

        public CouponDTO(string name, DateTime expiresOn)
        {
            this.name = name;
            this.expiresOn = expiresOn;
        }
    }
}

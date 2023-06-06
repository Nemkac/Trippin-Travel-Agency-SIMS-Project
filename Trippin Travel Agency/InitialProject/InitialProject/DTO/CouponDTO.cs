using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class CouponDTO
    {
        public int id { get; set; }
        public string name { get; set; }

        public DateTime expiresOn { get; set; } 
        public CouponDTO() { }

        public CouponDTO(int id, string name, DateTime expiresOn)
        {
            this.id = id;   
            this.name = name;
            this.expiresOn = expiresOn;
        }        
    }
}

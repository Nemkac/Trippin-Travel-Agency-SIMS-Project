using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Coupon
    {

        public int id { get; set; }
       
        public int userId { get; set; }

        public DateTime exiresOn { get; set; }

        public Coupon(int userId, DateTime exiresOn)
        {
            this.userId = userId;
            this.exiresOn = exiresOn;
        }

        public Coupon() { } 
    }
}

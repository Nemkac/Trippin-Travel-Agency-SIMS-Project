using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class ComplexRegularPairs
    {
    

        public int id {  get; set; }

        public int complexId { get; set; }
        public int regularId { get; set; }

        public ComplexRegularPairs(int complexId, int regularId)
        {
            this.complexId = complexId;
            this.regularId = regularId;
        }
        public ComplexRegularPairs() { }    
    }
}

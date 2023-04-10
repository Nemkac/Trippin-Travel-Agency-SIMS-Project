using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class FutureToursDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public language language { get; set; }

        public FutureToursDTO(int id, string name, language language)
        {
            this.id = id;
            this.name = name;
            this.language = language;
        }

        public FutureToursDTO()
        {
        }
    }
}

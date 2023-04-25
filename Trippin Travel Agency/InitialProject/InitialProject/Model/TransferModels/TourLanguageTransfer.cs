using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model.TransferModels
{
    public class TourLanguageTransfer
    {
        public int id { get; set; }
        public string language { get; set; }

        public TourLanguageTransfer(string language)
        {
            this.language = language;
        }

    }
}

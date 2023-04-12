using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class TourAndGuideRateDTO
    {
        public int id { get; set; }
        public int keyPointId { get; set; }
        public int guestId { get; set; }
        public int guideKnowledgeRating { get; set; }
        public int guideLanguageUsageRating { get; set; }
        public int contentRating { get; set; }
        public bool valid { get; set; }

        public TourAndGuideRateDTO(int id, int keyPointId, int guestId, int guideKnowledgeRating, int guideLanguageUsageRating, int contentRating, bool valid)
        {
            this.id = id;
            this.keyPointId = keyPointId;
            this.guestId = guestId;
            this.guideKnowledgeRating = guideKnowledgeRating;
            this.guideLanguageUsageRating = guideLanguageUsageRating;
            this.contentRating = contentRating;
            this.valid = valid;
        }

        public TourAndGuideRateDTO()
        {
        }
    }
}

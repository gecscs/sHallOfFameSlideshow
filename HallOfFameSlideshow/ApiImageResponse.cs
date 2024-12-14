using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HallOfFameSlideshow
{
    public class ApiImageResponse
    {
        public string Id { get; set; }
        public bool IsApproved { get; set; }
        public bool IsReported { get; set; }
        public int FavoritesCount { get; set; }
        public decimal FavoritesPerDay { get; set; }
        public int FavoritingPercentage { get; set; } 
        public int ViewsCount { get; set; }
        public decimal ViewsPerDay { get; set; }
        public string CityName { get; set; }
        public int CityMilestone {  get; set; }
        public int CityPopulation { get; set; }
        public string ImageUrlThumbnail { get; set; }
        public string ImageUrlFHD { get; set; } 
        public string ImageUrl4K { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedAtFormatted { get; set; }
        public string CreatorId { get; set; }
        public Creator Creator { get; set; }
        public string __algorithm { get; set; }
        public bool __favorited { get; set; }
    }
}

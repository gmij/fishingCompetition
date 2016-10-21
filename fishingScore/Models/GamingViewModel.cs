using System;

namespace fishingScore.Models
{
    public class GamingViewModel
    {
        public string CompetitionId { get; set; }

        public string Title { get; set; }

        public DateTime Time { get; set; }

        /// <summary>
        ///    当前在第几场比赛
        /// </summary>
        public int Round { get; set; }
        
    }
}
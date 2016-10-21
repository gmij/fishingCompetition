using System.Collections.Generic;

namespace fishingScore.Models
{
    public class RoundCompetitionPostViewModel
    {

   
        public IList<RoundScorePostViewModel> RoundScores { get; set; }

        public int Round { get; set; }
    }
}
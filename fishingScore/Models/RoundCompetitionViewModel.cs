using System.Collections.Generic;

namespace fishingScore.Models
{
    public class RoundCompetitionViewModel
    {
        

        public RoundCompetitionViewModel(int round, IList<RoundScoreViewModel> scores)
        {
            RoundScores = scores;
            Round = round;
        }
        

        public IList<RoundScoreViewModel> RoundScores { get; set; }

        public int Round { get; set; }
    }
}
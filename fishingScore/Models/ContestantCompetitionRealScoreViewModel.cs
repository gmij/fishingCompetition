using System.Collections.Generic;

namespace fishingScore.Models
{
    public class ContestantCompetitionRealScoreViewModel
    {
        
        public ContestantViewModel Contestant { get; set; }

        public float TotalScore { get; set; }

        public float TotalResult { get; set; }

        public int Order { get; set; }

        public IList<RoundScoreViewModel> RoundScores { get; set; }

    }
}
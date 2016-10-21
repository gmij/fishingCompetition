using System.Collections.Generic;
using System.Linq;
using fishingScore.Persistence;

namespace fishingScore.Models
{
    public class ScorePreviewViewModel
    {
        public ScorePreviewViewModel(Competition competition, IList<ContestantCompetitionScore> scores)
        {
            Competition = competition;
            Scores = scores;
        }

        private Competition Competition { get; }

        public string CompetitionId => Competition.Id;

        public string Title => Competition.Title;

        public string Time => Competition.Time.ToString("yyyy年MM月dd日");

        public IList<ContestantCompetitionScore> Scores { get; }

        
    }

    public class ScorePrintViewModel
    {
        public ScorePrintViewModel(Competition competition, IList<ContestantCompetitionScore> ccs)
        {
            Scores = ccs;
            Competition = competition;
        }

        private Competition Competition { get; }

        public string Title => Competition.Title;

        public string Date => Competition.Time.ToString("yyyy年MM月dd日");

        public IList<ContestantCompetitionScore> Scores { get; }
    }
}
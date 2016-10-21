using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace fishingScore.Persistence
{
    public class ContestantCompetitionScore
    {
        public ContestantCompetitionScore(params RoundScore[] scores)
        {
            foreach (var score in scores)
                RoundScores.Add(score);
        }

        public IList<RoundScore> RoundScores { get; set; } = new List<RoundScore>();

        [Display(Name = "总分")]
        public Single TotalScore
        {
            get { return RoundScores.Sum(s => s.Score ?? 0); }
        }

        [Display(Name = "总成绩")]
        public float TotalResult => RoundScores.Sum(s => s.Result ?? 0);

        [Display(Name = "名次")]
        public int Order { get; set; }

        public Contestant Contestant=> RoundScores.First().Contestant;

        public string CompetitionId => Contestant.CompetitionId;

        public string ContestantId => Contestant.Id;

        [Display(Name = "姓名")]
        public string Name => Contestant.Name;

        [Display(Name = "编号")]
        public string Number => Contestant.Number;

        public RoundScore Round1 => RoundScores.First(rs => rs.Round == 1);

        public RoundScore Round2 => RoundScores.First(rs => rs.Round == 2);

        public RoundScore Round3 => RoundScores.FirstOrDefault(rs => rs.Round == 3);

        public RoundScore Round4 => RoundScores.FirstOrDefault(rs => rs.Round == 4);
    }
}
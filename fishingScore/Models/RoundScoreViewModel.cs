using System.ComponentModel.DataAnnotations;

namespace fishingScore.Models
{
    public class RoundScoreViewModel
    {

        
        public string Id { get; set; } 
        
        public ContestantViewModel Contestant { get; set; }

        public string GroupNum => Contestant.GroupNum;

        [Display(Name = "成绩")]
        public float Result { get; set; }
        [Display(Name = "得分")]
        public float Score { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace fishingScore.Models
{
    public class ContestantViewModel
    {
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "参赛编号")]
        public string Number { get; set; }
        [Display(Name = "小组编号")]
        public string GroupNum { get; set; }

        public string Id { get; set; }
    }
}
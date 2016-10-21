using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fishingScore.Persistence
{
    /// <summary>
    ///     比赛表
    /// </summary>
    [Table("competition")]
    public class Competition
    {
        // Primary key 
        [StringLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString("D");

        [Display(Name = "比赛名称")]
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Display(Name = "比赛日期")]
        public DateTime Time { get; set; }

        [Display(Name = "状态")]
        public string State { get; set; }

        public virtual ICollection<RoundScore> Rounds { get; set; }

    }
}
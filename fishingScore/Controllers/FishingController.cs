using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fishingScore.Models;
using fishingScore.Persistence;

namespace fishingScore.Controllers
{
    public class FishingController : Controller
    {
        private readonly FishingContext _context;

        public FishingController()
        {
            _context = new FishingContext();
        }

        protected string CompetitionId
        {
            get
            {
                var id = Request.Cookies["competition"]?.Value;
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException(nameof(id), "找不到比赛ID，无法添加参赛选手");
                return id;
            }
        }

        // GET: Fishing
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View(new FishingViewModel());
        }

        [HttpPost]
        public ActionResult New(FishingViewModel model)
        {
            var c = new Competition
            {
                Time = DateTime.Parse(model.Time),
                Title = model.Title
            };
            _context.Competitions.Add(c);
            _context.SaveChangesAsync();
            Response.AppendCookie(new HttpCookie("competition", c.Id) {HttpOnly = true});
            return RedirectToAction("ListContestant");
        }

        public ActionResult AddContestant()
        {
            return View(new ContestantViewModel());
        }

        [HttpPost]
        public ActionResult AddContestant(ContestantViewModel model)
        {
            var c = new Contestant
            {
                GroupNum = model.GroupNum,
                Name = model.Name,
                Number = model.Number,
                CompetitionId = CompetitionId
            };
            _context.Contestants.Add(c);
            _context.SaveChangesAsync();
            return RedirectToAction("ListContestant");
        }

        public ActionResult ListContestant()
        {
            var list = _context.Contestants.Where(item => item.CompetitionId == CompetitionId)
                .Select(item => new ContestantViewModel
                {
                    Name = item.Name,
                    Number = item.Number,
                    GroupNum = item.GroupNum,
                    Id = item.Id
                }).OrderBy(item => item.GroupNum).ThenBy(item => item.Number);
            return View("ListContestant", list);
        }

        public ActionResult DelContestant(string id)
        {
            var item = _context.Contestants.Find(id);
            if (item != null)
                _context.Contestants.Remove(item);
            return RedirectToAction("ListContestant");
        }

        public ActionResult Start()
        {
            var all = _context.Contestants.Where(item => item.CompetitionId == CompetitionId).ToList();
            var roundScore = all.Select(item => new RoundScore(1, item));
            _context.RoundScores.AddRange(roundScore);
            var r = _context.SaveChanges();

            return View("Round", roundScore.Select(item => new RoundScoreViewModel()
            {
                Id = item.Id,
                Contestant = new ContestantViewModel
                {
                    Id = item.ContestantId,
                    Number = item.Contestant.Number,
                    GroupNum = item.Contestant.GroupNum,
                    Name = item.Contestant.Name
                }
            }).OrderBy(item => item.Contestant.Number).ToList());
        }


        [HttpPost]
        public ActionResult Start(IList<RoundScorePostViewModel> models)
        {
            //todo: 保存到数据库中，然后提示开始下一层比赛
            var scores = StatisticalScore(models);
            return View("Round");
        }


        public static IList<RoundScore> StatisticalScore(IList<RoundScorePostViewModel> scores)
        {
            var scoresOrder = scores.OrderBy(s => s.GroupNum).ThenByDescending(s => s.Result);
            var i = 0;
            var beforeScore = scoresOrder.First();
            var result = new List<RoundScore>();
            foreach (var score in scoresOrder)
            {
                var s = new RoundScore
                {
                    Id = score.Id,
                    Result = score.Result,
                    Score =
                        score.Result == 0f
                            ? scoresOrder.Count(s1 => s1.GroupNum == score.GroupNum) + 1
                            : score.GroupNum != beforeScore.GroupNum ? (i = 1) : ++i
                };
                result.Add(s);
                beforeScore = score;
            }
            return result;
        }

        public static IList<RoundScore> AverageEqualScores(IList<RoundScore> scores)
        {

            var items = scores.OrderBy(s => s.Result).ToArray();
            for (var i = 0; i < items.Count(); i++)
            {
                var item = items[i];
                var ls = scores.Where(s => s.Result.Equals(item.Result));
                var lsCount = ls.Count();
                if (lsCount > 1)
                {
                    var ave = ls.Sum(s => s.Score)/lsCount;
                    foreach (var score in ls)
                    {
                        score.Score = ave;
                    }
                }
                i += lsCount;
            }
            return scores;
        }

        public static IEnumerable<ContestantCompetitionScore> Order(IList<ContestantCompetitionScore> scores)
        {
            var ls = scores.OrderBy(s => s.TotalScore).ToList();
            var i = 0;
            foreach (var t in ls)
            {
                t.Order = ++i;
            }
            for (i = 0; i < ls.Count();)
            {
                var item = ls[i];
                var ts = scores.Where(s => s.TotalScore.Equals(item.TotalScore)).ToList();
                var tsCount = ts.Count();
                if (tsCount > 1)
                {
                    var us = new List<RoundScore>();
                    foreach (var score in ts)
                    {
                        us.AddRange(score.RoundScores);
                    }
                    var uo = us.OrderBy(u => u.Score).ThenByDescending(u => u.Result).ToList();

                    var k = 1;
                    var c = uo.First();
                    foreach (var score in uo)
                    {
                        if (score.Id != c.Id)
                            k++;
                        if (k > tsCount)
                            break;
                        if (ts.Any())
                        {
                            var b = ts.FirstOrDefault(t => t.Name == score.Id);
                            if (b != null)
                            {
                                b.Order = i + k;
                                ts.Remove(b);
                                c = score;
                            }
                        }
                    }

                    i += tsCount;
                }
                else
                {
                    i++;
                }
            }
            return ls.OrderBy(l => l.Order);
        }
    }






}

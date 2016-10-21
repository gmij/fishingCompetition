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
            var cs = _context.Competitions.Where(c => c.State == "Init").ToList();
            var gs = cs.Select(c => new GamingViewModel
            {
                CompetitionId = c.Id,
                Time = c.Time,
                Title = c.Title,
                Round = c.Rounds.Any() ? c.Rounds.Max(r => r.Round) : 1
            }).OrderByDescending(c => c.Time);

            return View(gs);
        }

        public ActionResult Over()
        {
            var cs = _context.Competitions.Where(c => c.State == "End").ToList();
            var gs = cs.Select(c => new GamingViewModel
            {
                CompetitionId = c.Id,
                Time = c.Time,
                Title = c.Title
            }).OrderByDescending(c => c.Time);

            return View(gs);
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
                Title = model.Title,
                State = "Create"
            };
            _context.Competitions.Add(c);
            _context.SaveChanges();
            Response.AppendCookie(new HttpCookie("competition", c.Id) {HttpOnly = true});
            return RedirectToAction("ListContestant", new {id = c.Id});
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
            _context.SaveChanges();
            return RedirectToAction("AddContestant");
            //return RedirectToAction("ListContestant", new {id = CompetitionId});
        }

        public ActionResult ListContestant(string id)
        {
            var list = _context.Contestants.Where(item => item.CompetitionId == id)
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
            return RedirectToAction("ListContestant", CompetitionId);
        }

        public ActionResult Start()
        {
            var competition = _context.Competitions.First(item => item.Id == CompetitionId);
            competition.State = "Init";
            _context.SaveChanges();
            StartRound(1, CompetitionId);
            return RedirectToAction("Round", new {id = 1});
        }

        public ActionResult Round(int id)
        {
            var roundScores = _context.RoundScores.Where(rs => (rs.CompetitionId == CompetitionId) && (rs.Round == id));
            if (roundScores.Any())
                return View(new RoundCompetitionViewModel(id, roundScores.Select(item => new RoundScoreViewModel
                {
                    Id = item.Id,
                    Contestant = new ContestantViewModel
                    {
                        Id = item.ContestantId,
                        Number = item.Contestant.Number,
                        GroupNum = item.Contestant.GroupNum,
                        Name = item.Contestant.Name
                    }
                }).OrderBy(item => item.Contestant.Number).ToList()));
            throw new ArgumentOutOfRangeException(nameof(id), "回合数无效");
        }


        private RoundCompetitionViewModel StartRound(int round, string competitionId)
        {
            var all = _context.Contestants.Where(item => item.CompetitionId == competitionId).ToList();
            var roundScore = all.Select(item => new RoundScore(round, item)).ToList();
            _context.RoundScores.AddRange(roundScore);
            var r = _context.SaveChanges();

            return new RoundCompetitionViewModel(round, roundScore.Select(item => new RoundScoreViewModel
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
        public ActionResult Round(RoundCompetitionPostViewModel model)
        {
            var scoreData = StatisticalScore(model.RoundScores);
            scoreData = AverageEqualScores(scoreData);

            var competitionId = CompetitionId;
            var round = model.Round++;
            var dbData =
                _context.RoundScores.Where(rs => (rs.CompetitionId == competitionId) && (rs.Round == round)).ToList();
            foreach (var roundScore in dbData)
            {
                var score = scoreData.FirstOrDefault(rs => rs.Id == roundScore.Id);
                if (score == null)
                    continue;
                roundScore.Result = score.Result;
                roundScore.Score = score.Score;
            }

            _context.SaveChanges();
            if ("on" == Request.Form["endGame"])
                return RedirectToAction("ScorePreview", new {id = CompetitionId});
            StartRound(model.Round, competitionId);
            return RedirectToAction("Round", new {id = model.Round});
        }

        public ActionResult BeforeScore(int id)
        {
            var dbData =
                _context.RoundScores.Where(rs => (rs.CompetitionId == CompetitionId) && (rs.Round == id)).ToList();
            return View(dbData.OrderBy(item => item.Contestant.GroupNum).ThenBy(item => item.Score));
        }

        public ActionResult ScorePreview(string id)
        {
            var scores = _context.RoundScores.Where(rs => rs.CompetitionId == id).ToList();

            var contestants = _context.Contestants.Where(c => c.CompetitionId == id).ToList();

            var ts = new List<ContestantCompetitionScore>();
            foreach (var contestant in contestants)
            {
                var ls = scores.Where(s => s.ContestantId == contestant.Id);
                ts.Add(new ContestantCompetitionScore(ls.ToArray()));
            }

            var competition = _context.Competitions.First(c => c.Id == id);
            competition.State = "ScorePreview";
            _context.SaveChanges();

            return View(new ScorePreviewViewModel(competition, Order1(ts)));
        }

        [HttpPost]
        public ActionResult ScorePreview(List<CompetitionResultPostData> scores)
        {
            if (scores == null)
                throw new ArgumentNullException(nameof(scores));

            var cr = _context.CompetitionResults;
            var crs =
                scores.Select(
                    cr1 =>
                        new CompetitionResult()
                        {
                            CompetitionId = cr1.CompetitionId,
                            ContestantId = cr1.ContestantId,
                            Order = cr1.Order
                        });
            cr.AddRange(crs);
            _context.SaveChanges();
            return RedirectToAction("ScorePrint", new {id = scores.First().CompetitionId});
        }


        public ActionResult ScorePrint(string id)
        {
            var crs = _context.CompetitionResults.Where(cr => cr.CompetitionId == id).OrderBy(cr => cr.Order).ToList();

            var scores = _context.RoundScores.Where(rs => rs.CompetitionId == id).ToList();

            var contestants = _context.Contestants.Where(c => c.CompetitionId == id).ToList();

            var ts = new List<ContestantCompetitionScore>();
            foreach (var contestant in contestants)
            {
                var ls = scores.Where(s => s.ContestantId == contestant.Id);
                var ccs = new ContestantCompetitionScore(ls.ToArray());
                ccs.Order = crs.First(cr => cr.ContestantId == ccs.ContestantId).Order;
                ts.Add(ccs);
            }
            var result = ts.OrderBy(item => item.Order).ToList();
            var competition = _context.Competitions.First(c => c.Id == id);

            return View(new ScorePrintViewModel(competition, result));
        }


        public static IList<RoundScore> StatisticalScore(IList<RoundScorePostViewModel> scores)
        {
            var scoresOrder = scores.OrderBy(s => s.GroupNum).ThenByDescending(s => s.Result);
            var i = 0;
            var beforeScore = scoresOrder.First();
            var result = new List<RoundScore>();
            var maxScore = GetMaxScore(scores) + 1;
            foreach (var score in scoresOrder)
            {
                var s = new RoundScore
                {
                    Id = score.Id,
                    Result = score.Result,
                    Score = score.Result == 0f ? maxScore : score.GroupNum != beforeScore.GroupNum ? (i = 1) : ++i,
                    Contestant = new Contestant {GroupNum = score.GroupNum}
                };
                result.Add(s);
                beforeScore = score;
            }
            return result;
        }

        private static int GetMaxScore(IList<RoundScorePostViewModel> scores)
        {
            return
                scores.GroupBy(s => s.GroupNum)
                    .Select(s => new {GroupNum = s.Key, Count = s.Count()})
                    .Max(t2 => t2.Count);
        }

        public static IList<RoundScore> AverageEqualScores(IList<RoundScore> scores)
        {
            var items = scores.OrderBy(s => s.Result).ToArray();
            for (var i = 0; i < items.Count(); i++)
            {
                var item = items[i];
                if (item.Result == 0)
                    continue;
                var ls =
                    scores.Where(
                        s => s.Result.Equals(item.Result) && (s.Contestant.GroupNum == item.Contestant.GroupNum));
                var lsCount = ls.Count();
                if (lsCount > 1)
                {
                    var ave = ls.Sum(s => s.Score)/lsCount;
                    foreach (var score in ls)
                        score.Score = ave;
                }
                i += lsCount;
            }
            return scores;
        }

        public static IList<ContestantCompetitionScore> Order(IList<ContestantCompetitionScore> scores)
        {
            var ls = scores.OrderBy(s => s.TotalScore).ToList();
            var i = 0;
            foreach (var t in ls)
                t.Order = ++i;
            for (i = 0; i < ls.Count();)
            {
                var item = ls[i];
                var ts = scores.Where(s => s.TotalScore.Equals(item.TotalScore)).ToList();
                var tsCount = ts.Count();
                if (tsCount > 1)
                {
                    var us = new List<RoundScore>();
                    foreach (var score in ts)
                        us.AddRange(score.RoundScores);
                    var uo = us.OrderBy(u => u.Score).ToList();

                    var k = 1;
                    var c = uo.First();
                    var orderData = new List<ContestantCompetitionScore>();
                    foreach (var score in uo)
                    {
                        if (score.Id != c.Id)
                            k++;
                        if (k > tsCount)
                            break;
                        if (ts.Any())
                        {
                            var b = ts.FirstOrDefault(t => t.Contestant.Id == score.Contestant.Id && !orderData.Contains(t));
                            if (b != null)
                            {
                                b.Order = i + k;
                                orderData.Add(b);
                                c = score;
                            }
                        }
                    }
                    orderData.Clear();
                    i += tsCount;
                }
                else
                {
                    i++;
                }
            }
            return ls.OrderBy(l => l.Order).ToList();
        }

        public static IList<ContestantCompetitionScore> Order1(IList<ContestantCompetitionScore> scores)
        {
            var ls = scores.OrderBy(s => s.TotalScore).ToList();
            var i = 0;
            foreach (var t in ls)
                t.Order = ++i;
            for (i = 0; i < ls.Count();)
            {
                var item = ls[i];
                var ts = scores.Where(s => s.TotalScore.Equals(item.TotalScore)).ToList();
                var tsCount = ts.Count();
                if (tsCount > 1)
                {
                    var b = ts.Select(ts1 => new { ts1, score =String.Join("", ts1.RoundScores.OrderBy(rs => rs.Score).Select(rs => Math.Round(rs.Score.Value)))});
                    var c = b.OrderBy(b1 => b1.score).ThenByDescending(b1 => b1.ts1.TotalResult);
                    foreach (var c1 in c)
                    {
                        c1.ts1.Order = ++i;
                    }
                    //i += tsCount;
                }
                else
                {
                    i++;
                }
            }
            return ls.OrderBy(l => l.Order).ToList();
        }
    }
}
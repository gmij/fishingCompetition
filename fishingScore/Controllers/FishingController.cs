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

            return View("Round", roundScore.Select(item => new RoundScoreViewModel() {
                Id = item.Id,
                Contestant = new ContestantViewModel
                {
                    Id = item.ContestantId, Number = item.Contestant.Number, GroupNum = item.Contestant.GroupNum, Name = item.Contestant.Name
                }}).OrderBy(item => item.Contestant.Number).ToList());
        }


        [HttpPost]
        public ActionResult Start(IList<RoundScorePostViewModel> models)
        {
            //todo: 保存到数据库中，然后提示开始下一层比赛
            return View("Round");
        }

        
    }
}
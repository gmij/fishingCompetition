using Microsoft.VisualStudio.TestTools.UnitTesting;
using fishingScore.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fishingScore.Models;
using fishingScore.Persistence;

namespace fishingScore.Controllers.Tests
{
    [TestClass()]
    public class FishingControllerTests
    {
        [TestMethod()]
        public void StatisticalScoreTest()
        {
            IList<RoundScorePostViewModel> scores = new List<RoundScorePostViewModel>()
            {
                new RoundScorePostViewModel() { GroupNum = "1", Id = "洪天然", Result = 8.72f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "礼斌", Result = 9.53f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "连顺义", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "陈嘉璋", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "邱运财", Result = 14.58f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "郑福钓", Result = 8.62f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "傅寿南", Result = 14.45f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "罗道炳", Result = 11.55f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "朱典尧", Result = 7.51f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "周建平", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "魏奕泰", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "游克仁", Result = 5.4f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "唐兆光", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "谢成宝", Result = 6.95f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林源", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "林炳通", Result = 5.08f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑坤海", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "张木华", Result = 13.15f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "俞关贵", Result = 10.99f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "潘金生", Result = 0f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林鸣国", Result = 0f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "赖凯文", Result = 8.36f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑汉聪", Result = 11.22f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "李占源", Result = 6.73f},

            };
            var result = FishingController.StatisticalScore(scores);
            foreach (var roundScore in result)
            {
                Console.WriteLine($"{roundScore.Id}: {roundScore.Score}");
            }
        }

        [TestMethod]

        public void StatisticalScoreTest相同成绩()
        {
            IList<RoundScorePostViewModel> scores = new List<RoundScorePostViewModel>()
            {
                new RoundScorePostViewModel() { GroupNum = "1", Id = "洪天然", Result = 8.72f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "礼斌", Result = 9.53f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "连顺义", Result = 1.1f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "陈嘉璋", Result = 1.2f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "邱运财", Result = 14.58f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "郑福钓", Result = 8.62f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "傅寿南", Result = 14.45f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "罗道炳", Result = 11.55f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "朱典尧", Result = 7.51f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "周建平", Result = 1.2f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "魏奕泰", Result = 1.1f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "游克仁", Result = 5.4f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "唐兆光", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "谢成宝", Result = 6.95f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林源", Result = 1.1f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "林炳通", Result = 5.08f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑坤海", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "张木华", Result = 13.15f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "俞关贵", Result = 10.99f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "潘金生", Result = 0f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林鸣国", Result = 0f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "赖凯文", Result = 8.36f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑汉聪", Result = 11.22f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "李占源", Result = 6.73f},

            };
            var result = FishingController.StatisticalScore(scores);
            result = FishingController.AverageEqualScores(result);
            foreach (var roundScore in result)
            {
                Console.WriteLine($"{roundScore.Id}: {roundScore.Score}");
            }
        }


        [TestMethod]

        public void StatisticalScoreRound2Test相同成绩()
        {
            #region Round 2

            IList<RoundScorePostViewModel> round2Scores = new List<RoundScorePostViewModel>()
            {
                new RoundScorePostViewModel() { GroupNum = "1", Id = "洪天然", Result = 20.87f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "礼斌", Result = 9.03f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "连顺义", Result = 10.23f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "陈嘉璋", Result = 6.27f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "邱运财", Result = 18.89f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "郑福钓", Result = 27.93f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "傅寿南", Result = 0f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "罗道炳", Result = 5.43f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "朱典尧", Result = 9.02f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "周建平", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "魏奕泰", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "游克仁", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "唐兆光", Result = 8.23f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "谢成宝", Result = 10.79f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林源", Result = 6.16f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "林炳通", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑坤海", Result = 14.24f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "张木华", Result = 10.27f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "俞关贵", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "潘金生", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林鸣国", Result = 5.85f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "赖凯文", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑汉聪", Result = 13.8f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "李占源", Result = 12.76f},

            };

            #endregion

            var result = FishingController.StatisticalScore(round2Scores);
            result = FishingController.AverageEqualScores(result);
            foreach (var roundScore in result)
            {
                Console.WriteLine($"{roundScore.Id}: {roundScore.Score}");
            }
        }


        [TestMethod]

        public void StatisticalScoreRound2ChangeTest相同成绩()
        {
            #region Round 2

            IList<RoundScorePostViewModel> round2Scores = new List<RoundScorePostViewModel>()
            {
                new RoundScorePostViewModel() { GroupNum = "1", Id = "洪天然", Result = 20.87f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "礼斌", Result = 9.03f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "连顺义", Result = 10.23f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "陈嘉璋", Result = 6.27f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "邱运财", Result = 18.89f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "郑福钓", Result = 27.93f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "傅寿南", Result = 1.1f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "罗道炳", Result = 5.43f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "朱典尧", Result = 9.02f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "周建平", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "魏奕泰", Result = 1.1f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "游克仁", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "唐兆光", Result = 8.23f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "谢成宝", Result = 10.79f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林源", Result = 6.16f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "林炳通", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑坤海", Result = 14.24f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "张木华", Result = 10.27f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "俞关贵", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "潘金生", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林鸣国", Result = 5.85f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "赖凯文", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑汉聪", Result = 13.8f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "李占源", Result = 12.76f},

            };

            #endregion

            var result = FishingController.StatisticalScore(round2Scores);
            result = FishingController.AverageEqualScores(result);
            foreach (var roundScore in result)
            {
                Console.WriteLine($"{roundScore.Id}: {roundScore.Score}");
            }
        }


        [TestMethod()]
        public void StatisticalScoreTestTwoRound()
        {
            #region Round 1

            

            IList<RoundScorePostViewModel> round1Scores = new List<RoundScorePostViewModel>()
            {
                new RoundScorePostViewModel() { GroupNum = "1", Id = "洪天然", Result = 8.72f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "礼斌", Result = 9.53f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "连顺义", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "陈嘉璋", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "邱运财", Result = 14.58f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "郑福钓", Result = 8.62f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "傅寿南", Result = 14.45f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "罗道炳", Result = 11.55f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "朱典尧", Result = 7.51f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "周建平", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "魏奕泰", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "游克仁", Result = 5.4f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "唐兆光", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "谢成宝", Result = 6.95f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林源", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "林炳通", Result = 5.08f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑坤海", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "张木华", Result = 13.15f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "俞关贵", Result = 10.99f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "潘金生", Result = 0f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林鸣国", Result = 0f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "赖凯文", Result = 8.36f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑汉聪", Result = 11.22f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "李占源", Result = 6.73f},

            };

            #endregion


            #region Round 2

            IList<RoundScorePostViewModel> round2Scores = new List<RoundScorePostViewModel>()
            {
                new RoundScorePostViewModel() { GroupNum = "1", Id = "洪天然", Result = 20.87f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "礼斌", Result = 9.03f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "连顺义", Result = 10.23f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "陈嘉璋", Result = 6.27f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "邱运财", Result = 18.89f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "郑福钓", Result = 27.93f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "傅寿南", Result = 0f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "罗道炳", Result = 5.43f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "朱典尧", Result = 9.02f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "周建平", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "魏奕泰", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "游克仁", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "唐兆光", Result = 8.23f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "谢成宝", Result = 10.79f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林源", Result = 6.16f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "林炳通", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑坤海", Result = 14.24f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "张木华", Result = 10.27f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "俞关贵", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "潘金生", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林鸣国", Result = 5.85f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "赖凯文", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑汉聪", Result = 13.8f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "李占源", Result = 12.76f},

            };

            #endregion

            var result1 = FishingController.StatisticalScore(round1Scores);
            result1 = FishingController.AverageEqualScores(result1);

            var result2 = FishingController.StatisticalScore(round2Scores);
            result2 = FishingController.AverageEqualScores(result2);

            var k = from t1 in result1
                join t2 in result2 on t1.Id equals t2.Id
                select new ContestantCompetitionScore(t1, t2);

            var ls = FishingController.Order(k.ToList());

            foreach (var score in ls)
            {
                Console.WriteLine($"{score.Order} ~~ {score.Name}: {score.TotalScore} - {score.TotalResult}");
            }
        }

        [TestMethod()]
        public void StatisticalScoreTest4Round()
        {
            #region Round 1



            IList<RoundScorePostViewModel> round1Scores = new List<RoundScorePostViewModel>()
            {
                new RoundScorePostViewModel() { GroupNum = "1", Id = "洪天然", Result = 8.72f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "礼斌", Result = 9.53f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "连顺义", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "陈嘉璋", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "邱运财", Result = 14.58f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "郑福钓", Result = 8.62f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "傅寿南", Result = 14.45f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "罗道炳", Result = 11.55f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "朱典尧", Result = 7.51f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "周建平", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "魏奕泰", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "游克仁", Result = 5.4f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "唐兆光", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "谢成宝", Result = 6.95f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林源", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "林炳通", Result = 5.08f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑坤海", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "张木华", Result = 13.15f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "俞关贵", Result = 10.99f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "潘金生", Result = 0f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林鸣国", Result = 0f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "赖凯文", Result = 8.36f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑汉聪", Result = 11.22f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "李占源", Result = 6.73f},

            };

            #endregion


            #region Round 2

            IList<RoundScorePostViewModel> round2Scores = new List<RoundScorePostViewModel>()
            {
                new RoundScorePostViewModel() { GroupNum = "1", Id = "洪天然", Result = 20.87f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "礼斌", Result = 9.03f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "连顺义", Result = 10.23f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "陈嘉璋", Result = 6.27f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "邱运财", Result = 18.89f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "郑福钓", Result = 27.93f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "傅寿南", Result = 0f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "罗道炳", Result = 5.43f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "朱典尧", Result = 9.02f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "周建平", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "魏奕泰", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "游克仁", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "唐兆光", Result = 8.23f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "谢成宝", Result = 10.79f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林源", Result = 6.16f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "林炳通", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑坤海", Result = 14.24f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "张木华", Result = 10.27f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "俞关贵", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "潘金生", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林鸣国", Result = 5.85f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "赖凯文", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑汉聪", Result = 13.8f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "李占源", Result = 12.76f},

            };

            #endregion

            #region Round 3



            IList<RoundScorePostViewModel> round3Scores = new List<RoundScorePostViewModel>()
            {
                new RoundScorePostViewModel() { GroupNum = "1", Id = "洪天然", Result = 21f},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "礼斌", Result = 7f},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "连顺义", Result = 6},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "陈嘉璋", Result = 23},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "邱运财", Result = 9},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "郑福钓", Result = 13},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "傅寿南", Result = 7},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "罗道炳", Result = 14},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "朱典尧", Result =11},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "周建平", Result = 15},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "魏奕泰", Result = 2},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "游克仁", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "唐兆光", Result = 13},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "谢成宝", Result = 5},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林源", Result = 5},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "林炳通", Result = 2},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑坤海", Result = 37},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "张木华", Result = 8},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "俞关贵", Result = 8},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "潘金生", Result = 1},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林鸣国", Result = 5},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "赖凯文", Result = 18},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑汉聪", Result = 4},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "李占源", Result = 6},

            };

            #endregion

            #region Round 4



            IList<RoundScorePostViewModel> round4Scores = new List<RoundScorePostViewModel>()
            {
                new RoundScorePostViewModel() { GroupNum = "1", Id = "洪天然", Result =3},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "礼斌", Result = 3},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "连顺义", Result = 1},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "陈嘉璋", Result = 3},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "邱运财", Result = 2},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "郑福钓", Result = 27},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "傅寿南", Result = 9},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "罗道炳", Result = 2},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "朱典尧", Result =16},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "周建平", Result = 4},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "魏奕泰", Result = 0},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "游克仁", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "唐兆光", Result = 17},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "谢成宝", Result = 18},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林源", Result = 1},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "林炳通", Result = 3},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑坤海", Result =12},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "张木华", Result = 5},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "俞关贵", Result = 2},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "潘金生", Result = 1},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "林鸣国", Result = 2},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "赖凯文", Result = 0},

                new RoundScorePostViewModel() { GroupNum = "1", Id = "郑汉聪", Result = 5},
                new RoundScorePostViewModel() { GroupNum = "2", Id = "李占源", Result = 2},

            };

            #endregion


            var result1 = FishingController.StatisticalScore(round1Scores);
            result1 = FishingController.AverageEqualScores(result1);

            var result2 = FishingController.StatisticalScore(round2Scores);
            result2 = FishingController.AverageEqualScores(result2);

            var result3 = FishingController.StatisticalScore(round3Scores);
            result3 = FishingController.AverageEqualScores(result3);

            var result4 = FishingController.StatisticalScore(round4Scores);
            result4 = FishingController.AverageEqualScores(result4);

            var k = from t1 in result1
                    join t2 in result2 on t1.Id equals t2.Id
                    join t3 in result3 on t1.Id equals t3.Id
                    join t4 in result4 on t1.Id equals t4.Id
                    select new ContestantCompetitionScore(t1, t2, t3, t4);

            var ls = FishingController.Order(k.ToList());

            foreach (var score in ls)
            {
                Console.WriteLine($"{score.Order} ~~ {score.Name}: {score.TotalScore} - {score.TotalResult}");
            }
        }

    }
}
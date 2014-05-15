using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WorldCup.Models.Rankings;

namespace WorldCup.Controllers
{
    [Authorize]
    public class RankingsController : ControllerBase
    {
        public ActionResult Index()
        {
            var model = from user in Context.Users.ToList()
                let matchPoints = user.MatchPoints.Sum(m => m.Points)
                let longRunningPoints = user.LongRunningPoints != null
                    ? (user.LongRunningPoints.SecondStagePoints +
                       user.LongRunningPoints.QuarterFinalPoints +
                       user.LongRunningPoints.SemiFinalPoints +
                       user.LongRunningPoints.SmallFinalPoints +
                       user.LongRunningPoints.FinalPoints +
                       user.LongRunningPoints.WinnerPoints)
                    : 0
                let totalPoints = matchPoints + longRunningPoints
                orderby totalPoints descending
                select  new UserRankingViewModel
                {
                    Name = user.FirstName + " " + user.LastName,
                    MatchPoints = matchPoints,
                    LongRunningPoints = longRunningPoints
                };

            model =
                model.Select(
                    (m, i) =>
                        new UserRankingViewModel
                        {
                            Postion = i + 1,
                            Name = m.Name,
                            MatchPoints = m.MatchPoints,
                            LongRunningPoints = m.LongRunningPoints
                        });

            return View(model.ToList().AsQueryable());
        }

        public JsonResult TopTenInfo()
        {
            var topTenUsers = (from user in Context.Users
                let userPoints = user.MatchPoints.Sum(m => m.Points)
                                 + ( user.LongRunningPoints.SecondStagePoints +
                                       user.LongRunningPoints.QuarterFinalPoints +
                                       user.LongRunningPoints.SemiFinalPoints +
                                       user.LongRunningPoints.SmallFinalPoints +
                                       user.LongRunningPoints.FinalPoints +
                                       user.LongRunningPoints.WinnerPoints
                                     )
                orderby userPoints descending
                select user).Take(10).ToList();

            var usersInfoPerDate = new List<Dictionary<string, object>>();

            // The date that tournament is finished
            //var today = DateTime.Now > new DateTime(2014,7,13) ? new DateTime(2014,7,13) : DateTime.Now;
            var today = new DateTime(2014, 6, 14);
            for(int i = -4; i <= 0; i++)
            {
                var userInfo = new Dictionary<string, object>();

                var date = today.AddDays(i);
                userInfo.Add("date", date.ToString("M"));

                foreach(var user in topTenUsers)
                {
                    userInfo.Add(user.Id, user.GetPointsForDate(date));
                }
                usersInfoPerDate.Add(userInfo);
            }

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    users = topTenUsers.Select(u => new {name = u.FullName, valueField = u.Id}).ToList(),
                    rankings = usersInfoPerDate
                }
            };
        }

    }
}
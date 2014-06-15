using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WorldCup.Models.Rankings;

namespace WorldCup.Controllers
{
    public class RankingsController : ControllerBase
    {
        public ActionResult Index()
        {
            var model = from user in UserManager.ConfirmedUsers.ToList()
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
                    UserName = user.Email,
                    MatchPoints = matchPoints,
                    LongRunningPoints = longRunningPoints
                };

            model = model.GroupBy(m => m.TotalPoints)
                .SelectMany((m, i) =>
                    m.Select(
                        viewModel =>
                            new UserRankingViewModel
                            {
                                Postion = i + 1,
                                UserName = viewModel.UserName,
                                Name = viewModel.Name,
                                MatchPoints = viewModel.MatchPoints,
                                LongRunningPoints = viewModel.LongRunningPoints
                            }));
            
            return View(model.ToList().AsQueryable());
        }

        public JsonResult TopUsersInfo(int numberOfUsers)
        {
            var topUsers = (from user in UserManager.ConfirmedUsers
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
                            select user).Take(numberOfUsers).ToList();

            // Array containing the colors of the graph
            string[] graphColors = { "#428bca", "#5cb85c", "#5bc0de", "#f0ad4e", "#d9534f" };

            var usersInfoPerDate = new List<Dictionary<string, object>>();

            // The date that tournament is finished
            var today = DateTime.Now > new DateTime(2014,7,13) ? new DateTime(2014,7,13) : DateTime.Now;
            for(int i = -4; i <= 0; i++)
            {
                var userInfo = new Dictionary<string, object>();
                
                var date = today.AddDays(i);
                userInfo.Add("date", date.ToString("M"));

                foreach(var user in topUsers)
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
                    users = topUsers.Select((u,i) => new { name = u.FullName, valueField = u.Id, color = graphColors[i%5] }).ToList(),
                    rankings = usersInfoPerDate
                }
            };
        }

    }
}
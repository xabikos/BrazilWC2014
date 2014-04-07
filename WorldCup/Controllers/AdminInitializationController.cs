using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using WorldCup.Common.Entities;

namespace WorldCup.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminInitializationController : ControllerBase
    {
        // GET: Initialization
        public ActionResult Index()
        {
            return View(model: "These are operations that should be done only once to initialize the system.");
        }
        
        public async Task<ActionResult> InitializeTeams()
        {
            if (Context.Teams.Any()) 
                return View("Index", model: "The teams have been already initialized");
            
            var url = new Uri("http://footballdb.herokuapp.com/api/v1/event/world.2014/teams");

            var httpClient = new HttpClient();
            var teamsAsJson = await httpClient.GetStringAsync(url);
            var allTeams = JsonConvert.DeserializeAnonymousType(teamsAsJson, new {teams = new List<Team>()});

            Context.Teams.AddRange(allTeams.teams);

            await Context.SaveChangesAsync();

            return View("Index", model:"You successfully initialized all teams");    
        }

        public async Task<ActionResult> InitializeMatches()
        {
            if (Context.Matches.Any())
                return View("Index", model: "The matches have been already initialized");

            if(!Context.Teams.Any())
                return View("Index", model: "You should first initialize teams and then matches");

            for (int i = 1; i < 16; i++)
            {
                var url = new Uri(string.Format("http://footballdb.herokuapp.com/api/v1/event/world.2014/round/{0}", i));
                var httpClient = new HttpClient();
                var matchesAsJson = await httpClient.GetStringAsync(url);
                var matches = JsonConvert.DeserializeAnonymousType(matchesAsJson, new {games = new List<Match>()});

                Context.Matches.AddRange(matches.games);
            }

            await Context.SaveChangesAsync();
           
            return View("Index", model:"You successfully initialized all matches");    
        } 

    }
}
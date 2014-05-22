using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldCup.Common.Entities;

namespace WorldCup.Models.Reports
{
    public class UserPredictionsReportsModel
    {

        public string UserName { get; set; }
        public IEnumerable<MatchPrediction> MatchPredictions { get; set; }
        public LongRunningPrediction LongRunningPredictions { get; set; }

    }
}
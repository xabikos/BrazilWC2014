using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using WorldCup.Common.Entities;

namespace WorldCup.Models.Predictions
{
    public class PredictionsViewModel
    {

        public IQueryable<Match> Matches { get; set; }

    }
}
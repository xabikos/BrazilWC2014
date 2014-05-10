using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsUpdater
{
    public class RankingsContext : DbContext
    {
        /// <summary>
        /// Null initializer so not to be able to change database schema
        /// </summary>
        static RankingsContext()
        {
            Database.SetInitializer<RankingsContext>(null);
        }

        public RankingsContext()
            : base("DefaultConnection")
        {
            
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarChart.Data;

namespace StarChart.Models
{
    public class CelestialObjectRepository : ICelestialObjectRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CelestialObjectRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<CelestialObject> CelestialObjects => dbContext.CelestialObjects;

     }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StarChart.Models
{
    public class CelestialObject
    {
        private DbContext dbContext;
        public int Id { get; set; }
        public string name { get; set; }

        public CelestialObject(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StarChart.Models
{
    public class CelestialObject
    {
        private DbContext dbContext;
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? OrbitedObjectId { get; set; }

        [NotMapped]
        public List<CelestialObject> Satellites { get; set; }
        public TimeSpan OrbitalPeriod{get;set;}

    public CelestialObject(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

}
}

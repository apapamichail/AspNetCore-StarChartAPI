﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarChart.Models
{
    public interface ICelestialObjectRepository
    {

        IEnumerable<CelestialObject> CelestialObjects { get; }
    }
}

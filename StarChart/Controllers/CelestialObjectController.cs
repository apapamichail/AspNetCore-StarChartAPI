using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarChart.Models;

namespace StarChart.Controllers
{
    public class CelestialObjectController : Controller
    {
         private readonly ICelestialObjectRepository celestialObjectRepository;

        public CelestialObjectController(ICelestialObjectRepository celestialObjectRepository)
        {
            this.celestialObjectRepository = celestialObjectRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(CelestialObject celestialObject)
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Post(CelestialObject celestialObject)
        {
            return View();
        }

        [HttpPut]
        public IActionResult Put(CelestialObject celestialObject)
        {
            return View();
        }

        [HttpPatch]
        public IActionResult Patch(CelestialObject celestialObject)
        {
            return View();
        }

        public CelestialObject GetById(int id)
        {
            return celestialObjectRepository.CelestialObjects.Where(x => x.Id == id).FirstOrDefault();

        }

        public CelestialObject GetByName(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            return celestialObjectRepository.CelestialObjects.Where(x => x.Name.Equals(name)).FirstOrDefault();

        }

        public IEnumerable<CelestialObject> GetAll()
        {
             return celestialObjectRepository.CelestialObjects;
        }
    }
}

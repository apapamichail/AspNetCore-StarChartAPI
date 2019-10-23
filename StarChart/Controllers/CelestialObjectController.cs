using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiControllerAttribute]
    public class CelestialObjectController : ControllerBase
    {
        ApplicationDbContext _context;
        private readonly ICelestialObjectRepository celestialObjectRepository;

        public CelestialObjectController(ICelestialObjectRepository celestialObjectRepository)
        {
            this.celestialObjectRepository = celestialObjectRepository;
        }

        public CelestialObjectController(ApplicationDbContext context)
        {
            this._context = context;
        }



        [HttpPost]
        public IActionResult Create([FromBody] CelestialObject celestialObject)
        {
            _context.CelestialObjects.Add(celestialObject);
            _context.SaveChanges();
            return CreatedAtRoute("GetById", new { id = celestialObject.Id, celestialObject });
        }

        /*
          Create the Delete method

    This method should have a return type of IActionResult
    This method should accept a parameter of type int named id.
    This method should have the HttpDelete attribute with an argument of "{id}".
    This method should get a List of all CelestialObjects who 
    either have an Id or OrbitedObjectIdthat matches the
    provided parameter.
        If there are no matches it should return NotFound.
        If there are matching CelestialObjects
        call RemoveRange on the CelestialObjects DbSet
        with an argument of the list of matching CelestialObjects. 
        
            Then call SaveChanges.
    This method should return NoContent.


             */
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            var objectsToDelete = _context.CelestialObjects.Where(x => x.Id == id || x.OrbitedObjectId == id);
            if (objectsToDelete is null)
            {
                return NotFound();
            }
            _context.CelestialObjects.RemoveRange(objectsToDelete);
            _context.SaveChanges();

            return NoContent();
        }

        /* Create the Update method

    This method should have a return type of IActionResult .
    This method should accept a parameter of type int named id and a parameter of type CelestialObject.
    This method should have the HttpPut attribute with a value of "{id}".
    This method should locate the CelestialObject with an Id that matches the provided int parameter.
        If no match is found return NotFound.
        If a match is found set it's Name, OrbitalPeriod, and OrbitedObjectId 
        properties based on the provided CelestialObject parameter. 
        Call Update on the CelestialObjects DbSet with an 
        argument of the updated CelestialObject, and then call SaveChanges.
       This method should return NoContent.

*/
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Update(int id, CelestialObject celestialObject)
        {
            var celestialObjectToUpdate = _context.CelestialObjects.Where(x => x.Id == id).FirstOrDefault();
            if (celestialObjectToUpdate is null)
            {
                return NotFound();
            }

            celestialObjectToUpdate.Name = celestialObject.Name;
            celestialObjectToUpdate.OrbitalPeriod = celestialObject.OrbitalPeriod;
            celestialObjectToUpdate.OrbitedObjectId = celestialObject.OrbitedObjectId;

            _context.Update(celestialObjectToUpdate);

            _context.SaveChanges();

            return NoContent();
        }
        /*
          Create the RenameObject method

    This method should have a return type of IActionResult.
    This method should accept a parameter of type int named id and a parameter of type string named name.
    This method should have the HttpPatch attribute with an argument of "{id}/{name}".
    This method should locate the CelestialObject with an Id that matches the provided int parameter.
    If no match is found return NotFound.
    If a match is found set it's Name property to the provided name parameter. 
    Then call Update on the CelestialObjects DbSet with an argument of the updated 
    CelestialObject, and then call SaveChanges.
    This method should return NoContent.
    */
        [HttpPatch("{id}/{name}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RenameObject(int id, string name)
        {
            var celestialObject = _context.CelestialObjects.Where(x => x.Id == id).FirstOrDefault();
            if (celestialObject is null)
            {
                return NotFound();
            }
            celestialObject.Name = name;
            _context.Update(celestialObject);
            _context.SaveChanges();
            return NoContent();

        }


        [HttpGet("{id:int}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {

            var celestialObject = (celestialObjectRepository.CelestialObjects.
                                    Where(x => x.Id == id).FirstOrDefault());
            if (celestialObject == null)
            {
                return NotFound();
            }

            if (celestialObject.Satellites == null)
            {
                celestialObject.Satellites = new List<CelestialObject>();
            }

            celestialObject.Satellites = GetSatelites(celestialObject);

            return Ok(celestialObject);
        }

        private List<CelestialObject> GetSatelites(CelestialObject celestialObject)
        {
            if (celestialObject == null)
            {
                return null;
            }
            var satelites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == celestialObject.Id).ToList();


            return satelites;
        }
        [HttpGet("{name}", Name = "GetByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetByName(string name)
        {
            if (name is null)
            {
                return NotFound();
            }
            var celestialObjects = celestialObjectRepository.CelestialObjects.Where(x => x.Name.Equals(name));
            if (celestialObjects is null)
            {
                return NotFound();
            }
            foreach (var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = GetSatelites(celestialObject);
            }

            return Ok(celestialObjects);
        }

        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var celestialObjects = celestialObjectRepository.CelestialObjects;
            foreach (var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = GetSatelites(celestialObject);
            }
            return Ok(celestialObjects);

        }
    }
}

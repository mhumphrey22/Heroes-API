using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeroesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        // GET api/heroes
        [HttpGet]
        public ActionResult<IEnumerable<Hero>> GetHeroes()
        {
            using (var db = new TourOfHeroesContext())
            {
                //  Return all Heroes
                return db.Hero;
            }
        }

        // GET api/heroes/5
        [HttpGet("{id}")]
        //[Route("api/heroes/getHero/{id}")]
        public ActionResult<Hero> GetHero(int id)
        {
            using (var db = new TourOfHeroesContext())
            {
                //  Get Hero with Matching Id
                return db.Hero
                         .Where(x => x.Id == id)
                         .FirstOrDefault();
            }
        }

        // POST api/heroes
        [HttpPost]
        [Route("api/heroes/saveHero")]
        public ActionResult<Hero> SaveHero([FromBody] Hero hero)
        {
            using (var db = new TourOfHeroesContext())
            {
                //  Determine state based off of Hero Id.  
                //  Id = 0 is a new Hero.
                db.Entry(hero).State = hero.Id == 0 ? EntityState.Added
                                                    : EntityState.Modified;

                db.SaveChanges();

                return hero;
            }
        }

        // DELETE api/values/5
        //[HttpDelete("{id}")]
        [HttpPost]
        [Route("api/heroes/deleteHero")]
        //public void DeleteHero(int id)
        public void DeleteHero([FromBody] Hero hero)  
        {
            using (var db = new TourOfHeroesContext())
            {
                ////  
                //var hero = db.Hero.Where(x => x.Id == id)
                //                  .FirstOrDefault();

                db.Hero.Remove(hero);
            }

        }
    }
}

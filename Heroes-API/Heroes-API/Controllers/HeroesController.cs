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
            var db = new TourOfHeroesContext();

            return db.Hero;
        }

        // GET api/heroes/5
        [HttpGet("{id}")]
        public ActionResult<Hero> GetHero(int id)
        {
            var db = new TourOfHeroesContext();

            return db.Hero
                     .Where(x => x.Id == id)
                     .FirstOrDefault();
        }

        // POST api/heroes
        [HttpPost]
        public ActionResult<Hero> Post([FromBody] Hero hero)
        {
            using (var db = new TourOfHeroesContext())
            {
                db.Entry(hero).State = hero.Id == 0 ? EntityState.Added
                                                    : EntityState.Modified;

                db.SaveChanges();

                return hero;
            }
        }

        // PUT api/heroes/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

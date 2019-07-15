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
        [HttpGet]
        public ActionResult<IEnumerable<Hero>> GetHeroes()
        {
            using (var db = new TourOfHeroesContext())
            {
                //  Return all Heroes
                return db.Hero.ToList();
            }
        }

        [HttpGet("{id}")]
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

        [HttpGet("search/{term}")]
        public ActionResult<IEnumerable<Hero>> SearchHeroes(string term)
        {
            using (var db = new TourOfHeroesContext())
            {
                //  Search for Heroes matching Term
                return db.Hero
                         .Where(x => x.Name.Contains(term))
                         .ToList();
            }
        }

        [HttpPost]
        [Route("saveHero")]
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

        [HttpPost]
        [Route("deleteHero")]
        //public void DeleteHero(int id)
        public void DeleteHero([FromBody] Hero hero)  
        {
            using (var db = new TourOfHeroesContext())
            {
                db.Hero.Remove(hero);

                db.SaveChanges();
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Globalque.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleDbContext db;

        public PeopleController(PeopleDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            var people = db.Set<Person>().OrderBy(p => p.Id).Take(10).ToList();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var person = db.Set<Person>().Find(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post(Person person)
        {
            if (person.Id != default(int))
            {
                return BadRequest("Can't supply the Id with POST");
            }

            db.Set<Person>().Add(person);
            db.SaveChanges();

            return Created(new Uri($"/{person.Id}", UriKind.Relative), person);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Person person)
        {
            if (person.Id != default(int) && person.Id != id)
            {
                return BadRequest("Id on person had unexpected value");
            }

            person.Id = id;
            db.Set<Person>().Update(person);
            db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = db.Set<Person>().Find(id);

            if (person == null)
            {
                return NotFound();
            }

            db.Set<Person>().Remove(person);
            db.SaveChanges();

            return NoContent();
        }
    }
}
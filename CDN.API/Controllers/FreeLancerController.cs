using CDN.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Azure;
using System.Net;
using System.Text.Json;


namespace CDN.API.Controllers
{
    public class FreeLancerController : Controller
    {
        private readonly DB _context;
        public FreeLancerController(DB context)
        {
            _context = context;
        }

        [HttpGet("api/FreeLancer")]
        public ActionResult<List<FreeLancer>> Get()
        {
            List<FreeLancer> result = _context.FreeLancers.OrderBy(x => x.ID).ToList();
            return result;
        }   

        [HttpGet("api/FreeLancer/{id}")]
        public ActionResult<FreeLancer> Get(int id)
        {
            var freeLancer = _context.FreeLancers.Find(id);
            if (freeLancer == null)
            {
                return NotFound();
            }
            return freeLancer;
        }

        [HttpPost("api/FreeLancer/Create")]
        public ActionResult Create([FromBody] FreeLancer freeLancer)
        {
            try
            {
                _context.FreeLancers.Add(freeLancer);
                _context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = freeLancer.ID }, freeLancer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating freelancer.");
            }
        }

        [HttpPut("api/FreeLancer/Update")]
        public ActionResult<FreeLancer> Update([FromBody] FreeLancer freeLancer)
        {
            try
            { 
                var freeLancerToUpdate = _context.FreeLancers.Find(freeLancer.ID); 
                if (freeLancerToUpdate == null)
                {
                    return NotFound($"Freelancer with Id = {freeLancer.ID} not found");
                }
                else
                {
                    freeLancerToUpdate.Username = freeLancer.Username;
                    freeLancerToUpdate.Mail = freeLancer.Mail;
                    freeLancerToUpdate.HP = freeLancer.HP;
                    freeLancerToUpdate.Skills = freeLancer.Skills;
                    freeLancerToUpdate.Hobby = freeLancer.Hobby;
                }
                _context.SaveChanges();
                return new JsonResult(new
                {
                    message = "Successfuly updated."
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        [HttpDelete("api/FreeLancer/Delete")]
        public ActionResult<FreeLancer> Delete(int id)
        {

            var freeLancerToDelete = _context.FreeLancers.Find(id);

            if (freeLancerToDelete == null)
                return NotFound($"Freelancer with Id = {id} not found");

            _context.FreeLancers.Remove(freeLancerToDelete);
            _context.SaveChanges(); 
            return new JsonResult(new
            {
                message = "Successfuly removed."
            });
        }

    }
}

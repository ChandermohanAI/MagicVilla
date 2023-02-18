using MagicVilla.Data;
using MagicVilla.Logging;
using MagicVilla.Model;
using MagicVilla.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        public VillaApiController(ApplicationDbContext db){
            _db = db;
        }
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas(){
            return Ok(_db.Villas);
            
        }

        [HttpGet("id")]
        public ActionResult<VillaDTO> GetVilla(int id){
            return Ok(_db.Villas.FirstOrDefault(u=>u.Id==id));
        }


        [HttpPost]
        public ActionResult<VillaDTO>  _Create(VillaCreateDTO v){
            var existingVilla =_db.Villas.FirstOrDefault(x => x.Name == v.Name);
            if (existingVilla != null)
            {
                return Conflict("Cannot create the Id because it already exists.");
            }
            Villa model = new Villa(){
                Amenityy = v.Amenityy,
                Details = v.Details,
                ImageUrl = v.ImageUrl,
                Name = v.Name,
                Ocuupancy = v.Ocuupancy,
                Rate = v.Rate,
                Sqft = v.Sqft
                };
                _db.Villas.Add(model);
                _db.SaveChanges();

                return Ok(model);
        }

        [HttpPut]
        public ActionResult Put(VillaUpdateDTO v)
        {
            var existingVilla = _db.Villas.FirstOrDefault(x => x.Id == v.Id);
            if (v == null)
            {
                return BadRequest("Cannot update, Enter valid Data");
            }
            Villa model = new Villa(){
                Amenityy = v.Amenityy,
                Details = v.Details,
                Id = v.Id,
                ImageUrl = v.ImageUrl,
                Name = v.Name,
                Ocuupancy = v.Ocuupancy,
                Rate = v.Rate,
                Sqft = v.Sqft
                };
                _db.Villas.Add(model);
                _db.SaveChanges();

                return Ok(model); 
            
        }


    [HttpDelete]
    [Route("{Id}")]
    public ActionResult Delete(int Id)
    {
        var existingVilla = _db.Villas.FirstOrDefault(x => x.Id == Id);
        if (existingVilla == null)
        {
            return NotFound();
        }
        else
        {
            _db.Villas.Remove(existingVilla);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
}
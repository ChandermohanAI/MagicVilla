using MagicVilla.Data;
using MagicVilla.Logging;
using MagicVilla.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly Ilogging _logger;

        public VillaApiController(Ilogging logger){
            _logger = logger;
        }

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
        public ActionResult<VillaDTO>  _Create(VillaDTO v){
            var existingVilla =_db.Villas.Find(x => x.Id == v.Id);
            if (existingVilla != null)
            {
                return Conflict("Cannot create the Id because it already exists.");
            }
            else
            {
                VillaStore.villaList.Add(v);
                return Ok(v);
            }
        }

        [HttpPut]
        public ActionResult Put(VillaDTO v)
        {
            var existingVilla = VillaStore.villaList.Find(x => x.Id == v.Id);
            if (existingVilla == null)
            {
                return BadRequest("Cannot update a non existing term.");
            } 
            else
            {
                existingVilla.Name = v.Name;
                return Ok();
            }
        }


    [HttpDelete]
    [Route("{Id}")]
    public ActionResult Delete(int Id)
    {
        var existingVilla = VillaStore.villaList.Find(x => x.Id == Id);
        if (existingVilla == null)
        {
            return NotFound();
        }
        else
        {
            VillaStore.villaList.Remove(existingVilla);
            return NoContent();
        }
    }
}
}
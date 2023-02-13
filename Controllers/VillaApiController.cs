using MagicVilla.Data;
using MagicVilla.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas(){
            return Ok(VillaStore.villaList);
            
        }

        [HttpGet("id")]
        public ActionResult<VillaDTO> GetVilla(int id){
            return Ok(VillaStore.villaList.FirstOrDefault(u=>u.Id==id));
        }


        [HttpPost]
        public ActionResult<VillaDTO>  _Create(VillaDTO v){
            var existingVilla = VillaStore.villaList.Find(x => x.Id == v.Id);
            if (existingVilla != null)
            {
                return Conflict("Cannot create the Id because it already exists.");
            }
            else
            {
                VillaStore.villaList.Add(v);
                var resourceUrl = Request.Path.ToString() + '/' + v.Id;
                return Created(resourceUrl, v);
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
        var superheroItem = VillaStore.villaList.Find(x => x.Id == Id);
        if (superheroItem == null)
        {
            return NotFound();
        }
        else
        {
            VillaStore.villaList.Remove(superheroItem);
            return NoContent();
        }
    }
}
}
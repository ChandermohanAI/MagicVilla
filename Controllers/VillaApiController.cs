using AutoMapper;
using MagicVilla.Data;
using MagicVilla.Logging;
using MagicVilla.Model;
using MagicVilla.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public VillaApiController(ApplicationDbContext db,IMapper mapper){
            _db = db;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas(){
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
            
        }

        [HttpGet("id")]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id){
        
            var villa = await _db.Villas.FirstOrDefaultAsync(u=>u.Id==id);
            return Ok(_mapper.Map<VillaDTO>(villa));
        }


        [HttpPost]
        public async Task<ActionResult<VillaDTO>>  _Create(VillaCreateDTO v){
            var existingVilla =await _db.Villas.FirstOrDefaultAsync(x => x.Name == v.Name);
            if (existingVilla != null)
            {
                return Conflict("Cannot create the Id because it already exists.");
            }

            Villa model = _mapper.Map<Villa>(v);
            // Villa model = new Villa(){
            //     Amenityy = v.Amenityy,
            //     Details = v.Details,
            //     ImageUrl = v.ImageUrl,
            //     Name = v.Name,
            //     Ocuupancy = v.Ocuupancy,
            //     Rate = v.Rate,
            //     Sqft = v.Sqft
            //     };
                await _db.Villas.AddAsync(model);
                await _db.SaveChangesAsync();

                return Ok(model);
        }

        [HttpPut]
        public async Task<ActionResult> Put(VillaUpdateDTO v)
        {
            var existingVilla = await _db.Villas.FirstOrDefaultAsync(x => x.Id == v.Id);
            if (v == null)
            {
                return BadRequest("Cannot update, Enter valid Data");
            }
            Villa model = _mapper.Map<Villa>(v);
            // Villa model = new Villa(){
            //     Amenityy = v.Amenityy,
            //     Details = v.Details,
            //     Id = v.Id,
            //     ImageUrl = v.ImageUrl,
            //     Name = v.Name,
            //     Ocuupancy = v.Ocuupancy,
            //     Rate = v.Rate,
            //     Sqft = v.Sqft
            //     };
                await _db.Villas.AddAsync(model);
                await _db.SaveChangesAsync();

                return Ok(model); 
            
        }


    [HttpDelete]
    [Route("{Id}")]
    public async Task<ActionResult> Delete(int Id)
    {
        var existingVilla = await _db.Villas.FirstOrDefaultAsync(x => x.Id == Id);
        if (existingVilla == null)
        {
            return NotFound();
        }
        else
        {
            _db.Villas.Remove(existingVilla);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
}
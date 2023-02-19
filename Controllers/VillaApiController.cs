using System.Net;
using AutoMapper;
using MagicVilla.Data;
using MagicVilla.Logging;
using MagicVilla.Model;
using MagicVilla.Model.DTO;
using MagicVilla.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {

        protected APIResponse _response;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public VillaApiController(IVillaRepository dbVilla,IMapper mapper){
            _dbVilla = dbVilla;
            _mapper = mapper;
            this._response = new();
        }
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillas(){

            IEnumerable<Villa> villaList = await _dbVilla.GetAll();
            _response.Result = _mapper.Map<List<VillaDTO>>(villaList);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
            
        }

        [HttpGet("id")]
        public async Task<ActionResult<APIResponse>> GetVilla(int id){
        
            var villa = await _dbVilla.Get(u=>u.Id==id);
            if(villa==null){
                return NotFound();
            }
            _response.Result = _mapper.Map<VillaDTO>(villa);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
            
        }


        [HttpPost]
        public async Task<ActionResult<APIResponse>>  _Create(VillaCreateDTO v){
            var existingVilla =await _dbVilla.Get(x => x.Name == v.Name);
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
            await _dbVilla.Create(model);
            _response.Result = _mapper.Map<VillaDTO>(model);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPut]
        public async Task<ActionResult<APIResponse>> Put(VillaUpdateDTO v)
        {
            var existingVilla = await _dbVilla.Get(x => x.Id == v.Id);
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
            await _dbVilla.Update(model);
            _response.Result = _mapper.Map<VillaDTO>(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
            
        }


    [HttpDelete]
    [Route("{Id}")]
    public async Task<ActionResult<APIResponse>> Delete(int Id)
    {
        var existingVilla = await _dbVilla.Get(x => x.Id == Id);
        if (existingVilla == null)
        {
            return NotFound();
        }
        else
        {
            await _dbVilla.Remove(existingVilla);
            _response.Result = _mapper.Map<VillaDTO>(existingVilla);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
            
        }
    }
}
}
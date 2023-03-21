using System.Net;
using AutoMapper;
using MagicVilla.Model;
using MagicVilla.Model.DTO;
using MagicVilla.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers.V1
{
    [Route("api/v{version:apiVersion}/VillaAPI")]
    [ApiController]
    [ApiVersion("1.0")]
    public class VillaApiV1Controller : ControllerBase
    {

        protected APIResponse _response;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public VillaApiV1Controller(IVillaRepository dbVilla,IMapper mapper){
            _dbVilla = dbVilla;
            _mapper = mapper;
            this._response = new();
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillas(){

            IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();
            _response.Result = _mapper.Map<List<VillaDTO>>(villaList);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
            
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<APIResponse>> GetVilla(int id){
        
            var villa = await _dbVilla.GetAsync(u=>u.Id==id);
            if(villa==null){
                return NotFound();
            }
            _response.Result = _mapper.Map<VillaDTO>(villa);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
            
        }


        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<APIResponse>> _Create([FromBody] VillaCreateDTO v){
            var existingVilla =await _dbVilla.GetAsync(x => x.Name == v.Name);
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
            await _dbVilla.CreateAsync(model);
            _response.Result = _mapper.Map<VillaDTO>(model);
            _response.StatusCode = HttpStatusCode.OK;
            return CreatedAtRoute("GetVilla", new { id = model.Id }, _response);
        }

        
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<APIResponse>> Put(VillaUpdateDTO v)
        {
            var existingVilla = await _dbVilla.GetAsync(x => x.Id == v.Id);
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
            await _dbVilla.UpdateAsync(model);
            _response.Result = _mapper.Map<VillaDTO>(model);
            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
            
        }


        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            var existingVilla = await _dbVilla.GetAsync(x => x.Id == id);
            if (existingVilla == null)
            {
                return NotFound();
            }
            else
            {
                await _dbVilla.RemoveAsync(existingVilla);
                _response.Result = _mapper.Map<VillaDTO>(existingVilla);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
        }

}
}

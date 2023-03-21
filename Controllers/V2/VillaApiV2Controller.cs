using System.Net;
using AutoMapper;
using MagicVilla.Model;
using MagicVilla.Model.DTO;
using MagicVilla.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla.Controllers.V2
{
    [Route("api/v{version:apiVersion}/VillaAPI")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VillaApiV2Controller : ControllerBase
    {

        protected APIResponse _response;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        public VillaApiV2Controller(IVillaRepository dbVilla,IMapper mapper){
            _dbVilla = dbVilla;
            _mapper = mapper;
            this._response = new();
        }


        [HttpGet]
        public IEnumerable<string> Get(){
            return new string[] {"value1","value2"};
        }


        } 
}


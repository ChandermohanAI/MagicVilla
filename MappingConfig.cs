using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MagicVilla.Model;
using MagicVilla.Model.DTO;

namespace MagicVilla
{
    public class MappingConfig : Profile
    {
        public MappingConfig(){
            
            CreateMap<Villa,VillaDTO>();
            CreateMap<VillaDTO, Villa>();

            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();

            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();



        }
        

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicVilla.Model.DTO;

namespace MagicVilla.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>{
                new VillaDTO{Id=1,Name="Pool side"},
                new VillaDTO{Id=2,Name="Beach side"}
        };
    }
}
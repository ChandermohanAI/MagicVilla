using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicVilla.Logging
{
    public class Logging : Ilogging
    {
        public void Log(string message, string type)
        {
            if(type=="error"){
                Console.WriteLine("Error :"+message);
            }
            else{
                Console.WriteLine(message);
            }
        }
    }
}
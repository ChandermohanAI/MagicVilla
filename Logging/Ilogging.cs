using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MagicVilla.Logging
{
    public interface Ilogging
    {
        public void Log(string message,string type);
    }
}
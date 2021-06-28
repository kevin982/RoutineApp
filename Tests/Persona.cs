using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class Persona
    {
        public int Edad { get; set; } = 0;
 
        public bool EsMayorDeEdad() => Edad >= 18;


    }
}

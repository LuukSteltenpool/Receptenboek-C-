using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receptenboek_C_
{
    public class Recipe
    {
        public string name { get; set; }
        public string description { get; set; }
        public string ingredients { get; set; }

        List<string> ingredientsList = new List<string> { }; 
        public int preperationTime { get; set; }
        public string instructions { get; set; }
    }

}

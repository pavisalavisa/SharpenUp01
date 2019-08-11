using System.Collections;
using System.Collections.Generic;
using DynamicKeywordShowcase.Enums;

namespace DynamicKeywordShowcase.Models
{
    public class Engine
    {
        public string CodeName { get; set; }
        public int Displacement { get; set; }
        public FuelType FuelType { get; set; }
        public int PowerInKw { get; set; }
        public ICollection<Car> Cars { get; set; }

        public Engine()
        {
            Cars=new List<Car>();
        }
    }
}

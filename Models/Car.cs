using System;
using System.Collections.Generic;
using DynamicKeywordShowcase.Enums;

namespace DynamicKeywordShowcase.Models
{
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime FirstRegistrationDate { get; set; }
        public HeadlightType HeadlightType { get; set; }
        public Engine Engine { get; set; }
        public ICollection<Owner> PreviousOwners { get; set; }
        public int NumberOfPreviousOwners => PreviousOwners.Count;

        public Car()
        {
            PreviousOwners=new List<Owner>();
        }
    }
}

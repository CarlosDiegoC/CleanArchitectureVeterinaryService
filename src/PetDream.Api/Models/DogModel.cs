using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetDream.Api.Models
{
    public class DogModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bred_For { get; set; }
        public string Breed_Group { get; set; }
        public string Life_Span { get; set; }
        public string Temperament { get; set; }
        public string Origin { get; set; }
        public Height Height { get; set; }
        public Weight Weight { get; set; }
    }
}
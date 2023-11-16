using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace ModelFirst.Models
{
    public class PersonCars
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public int CarId { get; set; }
        public Person Person { get; set; }
        public Car Car { get; set; }
    }

}

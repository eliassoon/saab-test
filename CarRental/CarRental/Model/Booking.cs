using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Model
{
    public class Booking
    {

        public int Id { get; set; }

        public string SocialSecurity { get; set; }

        public string CarType { get; set; }

        public DateTime StartDate { get; set; }

        public int StartKilometers { get; set; }

        public bool Returned { get; set; }

    }
}

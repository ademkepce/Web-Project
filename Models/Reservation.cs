using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        [DisplayName("Email")]
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public ApplicationUser ApplicationUser { get; set; }
        public int HowManyPeople { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; }
    }
}

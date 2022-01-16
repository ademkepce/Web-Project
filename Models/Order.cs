using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Models
{
    public class Order
    {
        public int ID { get; set; }
        [DisplayName("Food Name")]
        public int? FoodId { get; set; }
        [ForeignKey("FoodId")]
        public Menu Menu { get; set; }
        [DisplayName("Email")]
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public ApplicationUser ApplicationUser { get; set; }
        public int Amount { get; set; }
        public int TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }

    public enum OrderStatus
    {
        Hazirlaniyor,
        Yolda,
        TeslimEdildi,
        IadeEdildi
    }
}

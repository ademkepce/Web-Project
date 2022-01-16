using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Models
{
    public class Menu
    {
        public int ID { get; set; }
        public string FoodName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        [DisplayName("Image")]
        public string ImageURL { get; set; }
        [DisplayName("Category Name")]
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

    }
}

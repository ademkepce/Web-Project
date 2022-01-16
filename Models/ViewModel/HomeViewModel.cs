using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Models.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Menu> Menu { get; set; }
        public IEnumerable<Discount> Discount { get; set; }
        public IEnumerable<Comments> Comments { get; set; }
    }
}

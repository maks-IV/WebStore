using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Details { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

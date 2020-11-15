using Demo.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Application.Models
{
    public class ProductModel : BaseModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public double Star { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

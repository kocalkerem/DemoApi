﻿using System;
using System.Collections.Generic;
using System.Text;
using Demo.Core.Entities.Base;

namespace Demo.Core.Entities
{
    public class Product: Entity
    { 
        public string Name { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
    }
}

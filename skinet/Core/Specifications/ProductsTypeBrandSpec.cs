﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductsTypeBrandSpec : BaseSpecification<Product>
    {
        public ProductsTypeBrandSpec()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        public ProductsTypeBrandSpec(int id) : base(x => x.Id == id)
        {

            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}

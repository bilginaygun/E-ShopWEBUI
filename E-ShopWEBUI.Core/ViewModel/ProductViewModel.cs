﻿using E_ShopWEBUI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ShopWEBUI.Core.ViewModel
{
    public class ProductViewModel
    {
        public List<ProductDTO> Products { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}

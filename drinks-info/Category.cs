﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drinks_info
{
    public class Category
    {
        public string catStr { get; set; }
    }

    public class Categories
    {
        [JsonProperty("drinks")]
        public List<Category> List { get; set; }
    }
}

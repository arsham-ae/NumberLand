﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.Models.Blogs
{
    public class BlogCategoryModel
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ICollection<BlogCategoryJoinModel> blogCategories { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberLand.Models.Pages
{
    public class PageeModel
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int categoryId { get; set; }
        [ForeignKey("categoryId")]
        public PageCategoryModel category { get; set; }
        public bool isVisible { get; set; }
    }
}
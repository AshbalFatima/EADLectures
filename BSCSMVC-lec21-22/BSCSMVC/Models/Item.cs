using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BSCSMVC.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string CategoryText { get; set; }
        public ICollection<Item> Items { get; set; }   

    }

}
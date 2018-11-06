using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Module.Stooges.Model
{
    public class SFile
    {
        [Required]
        public string src { get; set; }

        [Required]
        [Range(0, double.PositiveInfinity)]
        public double size { get; set; }

        [Required]
        public string name { get; set; }
    }
}

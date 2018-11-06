using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Module.Stooges.Model
{
    public class SImage : SFile
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int width { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int height { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceExample.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MineType { get; set; }
        public string Size { get; set; }
        public DateTime DateAdd { get; set; }
    }
}

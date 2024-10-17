using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiderPro.Core.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Place> Places { get; set; } = new List<Place>();
    }
}

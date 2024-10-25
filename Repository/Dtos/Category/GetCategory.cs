using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Dtos.Category
{
    public class GetCategory
    {
        public string Name { get; set; } = null!;

        public string? Status { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}

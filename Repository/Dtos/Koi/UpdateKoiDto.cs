using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Dtos.Koi
{
    public class UpdateKoiDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Variety { get; set; }
        public int? Size { get; set; }
        public IFormFile Avatar { get; set; }
        public string Description { get; set; }
    }
}

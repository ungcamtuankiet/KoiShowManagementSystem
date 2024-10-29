using System;
using System.Collections.Generic;
using Repository.Entities;

namespace Repository.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Competition> Competitions { get; set; } = new List<Competition>();
}

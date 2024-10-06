using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Competition> Competitions { get; set; } = new List<Competition>();

    public virtual ICollection<KoiRegistration> KoiRegistrations { get; set; } = new List<KoiRegistration>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}

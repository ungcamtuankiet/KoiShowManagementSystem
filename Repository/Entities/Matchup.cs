using System;
using System.Collections.Generic;

namespace Repository.Entities;
public partial class Matchup
{
    public int Id { get; set; }

    public int? CompetitionId { get; set; }

    public int? KoiId1 { get; set; }

    public int? KoiId2 { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Competition? Competition { get; set; }

    public virtual KoiFish? KoiId1Navigation { get; set; }

    public virtual KoiFish? KoiId2Navigation { get; set; }
}

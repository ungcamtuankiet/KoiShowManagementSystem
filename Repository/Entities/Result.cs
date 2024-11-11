using System;
using System.Collections.Generic;

namespace Repository.Entities;

public partial class Result
{
    public int Id { get; set; }

    public int? KoiId { get; set; }

    public int? CompetitionId { get; set; }

    public string? Rank { get; set; }

    public string? ResultKoi { get; set; }

    public decimal? TotalScore { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Competition? Competition { get; set; }

    public virtual KoiFish? Koi { get; set; }

    public virtual ICollection<ResultDetail> ResultDetails { get; set; } = new List<ResultDetail>();
}

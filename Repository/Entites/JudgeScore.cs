using System;
using System.Collections.Generic;

namespace Repository.Entites;

public partial class JudgeScore
{
    public int ScoreId { get; set; }

    public int? UserId { get; set; }

    public int? KoiId { get; set; }

    public int? CompetitionId { get; set; }

    public decimal? BodyScore { get; set; }

    public decimal? ColorScore { get; set; }

    public decimal? PatternScore { get; set; }

    public decimal? TotalScore { get; set; }

    public string? Comments { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Competition? Competition { get; set; }

    public virtual KoiFish? Koi { get; set; }

    public virtual User? User { get; set; }
}

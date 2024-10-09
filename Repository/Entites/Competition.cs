using System;
using System.Collections.Generic;

namespace Repository.Entites;

public partial class Competition
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Location { get; set; }

    public string? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<JudgeScore> JudgeScores { get; set; } = new List<JudgeScore>();

    public virtual ICollection<KoiRegistration> KoiRegistrations { get; set; } = new List<KoiRegistration>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}

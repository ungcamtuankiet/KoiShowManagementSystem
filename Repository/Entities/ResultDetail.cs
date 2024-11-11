using System;
using System.Collections.Generic;

namespace Repository.Entities;
public partial class ResultDetail
{
    public int Id { get; set; }

    public int? ResultId { get; set; }

    public int? KoiWin { get; set; }

    public string? Status { get; set; }

    public int? Top { get; set; }

    public virtual KoiFish? KoiWinNavigation { get; set; }

    public virtual Result? Result { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CompetitionResult
{
    public int? CompetitionResultID { get; set; }
    public string CompetitionName { get; set; }
    public DateTime CompetitionDate { get; set; }
    public int TotalParticipants { get; set; }
    public int TotalKoiFish { get; set; }
    public string WinnerName { get; set; }
    public string RunnerUpName { get; set; }
}


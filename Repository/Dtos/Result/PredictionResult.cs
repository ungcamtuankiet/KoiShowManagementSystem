using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PredictionResult
{
    public int? PredictionResultID { get; set; }
    public string ParticipantName { get; set; }
    public string PredictedWinner { get; set; }
    public double PredictionAccuracy { get; set; }
}

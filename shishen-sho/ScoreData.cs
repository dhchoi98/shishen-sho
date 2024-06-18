using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shishen_sho
{
    public class ScoreData
    {
        public string Mode { get; set; }
        public int Score { get; set; }

        public ScoreData(string mode, int score)
        {
            Mode = mode;
            Score = score;
        }
    }
}

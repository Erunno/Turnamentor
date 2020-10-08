using System;
using System.Collections.Generic;
using System.Text;

namespace Turnamentor.Interfaces
{
    public interface ITurnamentor<ScoreBoard, Score> where ScoreBoard : IScoreBoard<Score>
    {
        ScoreBoard RunTurnament();
    }
}

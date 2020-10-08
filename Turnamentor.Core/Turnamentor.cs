using System;
using System.Collections.Generic;
using System.Text;
using Turnamentor.Interfaces;

namespace Turnamentor.Core
{
    class Turnamentor<ScoreBoard, Score> : ITurnamentor<ScoreBoard, Score> where ScoreBoard : IScoreBoard<Score>
    {
        public ScoreBoard RunTurnament()
        {
            throw new NotImplementedException();
        }
    }
}

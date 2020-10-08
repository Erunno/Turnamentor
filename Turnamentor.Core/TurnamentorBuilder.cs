using System;
using Turnamentor.Interfaces;

namespace Turnamentor.Core
{
    public class TurnamentorBuilder<Contestant, Score, ScoreBoard> where ScoreBoard : IScoreBoard<Score>
    {
        ITurnamentor<ScoreBoard, Score> BuildTurnamentor() => throw new NotImplementedException();
    }
}

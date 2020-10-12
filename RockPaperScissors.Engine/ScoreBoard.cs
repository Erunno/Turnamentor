using System;
using System.Collections.Generic;
using System.Text;
using Turnamentor.Interfaces;

namespace RockPaperScissors.Engine
{
    public class ScoreBoard : IScoreBoard<(IAlgorithm, int, IAlgorithm, int)>
    {
        public Dictionary<(IAlgorithm, IAlgorithm), (int, int)> Results { get; set; } = new Dictionary<(IAlgorithm, IAlgorithm), (int, int)>();

        public void Add((IAlgorithm, int, IAlgorithm, int) score)
        {
            var (alg1, score1, alg2, score2) = score;
            Results.Add((alg1, alg2), (score1, score2));
        }
    }
}

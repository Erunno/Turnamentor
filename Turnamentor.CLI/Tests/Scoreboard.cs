using System;
using System.Collections.Generic;
using System.Text;

namespace Turnamentor.CLI.Tests
{
    class Scoreboard : Turnamentor.Interfaces.IScoreBoard<(IContestant, int)>
    {
        Dictionary<IContestant, int> dictionary = new Dictionary<IContestant, int>();
        public void Add((IContestant, int) score)
        {
            if (!dictionary.ContainsKey(score.Item1))
            {
                dictionary[score.Item1] = 0;
            }
            dictionary[score.Item1] += score.Item2;
        }
    }
}

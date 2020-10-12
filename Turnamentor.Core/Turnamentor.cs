using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turnamentor.Interfaces;

namespace Turnamentor.Core
{
    class Turnamentor<ScoreBoard, Score> : ITurnamentor<ScoreBoard, Score> where ScoreBoard : IScoreBoard<Score>
    {
        public Func<dynamic> EngineProvider { get; set; }

        public ScoreBoard ScoreBoardInstance { get; set; }

        public IEnumerable<IList<dynamic>> AllRounds { get; set; }

        public ScoreBoard RunTurnament()
        {
            dynamic engine = EngineProvider();

            foreach (var round in AllRounds)
            {
                Score s = engine.Play(ToConstestantList(round));
                ScoreBoardInstance.Add(s);
            }

            return ScoreBoardInstance;
        }
        
        public Type listOfContestantsType { get; set; }
        
        public void SetContstantType<Contestant>()
        {
            listOfContestantsType = typeof(List<>).MakeGenericType(typeof(Contestant));
        }

        private dynamic ToConstestantList(IList<dynamic> enumerable)
        {
            dynamic list = Activator.CreateInstance(listOfContestantsType);

            foreach (var item in enumerable)
                list.Add(item);

            return list;
        }
    }
}

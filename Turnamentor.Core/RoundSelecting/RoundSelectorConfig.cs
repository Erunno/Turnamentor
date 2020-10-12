using System;
using System.Collections.Generic;
using System.Text;

namespace Turnamentor.Core.RoundSelecting
{
    class RoundSelectorConfig
    {
        public int NumberOfContestantsInRound { get; set; }
        public List<Type> Contestants { get; set; }
    }
}

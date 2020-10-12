using System;
using System.Collections.Generic;
using System.Text;

namespace Turnamentor.Core.RoundSelecting
{
    interface IRoundSelector<Contestant>
    {
        IEnumerable<IList<Contestant>> GetAllRounds();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Turnamentor.Interfaces;
using System.Linq;

namespace Turnamentor.CLI.Tests
{
    class Engine : IGameEngine<IContestant, (IContestant, int)>
    {
        public (IContestant, int) Play(IList<IContestant> contestants)
        {
            return (contestants[0], contestants[0].Score);
        }
    }
}

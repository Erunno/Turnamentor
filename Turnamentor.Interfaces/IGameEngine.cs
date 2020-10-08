using System;
using System.Collections;
using System.Collections.Generic;

namespace Turnamentor.Interfaces
{
    public interface IGameEngine<Contestant, Score>
    {
        Score Play(IList<Contestant> contestants);
    }
}

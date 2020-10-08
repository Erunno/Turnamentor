using System;
using System.Collections.Generic;
using System.Text;

namespace Turnamentor.Interfaces
{
    public interface IScoreBoard<Score>
    {
        void Add(Score score);
    }
}

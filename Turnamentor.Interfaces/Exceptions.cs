using System;
using System.Collections.Generic;
using System.Text;

namespace Turnamentor.Interfaces
{
    public class NoInstanceOfGameEngineFoundException : Exception { }
    public class NoEmptyConstructorFoundException : Exception { }
    public class NoContestantFoundException : Exception { }
    public class AmbiguousGameEngine : Exception { }
}

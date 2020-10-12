using System;
using System.Collections.Generic;
using System.Text;

namespace RockPaperScissors.Engine
{
    public enum Move { Rock, Paper, Scissors }

    public interface IAlgorithm
    {
        Move GetNextMove();
        void RegisterOponentsMove(Move move);

        /// <summary>
        /// Every instance of same type returns same value.
        /// </summary>
        string GetAlgorithName();
    }
}

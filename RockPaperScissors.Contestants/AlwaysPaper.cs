using System;
using RockPaperScissors.Engine;

namespace RockPaperScissors.Contestants
{
    public class AlwaysPaper : IAlgorithm
    {
        public string GetAlgorithName() => "Always Paper";

        public Move GetNextMove() => Move.Paper;

        public void RegisterOponentsMove(Move move)
        {
            // paper rules
        }
    }
}

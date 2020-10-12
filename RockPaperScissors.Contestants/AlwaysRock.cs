using System;
using RockPaperScissors.Engine;

namespace RockPaperScissors.Contestants
{
    public class AlwaysRock : IAlgorithm
    {
        public string GetAlgorithName() => "Always Rock";

        public Move GetNextMove() => Move.Rock;

        public void RegisterOponentsMove(Move move)
        {
            // rock rules
        }
    }
}

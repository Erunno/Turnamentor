using System;
using RockPaperScissors.Engine;

namespace RockPaperScissors.Contestants
{
    public class AlwaysScissors : IAlgorithm
    {
        public string GetAlgorithName() => "Always Scissors";

        public Move GetNextMove() => Move.Scissors;

        public void RegisterOponentsMove(Move move)
        {
            // scissors rules
        }
    }
}

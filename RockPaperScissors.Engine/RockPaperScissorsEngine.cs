using System;
using System.Collections.Generic;
using Turnamentor.Interfaces;

namespace RockPaperScissors.Engine
{
    public class RockPaperScissorsEngine : IGameEngine<IAlgorithm, (IAlgorithm, int, IAlgorithm, int)>
    {

        /// <summary>
        /// Decides which alghorithm is better.
        /// </summary>
        /// <returns>Winning algorithm.</returns>
        public (IAlgorithm, int, IAlgorithm, int) Play(IList<IAlgorithm> contestants)
        {
            var alg1 = contestants[0];
            var alg2 = contestants[1];

            int score1 = 0, score2 = 0;

            for (int i = 0; i < 100; i++)
            {
                while(score1 < 3 && score2 < 3)
                {
                    Move m1 = alg1.GetNextMove();
                    Move m2 = alg2.GetNextMove();

                    bool alg1Won = (m1, m2) switch
                    {
                        (Move.Rock, Move.Scissors) => true,
                        (Move.Paper, Move.Rock) => true,
                        (Move.Scissors, Move.Paper) => true,
                        _ => false
                    };

                    if (alg1Won)
                        score1++;
                    else
                        score2++;

                    alg1.RegisterOponentsMove(m2);
                    alg2.RegisterOponentsMove(m1);
                }
            }

            return (alg1, score1, alg2, score2);
        }
    }
}

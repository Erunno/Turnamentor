using RockPaperScissors.Contestants;
using RockPaperScissors.Engine;
using System;
using Turnamentor.Core;
using Turnamentor.Interfaces;

namespace RockPaperScissors.ConsoleTurnament
{
    class Program
    {
        static void Main(string[] args)
        {
            var turnamentorBuilder = new TurnamentorBuilder<IAlgorithm, (IAlgorithm, int, IAlgorithm, int), ScoreBoard>()
                .SetGameEngine<RockPaperScissorsEngine>()
                //.AddContestants(ClassSource.SourceFile, @"C:\Users\mbrabec\source\repos\Turnamentor\RockPaperScissors.Contestants\bin\Debug\netcoreapp3.1")
                .AddContestants(ClassSource.SourceFiles, @"C:\Users\mbrabec\source\repos\Turnamentor\RockPaperScissors.Engine")
                .SetNumberOfContestantsInRound(2);

            var turnamentor = turnamentorBuilder.BuildTurnamentor();
            var scoreBoard = turnamentor.RunTurnament();

            foreach (var ((alg1,alg2),(score1,score2)) in scoreBoard.Results)
                Console.WriteLine($"{alg1.GetAlgorithName()} vs {alg2.GetAlgorithName()}: ({score1},{score2})");
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using Turnamentor.Core;
using Turnamentor.Interfaces;
using System.Reflection;

namespace Turnamentor.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[]
            { "Turnamentor.CLI.dll",
                "Turnamentor.CLI.dll",
                "1",
                @"..\..\..\Tests\inner"};
            if (args.Length < 3){
                PrintHelp();
            }else
            {
                string enginePath = args[0];
                string scoreBoardPath = args[1];
                int toSkip = 2;
                if (int.TryParse(args[2], out int playerCount))
                {
                    toSkip++;
                }
                else playerCount = DefaultPlayerCount;
                IEnumerable<string> contestnts = args.Skip(toSkip);
                TryRun(enginePath, scoreBoardPath, contestnts, playerCount);
            }
        }

        static void PrintHelp()
        {
            Console.WriteLine("Vypisuju help pico");
        }

        public static int DefaultPlayerCount => 2;
        static string GameEngineName => "IGameEngine`2";
        static string ScoreboardName => "IScoreBoard`1";

        static public void TryRun(string enginePath, string scoreBoardPath,
            IEnumerable<string> contestantsPaths, int playerCount)
        {
            Type engineType = GetAncestorOfInterface(GameEngineName, enginePath);

            Type interf = engineType.GetInterface(GameEngineName);
            Type contestType = interf.GenericTypeArguments[0];
            Type scoreType = interf.GenericTypeArguments[1];

            Type scoreboardType = GetAncestorOfInterface(ScoreboardName, scoreBoardPath);

            if (scoreboardType.GetInterface(ScoreboardName).GenericTypeArguments[0] != scoreType)
            {
                throw new ScoreOfScoreBoardAndEngineNotEqualException(scoreboardType, engineType);
            }

            Type builderPretype = typeof(TurnamentorBuilder<,,>);

            Type builderType = builderPretype.MakeGenericType(contestType, scoreType, scoreboardType);

            dynamic builder = Activator.CreateInstance(builderType);

            builderType.GetMethod("SetGameEngine", new Type[0]).MakeGenericMethod(engineType)
                .Invoke(builder, new object[0]);

            builder
                .AddContestants(ClassSource.Both, contestantsPaths.ToArray())
                .SetNumberOfContestantsInRound(playerCount);

            dynamic turnamentor = builder.BuildTurnamentor();
            dynamic scoreBoard = turnamentor.RunTurnament();
        }

        static Type GetAncestorOfInterface(string interfaceName, string assemblyPath)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyPath);

            IEnumerable<Type> derives = assembly.GetTypes().Where(t => t.GetInterface(interfaceName) != null);
            Type final = null;
            foreach (var t in derives)
            {
                if (!t.IsAbstract && !t.IsInterface)
                {
                    final = t;
                    break;
                }
            }
            return final;
        }
    }

    public class ScoreOfScoreBoardAndEngineNotEqualException : Exception
    {
        public ScoreOfScoreBoardAndEngineNotEqualException(Type scoreboard, Type engine)
            :base("Types " + scoreboard + " and " + engine + " has different score type."){}
    }
}

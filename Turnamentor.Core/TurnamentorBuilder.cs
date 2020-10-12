using System;
using System.Collections.Generic;
using System.Linq;
using Turnamentor.Core.Loading;
using Turnamentor.Core.RoundSelecting;
using Turnamentor.Interfaces;

namespace Turnamentor.Core
{
    public class TurnamentorBuilder<Contestant, Score, ScoreBoard> : ITurnamentorBuilder<Contestant, Score, ScoreBoard> where ScoreBoard : IScoreBoard<Score>
    {
        public TurnamentorBuilder()
        {
            SetDefaults();
        }

        private void SetDefaults()
        {
            turnamentor = new Turnamentor<ScoreBoard, Score>();
            turnamentor.SetContstantType<Contestant>();
            if(typeof(ScoreBoard).GetConstructor(Type.EmptyTypes) != null)
                turnamentor.ScoreBoardInstance = (ScoreBoard)Activator.CreateInstance(typeof(ScoreBoard));

            roundSelectorConfig = new RoundSelectorConfig
            {
                NumberOfContestantsInRound = 2,
                Contestants = new List<Type>()
            };
            roundSelectorProvider = config => new AllWithAllSelector<Contestant>(config);
        }

        private Turnamentor<ScoreBoard, Score> turnamentor;
        private RoundSelectorConfig roundSelectorConfig;
        private Func<RoundSelectorConfig, IRoundSelector<Contestant>> roundSelectorProvider;

        public ITurnamentorBuilder<Contestant, Score, ScoreBoard> AddContestant<ContestantImplementation>() where ContestantImplementation : Contestant, new()
        {
            if(!roundSelectorConfig.Contestants.Contains(typeof(ContestantImplementation)))
                roundSelectorConfig.Contestants.Add(typeof(ContestantImplementation));
            
            return this;
        }

        public ITurnamentorBuilder<Contestant, Score, ScoreBoard> AddContestants(ClassSource source, params string[] directories)
        {
            ITypeLoader loader = GetTypeLoader(source);

            if (directories.Length == 0)
                directories = new string[] { "" }; //add at least work directory

            var loadedTypes = loader.LoadTypes<Contestant>(directories);
            roundSelectorConfig.Contestants.AddRange(
                loadedTypes.Where(type => !roundSelectorConfig.Contestants.Contains(type))
            );

            return this;
        }

        public ITurnamentorBuilder<Contestant, Score, ScoreBoard> SetGameEngine<GameEngine>() where GameEngine : IGameEngine<Contestant, Score>, new()
        {
            turnamentor.EngineProvider = () => Activator.CreateInstance(typeof(GameEngine));
            return this;
        }

        public ITurnamentorBuilder<Contestant, Score, ScoreBoard> SetGameEngine<GameEngine>(GameEngine gameEngine) where GameEngine : IGameEngine<Contestant, Score>
        {
            turnamentor.EngineProvider = () => gameEngine;
            return this;
        }

        public ITurnamentorBuilder<Contestant, Score, ScoreBoard> SetGameEngine<GameEngine>(Func<GameEngine> gameEngineProvider) where GameEngine : IGameEngine<Contestant, Score>
        {
            turnamentor.EngineProvider = () => gameEngineProvider();
            return this;
        }

        public ITurnamentorBuilder<Contestant, Score, ScoreBoard> SetGameEngine(ClassSource source, string directoryWithFile)
        {
            ITypeLoader loader = GetTypeLoader(source);
            var types = loader.LoadTypes<IGameEngine<Contestant, Score>>(new string[] { directoryWithFile });

            if (types.Count == 0)
                throw new NoInstanceOfGameEngineFoundException();

            if (types.Count > 1)
                throw new AmbiguousGameEngine();

            turnamentor.EngineProvider = () => Activator.CreateInstance(types[0]);
            return this;
        }

        public ITurnamentorBuilder<Contestant, Score, ScoreBoard> SetNumberOfContestantsInRound(int count)
        {
            roundSelectorConfig.NumberOfContestantsInRound = count;
            return this;
        }

        public ITurnamentorBuilder<Contestant, Score, ScoreBoard> SetScoreBoard(ScoreBoard scoreBoard)
        {
            turnamentor.ScoreBoardInstance = scoreBoard;
            return this;
        }

        public ITurnamentor<ScoreBoard, Score> BuildTurnamentor()
        {
            if (turnamentor.ScoreBoardInstance == null)
                throw new NoEmptyConstructorFoundException();

            turnamentor.AllRounds = 
                roundSelectorProvider(roundSelectorConfig)
                .GetAllRounds()
                .Select(round => round.Cast<dynamic>().ToList());

            var t = turnamentor;
            SetDefaults();
            return t;
        }

        private ITypeLoader GetTypeLoader(ClassSource source)
            => source switch
            {
                ClassSource.Assemblies  => new AssemblyTypeLoader(),
                ClassSource.SourceFiles  => new SourceCodeTypeLoader(),
                ClassSource.Both        => new BothTypeLoader(),
                
                _ => throw new NotImplementedException()
            };
    }
}

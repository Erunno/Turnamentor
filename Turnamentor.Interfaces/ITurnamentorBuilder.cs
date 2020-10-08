using System;
using System.Collections.Generic;
using System.Text;
using Turnamentor.Interfaces;

namespace Turnamentor.Interfaces
{
    public enum ClassSource { Assemblies, SourceFile, Both }

    public interface ITurnamentorBuilder<Contestant, Score, ScoreBoard> where ScoreBoard : IScoreBoard<Score>
    {
        /// <summary>
        /// Builds instance of ITurnamentor based on given configuration.
        /// </summary>
        /// <exception cref="NoEmptyConstructorFoundException">In case that score board is not set explicitely and given type of score board does not implement empty constructor.</exception>
        ITurnamentor<ScoreBoard, Score> BuildTurnamentor();

        /// <summary>
        /// Sets GameEngine. Given GameEngine has to implement emty constructor otherwise it throws exception.
        /// </summary>
        /// <typeparam name="GameEngine">Used GameEngine</typeparam>
        ITurnamentorBuilder<Contestant, Score, ScoreBoard> SetGameEngine<GameEngine>() 
            where GameEngine : IGameEngine<Contestant, Score>, new();

        /// <summary>
        /// Sets GameEngine. Bare in mind that turnament may run parallelly. 
        /// </summary>
        /// <param name="gameEngine">Is used in every thread.</param>
        ITurnamentorBuilder<Contestant, Score, ScoreBoard> SetGameEngine<GameEngine>(GameEngine gameEngine)
            where GameEngine : IGameEngine<Contestant, Score>;

        /// <summary>
        /// Sets GameEngine using given provider. Bare in mind that turnament may run parallelly. 
        /// </summary>
        /// <param name="gameEngineProvider">Is used to create instance of GameEngine for every thread.</param>
        ITurnamentorBuilder<Contestant, Score, ScoreBoard> SetGameEngine<GameEngine>(Func<GameEngine> gameEngineProvider)
            where GameEngine : IGameEngine<Contestant, Score>;

        /// <summary>
        /// Finds implementation of GameEngine in given directory. 
        /// All assemblies (resp. source files) in given directory (and child directories) are loaded (resp. compiled and loaded)
        /// </summary>
        /// <param name="source">Type of files which will be searched.</param>
        /// <param name="directoryWithFile">This directory and child directories are searched.</param>
        /// <exception cref="NoEmptyConstructorFoundException">GameEngine does not implement emty constructor.</exception>
        /// <exception cref="NoInstanceOfGameEngineFoundException">GameEngine does not exists in given files.</exception>
        ITurnamentorBuilder<Contestant, Score, ScoreBoard> SetGameEngine(ClassSource source, string directoryWithFile);

        /// <summary>
        /// Adds contestant type to the game.
        /// </summary>
        ITurnamentorBuilder<Contestant, Score, ScoreBoard> AddContestant<ContestantImplementation>()
            where ContestantImplementation : Contestant, new();

        /// <summary>
        /// Adds constestants to the game.
        /// All assemblies (resp. source files) in given directory (and child directories) are loaded (resp. compiled and loaded)
        /// </summary>
        /// <param name="source">Type of files which will be searched.</param>
        /// <param name="pathsToDirectories">This directory and child directories are searched.</param>
        /// <exception cref="NoContestantFoundException">No contestats found in give directories.</exception>
        /// <exception cref="NoEmptyConstructorFoundException">One or more contestant does not implement emty constructor.</exception>
        ITurnamentorBuilder<Contestant, Score, ScoreBoard> AddContestants(ClassSource source, params string[] pathsToDirectories);

        /// <summary>
        /// Number of constestant which will compete in one round.
        /// </summary>
        ITurnamentorBuilder<Contestant, Score, ScoreBoard> SetNumberOfContestantsInRound(int count);

        /// <summary>
        /// Set used score board. This score board will be return by built Turnamentor. 
        /// </summary>
        /// <param name="scoreBoard">Used score board.</param>
        ITurnamentorBuilder<Contestant, Score, ScoreBoard> SetScoreBoard(ScoreBoard scoreBoard);
    }
}

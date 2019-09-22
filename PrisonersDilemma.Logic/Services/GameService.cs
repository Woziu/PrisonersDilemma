using PrisonersDilemma.Core.Enums;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Settings;
using System;
using System.Collections.Generic;

namespace PrisonersDilemma.Logic.Services
{
    public class GameService : IGameService
    {
        private readonly IStrategyService _strategyService;
        private readonly GameSettings _gameSettings;
        public GameService(IStrategyService strategyService, IGameSettingsProvider gameSettingsProvider)
        {
            _strategyService = strategyService;
            _gameSettings = gameSettingsProvider.GetGameSettings();
        }
        public Game Play(Player firstPlayer, Player secondPlayer)
        {
            string playerId1 = firstPlayer.Id;
            string playerId2 = secondPlayer.Id;
            var rounds = new List<Round>();
            for (int i = 1; i <= _gameSettings.TotalRounds; i++)
            {
                //make moves                
                PlayerMove firstPlayerMove = _strategyService.GetNextMove(firstPlayer, rounds);
                PlayerMove secondPlayerMove = _strategyService.GetNextMove(secondPlayer, rounds);
                //add moves to history
                rounds.Add(GetRound(i, firstPlayerMove, secondPlayerMove));
            }
            var game = new Game
            {
                Id = Guid.NewGuid().ToString(),
                FirstPlayer = firstPlayer,
                SecondPlayer = secondPlayer,
                Rounds = rounds
            };

            return game;
        }
        public Round GetRound(int roundNumer, PlayerMove firstPlayerMove, PlayerMove secondPlayerMove)
        {
            try
            {
                var round = new Round
                {
                    Id = roundNumer,
                    FirstPlayerScore = 0,
                    SecondPlayerScore = 0,
                    PlayersMoves = new List<PlayerMove>() { firstPlayerMove, secondPlayerMove }
                };


                if (firstPlayerMove.Type == secondPlayerMove.Type)
                {
                    MoveType move = firstPlayerMove.Type;
                    int score = move == MoveType.Cooperate ? 3 : 1;
                    round.FirstPlayerScore += score;
                    round.SecondPlayerScore += score;
                }
                else
                {
                    if (firstPlayerMove.Type == MoveType.Cooperate)
                    {
                        round.SecondPlayerScore += 5;
                    }
                    if (secondPlayerMove.Type == MoveType.Cooperate)
                    {
                        round.FirstPlayerScore += 5;
                    }
                }

                return round;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PrisonersDilemma.Core.Enums;
using PrisonersDilemma.Core.Helpers;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;
using PrisonersDilemma.Core.Settings;

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
                    FirstPlayerScore = _gameSettings.MoveModifier,
                    SecondPlayerScore = _gameSettings.MoveModifier,
                    PlayersMoves = new List<PlayerMove>() { firstPlayerMove, secondPlayerMove }
                };

                if (firstPlayerMove.Type == MoveType.Cooperate)
                {
                    round.SecondPlayerScore += _gameSettings.CooperateModifier;
                }
                if (secondPlayerMove.Type == MoveType.Cooperate)
                {
                    round.FirstPlayerScore += _gameSettings.CooperateModifier;
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

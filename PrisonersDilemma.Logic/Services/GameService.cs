﻿using System;
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
        private readonly IGameRepository _gameRepository;
        private readonly IStrategyService _strategyService;
        private readonly GameSettings _gameSettings;
        public GameService(IGameRepository gameRepository, IStrategyService strategyService, IGameSettingsProvider gameSettingsProvider)
        {
            _gameRepository = gameRepository;
            _strategyService = strategyService;
            _gameSettings = gameSettingsProvider.GetGameSettings();
        }
        public async Task<Game> PlayAsync(Player firstPlayer, Player secondPlayer, int totalRounds)
        {            
            var rounds = new List<Round>();
            for (int i = 0; i < totalRounds; i++)
            {
                //make moves                
                PlayerMove firstPlayerMove = await _strategyService.GetNextMoveAsync(firstPlayer, rounds);
                PlayerMove secondPlayerMove = await _strategyService.GetNextMoveAsync(secondPlayer, rounds);
                //add moves to history
                rounds.Add(GetRound(firstPlayerMove, secondPlayerMove));                
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

        public Round GetRound(PlayerMove firstPlayerMove, PlayerMove secondPlayerMove)
        {
            var round = new Round();
            round.PlayersMoves = new List<PlayerMove>
            {
                firstPlayerMove,
                secondPlayerMove
            };

            round.FirstPlayerScore += _gameSettings.MoveModifier;
            round.SecondPlayerScore += _gameSettings.MoveModifier;

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

        public Task SaveGameAsync(string populationId)
        {
            throw new NotImplementedException();
        }
    }
}

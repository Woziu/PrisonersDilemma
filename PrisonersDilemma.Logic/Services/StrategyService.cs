using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrisonersDilemma.Core.Enums;
using PrisonersDilemma.Core.Models;
using PrisonersDilemma.Core.Repositories;

namespace PrisonersDilemma.Logic.Services
{
    public class StrategyService : IStrategyService
    {
        private readonly IStrategyRepository _strategyRepository;
        public StrategyService(IStrategyRepository strategyRepository)
        {
            _strategyRepository = strategyRepository;
        }
        public Task<PlayerMove> GetNextMoveAsync(Player player, List<Round> roundsHistory)
        {
            int maxPriority = 100;
            int topPriority = -1;
            MoveType selectedMove = MoveType.Undefined;

            var rounds = roundsHistory.OrderByDescending(r => r.Id).ToList();

            var moves = player.Strategy.Moves
                .Where(c => c.TotalDepth <= roundsHistory.Count)
                .OrderByDescending(d => d.TotalDepth);//moves with highest depth have highest prioryty

            foreach(Move move in moves)
            {
                if (move.Priority > maxPriority) move.Priority = maxPriority;
                if (move.Priority * move.TotalDepth > topPriority && MoveConditionsMet(player.Id, move, rounds))
                {
                    selectedMove = move.MoveType;
                    topPriority = move.Priority * move.TotalDepth;//higher depth higher priority
                }
            }
            if (selectedMove == MoveType.Undefined)
            {
                //TODO: throw or log?
                selectedMove = MoveType.Cooperate;
            }
            return Task.FromResult(new PlayerMove() { PlayerId = player.Id, Type = selectedMove });            
        }

        private bool MoveConditionsMet(string thisPlayerId, Move move, List<Round> roundsHistory)//conditions met or completed?
        {
            if (move.Conditions == null) return true;

            bool ok = true;
            
            foreach (Condition condition in move.Conditions)
            {
                if (condition.Depth > roundsHistory.Count)
                {
                    //check if self condition defined and ok
                    if (condition.PlayerMove != MoveType.Undefined)
                    {
                        MoveType playerMove = roundsHistory[condition.Depth].PlayersMoves
                            .Where(p => p.PlayerId == thisPlayerId).FirstOrDefault().Type;
                        if (condition.PlayerMove != playerMove)
                        {
                            ok = false;
                        }
                    }
                    //check if enemy condition defined and ok
                    if (condition.EnemyMove != MoveType.Undefined)
                    {
                        MoveType enemyMove = roundsHistory[condition.Depth].PlayersMoves
                            .Where(p => p.PlayerId != thisPlayerId).FirstOrDefault().Type;
                        if (condition.EnemyMove != enemyMove)
                        {
                            ok = false;
                        }
                    }
                }                
            }
            return ok;
        }
    }
}

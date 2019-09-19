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
            //TODO: problably can be sync
            int maxPriority = 100;
            int topPriority = -1;
            MoveType selectedMove = MoveType.Undefined;

            try
            {
                var rounds = roundsHistory.OrderByDescending(r => r.Id).ToList();

                var moves = player.Strategy.Moves
                    .Where(c => c.TotalDepth <= roundsHistory.Count)
                    .OrderByDescending(d => d.TotalDepth);//moves with highest depth have highest prioryty

                foreach (Move move in moves)
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
            }
            catch (Exception ex)
            {

            }
            return Task.FromResult(new PlayerMove() { PlayerId = player.Id, Type = selectedMove });            
        }
        private bool MoveConditionsMet(string thisPlayerId, Move move, List<Round> roundsHistory)//conditions met or completed?/TODO: change to fulfilled?
        {
            //TODO: this sould be tested
            if (move.Conditions == null) return true;

            bool ok = true;

            foreach (Condition condition in move.Conditions)
            {
                try
                {
                    if (roundsHistory.Count > condition.Depth)
                    {
                        //check if self condition defined and ok
                        if (condition.PlayerMove != MoveType.Undefined)
                        {
                            MoveType playerMove = roundsHistory[condition.Depth - 1].PlayersMoves
                                .Where(p => p.PlayerId == thisPlayerId).FirstOrDefault().Type;
                            if (condition.PlayerMove != playerMove)
                            {
                                if (move.ConditionsOperator == ConditionOperator.AND) return false;
                                ok = false;
                            }
                        }
                        //check if enemy condition defined and ok
                        if (condition.EnemyMove != MoveType.Undefined)
                        {
                            MoveType enemyMove = roundsHistory[condition.Depth - 1].PlayersMoves
                                .Where(p => p.PlayerId != thisPlayerId).FirstOrDefault().Type;
                            if (condition.EnemyMove != enemyMove)
                            {
                                if (move.ConditionsOperator == ConditionOperator.AND) return false;
                                ok = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }             
            }
            return ok;
        }

        public List<Strategy> GetStrategiesById(List<string> idList)
        {
            List<Strategy> strategies = _strategyRepository.Get(idList);
            //check if strategie are complete
            if (strategies != null && strategies.Any())
            {
                for (int i = 0; i < strategies.Count; i++)
                {
                    if (strategies[i].Moves != null && strategies[i].Moves.Any())
                    {
                        for (int j = 0; j < strategies[i].Moves.Count; j++)
                        {
                            //calculate total depth and priority if none
                            if (strategies[i].Moves[j].TotalDepth == 0 && strategies[i].Moves[j].Conditions != null)
                            {
                                strategies[i].Moves[j].TotalDepth = strategies[i].Moves[j].Conditions.Max(c => c.Depth);
                            }
                            if (strategies[i].Moves[j].Priority == 0)
                            {
                                strategies[i].Moves[j].Priority = strategies[i].Moves[j].TotalDepth;
                            }
                        }
                    }                    
                }
            }
            return strategies;
        }

        public async Task<List<Strategy>> GetAllStrategies() =>
             await _strategyRepository.GetAll();   
        
        public async Task<Strategy> GetStrategyByName(string name)
        {
            List<Strategy> allStrategies = await _strategyRepository.GetAll();
            return allStrategies.Where(s => s.Name == name).FirstOrDefault();
        }
    }
}

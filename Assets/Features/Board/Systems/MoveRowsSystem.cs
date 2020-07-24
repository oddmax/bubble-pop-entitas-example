using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Features.Board.Systems
{
    public class MoveRowsSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts contexts;
        private IGroup<GameEntity> bubbles;
        private IGameConfig gameConfig;

        public MoveRowsSystem(Contexts contexts) : base(contexts.game)
        {
            this.contexts = contexts;
            gameConfig = contexts.config.gameConfig.value;
            bubbles = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Position, GameMatcher.BubbleNumber));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.MoveComplete
            );
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isMoveComplete;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var currentAmountOfRows = GetCurrentAmountOfRows();
            
            var rowsToAdd = CalculateHowManyRowsToAdd(currentAmountOfRows);
            if (rowsToAdd != 0)
            {
                CreateNewRows(rowsToAdd);
                MoveRows(rowsToAdd);
                var isFirstRowShifted = contexts.game.board.isFirstRowShifted;
                var isAmountOfAddedRowsEven = rowsToAdd % 2 == 0;
                if (!isAmountOfAddedRowsEven)
                    contexts.game.board.isFirstRowShifted = !isFirstRowShifted;
            }
            
            contexts.game.isMoveComplete = false;
            contexts.game.CreateLauncherBubble();
        }

        private void CreateNewRows(int rowsToAdd)
        {
            for (int i = 0; i < rowsToAdd; i++)
                CreateRow(-1 - i);
        }

        private void CreateRow(int row)
        {
            BoardLogic.GetColumnStartEnd(row, contexts.game.board.isFirstRowShifted, gameConfig.boardSize.x, out var columnStart, out var columnEnd);
            for (int x = columnStart; x < columnEnd; x+=2)
            {
                var bubble = contexts.game.CreateRandomBoardBubble(x, row);
                bubble.AddStartPosition(new Vector2Int(x, row));
            }
        }

        private void MoveRows(int rowsToAdd)
        {
            Vector2Int position;
            var bubblesToRemove = new List<GameEntity>();
            foreach (var bubble in bubbles)
            {
                position = bubble.position.value;
                position.y += rowsToAdd;
                bubble.ReplacePosition(position);
                if (position.y < 0)
                {
                    bubblesToRemove.Add(bubble);
                    bubble.AddMergeTarget(position);
                }
            }
            
            foreach (var bubbleToRemove in bubblesToRemove)
            {
                bubbleToRemove.RemovePosition();
            }
        }

        private int CalculateHowManyRowsToAdd(int currentAmountOfRows)
        {
            int rowsToAdd = 0;
            if (currentAmountOfRows < gameConfig.MinAmountOfRows)
                rowsToAdd = gameConfig.MinAmountOfRows - currentAmountOfRows;
            else if (currentAmountOfRows < gameConfig.MediumAmountOfRows)
                rowsToAdd = 1;
            else if (currentAmountOfRows >= gameConfig.MaxAmountOfRows)
                rowsToAdd = gameConfig.MaxAmountOfRows - currentAmountOfRows;
            return rowsToAdd;
        }

        private int GetCurrentAmountOfRows()
        {
            var rowsAmount = 0;
            foreach (var bubble in bubbles)
            {
                if (bubble.position.value.y > rowsAmount)
                    rowsAmount = bubble.position.value.y;
            }

            return rowsAmount + 1;
        }
    }
}
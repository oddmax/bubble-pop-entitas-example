using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Features.Board.Systems
{
    public class DestroyHangingClustersSystem : ReactiveSystem<GameEntity>
    {
        readonly Contexts contexts;
        private Vector2Int boardSize;
        private BoardComponent board;
        private IGroup<GameEntity> bubbles;

        public DestroyHangingClustersSystem(Contexts contexts) : base(contexts.game)
        {
            this.contexts = contexts;
            bubbles = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Position, GameMatcher.BubbleNumber));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.BoardCleaningHangingClusters);

        protected override bool Filter(GameEntity entity) => entity.isBoardCleaningHangingClusters;

        protected override void Execute(List<GameEntity> entities)
        {
            board = contexts.game.board;
            boardSize = contexts.config.gameConfig.value.boardSize;
            List<GameEntity> bubblesToCheck = new List<GameEntity>();
            MarkTopRowAsConnectedToTop(bubblesToCheck);
            MarkAllBubblesConnectedToTop(bubblesToCheck);
            DeleteHangingClusters();

            contexts.game.isBoardCleaningHangingClusters = false;
        }

        private void DeleteHangingClusters()
        {
            var bubblesToRemove = new List<GameEntity>();
            foreach (var bubble in bubbles)
            {
                if (bubble.isConnectedToTop)
                    bubble.isConnectedToTop = false;
                else
                    bubblesToRemove.Add(bubble);
            }

            foreach (var bubbleToRemove in bubblesToRemove)
            {
                bubbleToRemove.isFalling = true;
                bubbleToRemove.RemovePosition();
            }
        }

        private void MarkTopRowAsConnectedToTop(List<GameEntity> bubblesToCheck)
        {
            BoardLogic.GetColumnStartEnd(0, board.isFirstRowShifted, boardSize.x, out var columnStart, out var columnEnd);

            for (int x = columnStart; x < columnEnd; x += 2)
            {
                var bubble = contexts.game.GetBubbleWithPosition(new Vector2Int(x, 0));
                if(bubble == null)
                    continue;
                bubble.isConnectedToTop = true;
                bubblesToCheck.Add(bubble);
            }
        }

        private void MarkAllBubblesConnectedToTop(List<GameEntity> bubblesToCheck)
        {
            while (bubblesToCheck.Count > 0)
            {
                List<GameEntity> nextBubblesToCheck = new List<GameEntity>();
                foreach (var bubbleToCheck in bubblesToCheck)
                {
                    foreach (var bubbleNeighboursOffset in BoardLogic.BubbleNeighboursOffsets)
                    {
                        var neighbour =
                            contexts.game.GetBubbleWithPosition(bubbleToCheck.position.value + bubbleNeighboursOffset);
                        
                        if (neighbour == null || neighbour.isConnectedToTop)
                            continue;

                        neighbour.isConnectedToTop = true;
                        nextBubblesToCheck.Add(neighbour);
                    }
                }

                bubblesToCheck = nextBubblesToCheck;
            }
        }
    }
}
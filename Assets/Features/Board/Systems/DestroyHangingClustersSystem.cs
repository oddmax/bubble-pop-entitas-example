using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Features.Board.Systems
{
    public class DestroyHangingClustersSystem : ReactiveSystem<GameEntity>
    {
        readonly Contexts _contexts;
        private Vector2Int _boardSize;
        private BoardComponent _board;
        private IGroup<GameEntity> _bubbles;

        public DestroyHangingClustersSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
            _bubbles = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Position, GameMatcher.BubbleNumber));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.BoardCleaningHangingClusters);

        protected override bool Filter(GameEntity entity) => entity.isBoardCleaningHangingClusters;

        protected override void Execute(List<GameEntity> entities)
        {
            _board = _contexts.game.board;
            _boardSize = _contexts.config.gameConfig.value.boardSize;
            List<GameEntity> bubblesToCheck = new List<GameEntity>();
            MarkTopRowAsConnectedToTop(bubblesToCheck);
            MarkAllBubblesConnectedToTop(bubblesToCheck);
            DeleteHangingClusters();

            _contexts.game.isBoardCleaningHangingClusters = false;
        }

        private void DeleteHangingClusters()
        {
            var bubblesToRemove = new List<GameEntity>();
            foreach (var bubble in _bubbles)
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
            BoardLogic.GetColumnStartEnd(0, _board.isFirstRowShifted, _boardSize.x, out var columnStart, out var columnEnd);

            for (int x = columnStart; x < columnEnd; x += 2)
            {
                var bubble = _contexts.game.GetBubbleWithPosition(new Vector2Int(x, 0));
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
                            _contexts.game.GetBubbleWithPosition(bubbleToCheck.position.value + bubbleNeighboursOffset);
                        
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
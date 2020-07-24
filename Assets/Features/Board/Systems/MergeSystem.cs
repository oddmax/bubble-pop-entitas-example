using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Entitas;
using JetBrains.Annotations;
using UnityEngine;

namespace Features.Board.Systems
{
    public class MergeSystem: ReactiveSystem<GameEntity>
    {
        readonly Contexts contexts;

        public MergeSystem(Contexts contexts) : base(contexts.game)
        {
            this.contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.NewBubble);

        protected override bool Filter(GameEntity entity) => entity.isNewBubble && entity.isBubble && !entity.isDestroyed;

        protected override void Execute(List<GameEntity> entities)
        {
            var newBubble = entities.SingleEntity();
            newBubble.isNewBubble = false;

            List<GameEntity> neighboursWithSameNumber = new List<GameEntity>(); 
            neighboursWithSameNumber.Add(newBubble);
            newBubble.isMerge = true;

            var bubbleNumber = newBubble.bubbleNumber.Number;
            CollectNeighboursWithSameNumber(newBubble, bubbleNumber, neighboursWithSameNumber);

            if (neighboursWithSameNumber.Count > 1)
            {
                var mergeNumber = BoardLogic.GetMergeNumber(bubbleNumber, neighboursWithSameNumber.Count);
                var mergeBubble = GetMergeBubble(neighboursWithSameNumber, mergeNumber);
                var mergeBubblePosition = mergeBubble.position.value;
                MergeBubbles(neighboursWithSameNumber, mergeNumber, mergeBubblePosition);
                
                if (mergeNumber > 2048)
                    ExplodeAtPosition(mergeBubblePosition);
                else
                    contexts.game.CreateBoardBubble(mergeNumber, mergeBubblePosition.x, mergeBubblePosition.y);

                var mergeTime = contexts.config.gameConfig.value.MergeTimeInSeconds;
                var timerEntity = contexts.timer.SetMergeProgress(mergeBubblePosition, mergeNumber, mergeTime);
                timerEntity.AddTimer(mergeTime);
                timerEntity.isTimerRunning = true;
                contexts.game.isBoardCleaningHangingClusters = true;
            }
            else
            {
                newBubble.isMerge = false;
                contexts.game.isMoveComplete = true;
            }
        }

        private void ExplodeAtPosition(Vector2Int mergeBubblePosition)
        {
            var bubbleNeighbours = contexts.game.GetBubbleNeighbours(mergeBubblePosition);
            foreach (var bubble in bubbleNeighbours)
            {
                bubble.RemovePosition();
                bubble.isFalling = true;
            }
            
            if(contexts.game.hasCameraShake == false)
                contexts.game.SetCameraShake(0);
            else
                contexts.game.ReplaceCameraShake(contexts.game.cameraShake.amountOfShakes++);
        }

        private void MergeBubbles(List<GameEntity> mergeBubbles, int mergeNumber, Vector2Int mergeBubblePosition)
        {
            foreach (var bubble in mergeBubbles)
            {
                bubble.isMerge = false;
                bubble.RemovePosition();
                bubble.AddMergeTarget(mergeBubblePosition);
            }
        }

        [CanBeNull]
        private GameEntity GetMergeBubble(List<GameEntity> mergeBubbles, int mergeNumber)
        {
            GameEntity mergeEntity = null;
            var willMergeCreateNextMerge = false;
            
            foreach (var bubble in mergeBubbles)
            {
                if (mergeEntity == null)
                    mergeEntity = bubble;

                var neighbours = contexts.game.GetBubbleNeighbours(bubble.position.value);
                var willHaveNextMerge = false;
                foreach (var bubbleNeighbours in neighbours)
                {
                    if (bubbleNeighbours.bubbleNumber.Number == mergeNumber)
                    {
                        willHaveNextMerge = true;
                        break;
                    }
                }

                if (willHaveNextMerge && !willMergeCreateNextMerge)
                {
                    willMergeCreateNextMerge = true;
                    mergeEntity = bubble;
                }
                else if(!willHaveNextMerge && willMergeCreateNextMerge)
                {
                    continue;
                }
                else if (bubble.position.value.y < mergeEntity.position.value.y
                || bubble.position.value.y == mergeEntity.position.value.y && bubble.position.value.x < mergeEntity.position.value.x)
                {
                    mergeEntity = bubble;
                }
            }

            return mergeEntity;
        }

        private void CollectNeighboursWithSameNumber(GameEntity bubble,
            int bubbleNumber,
            List<GameEntity> neighboursWithSameNumber)
        {
            var neighbours = contexts.game.GetBubbleNeighbours(bubble.position.value);
            foreach (var neighbour in neighbours)
            {
                if (neighbour.bubbleNumber.Number == bubbleNumber && neighboursWithSameNumber.Contains(neighbour) == false)
                {
                    neighboursWithSameNumber.Add(neighbour);
                    neighbour.isMerge = true;
                    CollectNeighboursWithSameNumber(neighbour, bubbleNumber, neighboursWithSameNumber);
                }
            }
        }
    }
}
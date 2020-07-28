using Entitas;
using UnityEngine;

namespace Features.Launcher
{
    public class FlyingBubblePositionUpdateSystem : IExecuteSystem
    {
        private readonly Contexts contexts;
        private readonly IGroup<GameEntity> flyingBubbleGroup;
        private GameEntity  flyingBubble;
        private IGroup<GameEntity> previewGroup;

        public FlyingBubblePositionUpdateSystem(Contexts contexts)
        {
            this.contexts = contexts; 
            flyingBubbleGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.FlyingBubble, GameMatcher.BubbleNumber, GameMatcher.TransformPosition));
            previewGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.BubblePreview));
        }

        public void Execute()
        {
            flyingBubble = flyingBubbleGroup.GetSingleEntity();
            if (flyingBubble != null && flyingBubble.isFlyingBubble)
            {
                var trajectoryComponent = contexts.game.launcherEntity.launcherTrajectory;
                var points = trajectoryComponent.actualTrajectoryPointsArray;
                var timeSinceLastFrame = Time.deltaTime;
                var traveledDistance = contexts.config.gameConfig.value.MovementSpeed * timeSinceLastFrame;
                var progress = flyingBubble.currentTrajectoryIndex.progress;
                var startPoint = points[flyingBubble.currentTrajectoryIndex.index];
                var endPosition = points[flyingBubble.currentTrajectoryIndex.index + 1];
                
                var totalDistance = (endPosition - startPoint).magnitude;
                var progressChange = traveledDistance/totalDistance;
                progress += progressChange;
                if (progress >= 1)
                {
                    progress = 1;
                    flyingBubble.ReplaceCurrentTrajectoryIndex(flyingBubble.currentTrajectoryIndex.index + 1, 0);
                    flyingBubble.ReplaceTransformPosition(endPosition);
                    if (flyingBubble.currentTrajectoryIndex.index == trajectoryComponent.actualTrajectoryPointsArray.Length - 1)
                    {
                        //bubble has arrived
                        flyingBubble.isFlyingBubble = false;
                        flyingBubble.RemoveCurrentTrajectoryIndex();
                        flyingBubble.RemoveTransformPosition();
                        flyingBubble.AddPosition(previewGroup.GetSingleEntity().position.value);
                        flyingBubble.isNewBubble = true;
                        return;
                    }
                }
                else
                {
                    flyingBubble.ReplaceCurrentTrajectoryIndex(flyingBubble.currentTrajectoryIndex.index, progress);
                    var newPosition = Vector2.Lerp(startPoint, endPosition, progress);
                    flyingBubble.ReplaceTransformPosition(newPosition);
                }
            }
        }
    }
}
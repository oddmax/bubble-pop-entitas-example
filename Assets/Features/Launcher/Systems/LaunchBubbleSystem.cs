using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using Features.Pieces;
using UnityEngine;

namespace Features.Launcher
{
    public class LaunchBubbleSystem : ReactiveSystem<InputEntity>
    {
        readonly Contexts contexts;
        private readonly IGroup<GameEntity> launcher;
        private readonly IGroup<GameEntity> preview;
        private readonly IGroup<GameEntity> bubbleToLaunch;
        private GameEntity  flyingBubble;
        private const int maxStepDistance = 200;

        public LaunchBubbleSystem(Contexts contexts) : base(contexts.input)
        {
            this.contexts = contexts;
            launcher = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Launcher));
            preview = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.BubblePreview));
            bubbleToLaunch = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LaunchBubble, GameMatcher.BubbleNumber, GameMatcher.TransformPosition));
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
            => context.CreateCollector(InputMatcher.AllOf(InputMatcher.Input, InputMatcher.MouseUp));

        protected override bool Filter(InputEntity entity) => entity.isInput && entity.hasMouseUp;

        protected override void Execute(List<InputEntity> entities)
        {
            var inputEntity = entities.SingleEntity();
            var input = inputEntity.isInput;
        
            var launcherEntity = launcher.GetSingleEntity();
            var previewEntity = preview.GetSingleEntity();

            flyingBubble = bubbleToLaunch.GetSingleEntity();

            if (flyingBubble != null && flyingBubble.isLaunchBubble)
            {
                launcherEntity.ReplaceVisible(false);
                previewEntity.ReplaceVisible(false);

                flyingBubble.isLaunchBubble = false;
                flyingBubble.isFlyingBubble = true;
                
                Vector2 endPosition = CoordinatesConverter.Convert(previewEntity.position.value, contexts.config.gameConfig.value.BubbleSize);
                endPosition = GameObject.FindWithTag("Board").transform.TransformPoint(endPosition);

                var newBubbleCoor = previewEntity.position.value;
                var newBubbleNumber = flyingBubble.bubbleNumber.Number;
                var launcherTrajectory = launcherEntity.launcherTrajectory;
                float distance;
                Tween tween = null;
                
                if (launcherTrajectory.isReflected == false)
                {
                    distance = (endPosition - flyingBubble.transformPosition.value).magnitude;
                    tween = DOTween.To(() => flyingBubbleTransformPosition,
                        x => flyingBubbleTransformPosition = x,
                        endPosition,
                        CalculateTime(distance)).SetEase(Ease.Linear);
                    
                    tween?.OnComplete(() => { CreateNewBubbleOnTheBoard(newBubbleCoor); });
                }
                else
                {
                    var distanceTillReflection =
                        (launcherTrajectory.ReflectionPoint - flyingBubble.transformPosition.value).magnitude; 
                    Tween reflectionTween = DOTween.To(() => flyingBubbleTransformPosition,
                        x => flyingBubbleTransformPosition = x,
                        launcherTrajectory.ReflectionPoint,
                        CalculateTime(distanceTillReflection)).SetEase(Ease.Linear);

                    distance = (endPosition - launcherTrajectory.ReflectionPoint).magnitude;

                    
                    reflectionTween.OnComplete(() =>
                    {
                        tween = DOTween.To(() => flyingBubbleTransformPosition,
                            x => flyingBubbleTransformPosition = x,
                            endPosition,
                            CalculateTime(distance)).SetEase(Ease.Linear);
                        
                        tween?.OnComplete(() => { CreateNewBubbleOnTheBoard(newBubbleCoor); });
                    });
                    
                }
            }
        }

        private void CreateNewBubbleOnTheBoard(Vector2Int newBubbleCoor)
        {
            flyingBubble.isFlyingBubble = false;
            flyingBubble.RemoveTransformPosition();
            flyingBubble.AddPosition(newBubbleCoor);
            flyingBubble.isNewBubble = true;
        }

        private float CalculateTime(float distance)
        {
            return distance / contexts.config.gameConfig.value.MovementSpeed;
        }

        public Vector2 flyingBubbleTransformPosition
        {
            get => flyingBubble.transformPosition.value;
            set => flyingBubble.ReplaceTransformPosition(value);
        }

    }
}
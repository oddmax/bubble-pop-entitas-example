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
            var launcherEntity = launcher.GetSingleEntity();
            var previewEntity = preview.GetSingleEntity();

            flyingBubble = bubbleToLaunch.GetSingleEntity();

            if (flyingBubble != null && flyingBubble.isLaunchBubble)
            {
                launcherEntity.ReplaceVisible(false);
                previewEntity.ReplaceVisible(false);

                flyingBubble.isLaunchBubble = false;
                flyingBubble.isFlyingBubble = true;
                flyingBubble.AddCurrentTrajectoryIndex(0, 0);
            }
        }
    }
}
using System.Collections.Generic;
using Entitas;
using Features.Pieces;

namespace Features.Board.Systems
{
    public class CompleteMergeSystem : ReactiveSystem<TimerEntity>
    {
        private readonly Contexts contexts;

        public CompleteMergeSystem(Contexts contexts) : base(contexts.timer)
        {
            this.contexts = contexts;
        }

        protected override ICollector<TimerEntity> GetTrigger(IContext<TimerEntity> context)
        {
            return context.CreateCollector(
                TimerMatcher.TimerRunning.Removed()
            );
        }

        protected override bool Filter(TimerEntity entity)
        {
            return entity.hasMergeProgress && entity.hasTimer && !entity.isTimerRunning;
        }

        protected override void Execute(List<TimerEntity> entities)
        {
            foreach (var e in entities)
            {
                var mergeProgressMergeNumber = e.mergeProgress.mergeNumber;
                var mergeBubblePosition = e.mergeProgress.mergeBubblePosition;
                
                e.RemoveTimer();
                contexts.timer.RemoveMergeProgress();
                var newBubble = contexts.game.GetBubbleWithPosition(mergeBubblePosition);
                contexts.game.SpawnEffect(
                    CoordinatesConverter.Convert(mergeBubblePosition, contexts.config.gameConfig.value.BubbleSize), mergeProgressMergeNumber);
                
                contexts.game.SpawnTextEffect(
                    CoordinatesConverter.Convert(mergeBubblePosition, contexts.config.gameConfig.value.BubbleSize), mergeProgressMergeNumber);
                
                contexts.gameState.ReplaceScore(contexts.gameState.score.value + mergeProgressMergeNumber);

                if (newBubble != null)
                {
                    newBubble.isNewBubble = true;
                }
                else
                {
                    contexts.game.isMoveComplete = true;
                }
            }
        }

        
    }
}
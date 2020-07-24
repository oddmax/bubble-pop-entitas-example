using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Features.View
{
    public class SpawnTextViewSystem: ReactiveSystem<GameEntity>
    {
        readonly Transform _previewParent;
        private readonly BubbleSprite[] _bubbleSprites;

        public SpawnTextViewSystem(Contexts contexts) : base(contexts.game)
        {
            _previewParent = GameObject.FindWithTag("Board").transform;
            _bubbleSprites = contexts.config.gameConfig.value.BubbleSprites;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.TextSpawn);

        protected override bool Filter(GameEntity entity) => entity.hasAsset && entity.isTextSpawn && !entity.hasView && !entity.isDestroyed;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
                SpawnEffect(e);
        }

        void SpawnEffect(GameEntity entity)
        {
            var prefab = Resources.Load<GameObject>(entity.asset.value);
            var gameObject = Object.Instantiate(prefab, _previewParent);
            var view = gameObject.GetComponent<SpawnTextView>();
            view.bubbleNumber = entity.bubbleNumber.Number;
            view.Show();
            Vector3 position = entity.transformPosition.value;
            position.z = -5;
            gameObject.transform.localPosition = position; 
            entity.isTextSpawn = false;
            entity.Destroy();
        }
    }
}
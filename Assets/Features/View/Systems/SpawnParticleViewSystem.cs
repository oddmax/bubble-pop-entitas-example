using System.Collections.Generic;
using Entitas;
using Features.Pieces.View;
using UnityEngine;

namespace Features.View
{
    public class SpawnParticleViewSystem : ReactiveSystem<GameEntity>
    {
        readonly Transform parent;
        private readonly BubbleSprite[] bubbleSprites;

        public SpawnParticleViewSystem(Contexts contexts) : base(contexts.game)
        {
            parent = GameObject.FindWithTag("Board").transform;
            bubbleSprites = contexts.config.gameConfig.value.BubbleSprites;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.Asset);

        protected override bool Filter(GameEntity entity) => entity.hasAsset && entity.isParticle && !entity.hasView && !entity.isDestroyed;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
                SpawnEffect(e);
        }

        void SpawnEffect(GameEntity entity)
        {
            var prefab = Resources.Load<GameObject>(entity.asset.value);
            var gameObject = Object.Instantiate(prefab, parent);
            var view = gameObject.GetComponent<ParticleEffectView>();
            view.SetSpritesInfo(bubbleSprites);
            view.bubbleNumber = entity.bubbleNumber.Number;
            view.Show();
            Vector3 position = entity.transformPosition.value;
            position.z = -5;
            gameObject.transform.localPosition = position; 
            entity.isParticle = false;
            entity.Destroy();
        }
    }
}
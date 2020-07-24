using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Features.View
{
    public class AddLauncherView: ReactiveSystem<GameEntity>
    {
        readonly Transform _boardParent;
        readonly Transform _previewParent;
        private readonly BubbleSprite[] _bubbleSprites;

        public AddLauncherView(Contexts contexts) : base(contexts.game)
        {
            
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
            => context.CreateCollector(GameMatcher.Asset);

        protected override bool Filter(GameEntity entity) => entity.hasAsset && !entity.hasView && !entity.isDestroyed;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
                e.AddView(instantiateView(e));
        }

        IView instantiateView(GameEntity entity)
        {
            var prefab = Resources.Load<GameObject>(entity.asset.value);
            var view = Object.Instantiate(prefab, _boardParent).GetComponent<IView>();
            view.SetSpritesInfo(_bubbleSprites);
            view.Link(entity);
            return view;
        }

    }
}
using DG.Tweening;
using Entitas;
using UnityEngine;

public class BubbleView : View, ITransformPositionListener
{
    public override void Link(IEntity entity)
    {
        base.Link(entity);
        var e = (GameEntity)entity;
        e.AddTransformPositionListener(this);
        transform.position = e.transformPosition.value;
    }

    public void OnTransformPosition(GameEntity entity, Vector2 value)
    {
        transform.DOKill();
        transform.position = entity.transformPosition.value;
    }

    public override void OnDestroyed(GameEntity entity)
    {
        base.OnDestroyed(entity);
        entity.Destroy();
    }
}
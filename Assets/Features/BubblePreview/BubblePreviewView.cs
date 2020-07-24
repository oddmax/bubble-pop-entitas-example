using DG.Tweening;
using Entitas;
using Features.Pieces;
using UnityEngine;

public class BubblePreviewView : MonoBehaviour, IView ,IPositionListener, IVisibleListener
{
    public SpriteRenderer SpriteRenderer;
    public void Link(IEntity entity)
    {
        var e = (GameEntity)entity;
        e.AddPositionListener(this);
        e.AddVisibleListener(this);

        if (e.hasPosition)
        {
            transform.localPosition = CoordinatesConverter.Convert(e.position.value, 1.2f);
        }

        SpriteRenderer.enabled = false;
    }

    public void SetSpritesInfo(BubbleSprite[] bubbleSprites)
    {
    }

    public virtual void OnPosition(GameEntity entity, Vector2Int value)
    {
        transform.DOKill();
        transform.localScale = new Vector3(0.01f, 0.01f, 1);
        var newPos = CoordinatesConverter.Convert(value, 1.2f);
        newPos.z = -1;
        transform.localPosition = newPos;

        transform.DOScale( new Vector3(0.45f, 0.45f, 1), 0.5f);
    }

    public void OnVisible(GameEntity entity, bool isVisible)
    {
        SpriteRenderer.enabled = isVisible;
    }
}

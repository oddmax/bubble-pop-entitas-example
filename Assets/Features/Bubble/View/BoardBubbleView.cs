using System;
using DG.Tweening;
using Entitas;
using Features.Pieces;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardBubbleView : View, IPositionListener, IMergeTargetListener, IFallingListener
{
    public float destroyDuration;
    public Vector2Int bubblePosition;
    public Collider2D collider;
    public Rigidbody2D rigidbody2D;
    public GameObject shadow;

    public override void Link(IEntity entity)
    {
        base.Link(entity);
        var e = (GameEntity)entity;
        e.AddPositionListener(this);
        e.AddMergeTargetListener(this);
        e.AddFallingListener(this);
        collider.enabled = false;
        if (e.hasPosition)
        {
            if(e.hasStartPosition)
                transform.localPosition = CoordinatesConverter.Convert(e.startPosition.Position, size);
            else
                transform.localPosition = CoordinatesConverter.Convert(e.position.value, size);
            
            bubblePosition = e.position.value;
            collider.enabled = true;
        }
    }
    
    public virtual void OnPosition(GameEntity entity, Vector2Int value)
    {
        transform.DOLocalMove(CoordinatesConverter.Convert(value, size), 0.3f);
        bubblePosition = value;

        collider.enabled = true;
    }

    protected override void destroy()
    {
        var color = sprite.color;
        color.a = 0f;
        gameObject.transform
            .DOScale(0.3f, destroyDuration)
            .OnComplete(() =>
            {
                base.destroy();
            });
    }

    public void OnMergeTarget(GameEntity entity, Vector2Int value)
    {
        shadow.SetActive(false);
        gameObject.transform.DOLocalMove(CoordinatesConverter.Convert(value, size), destroyDuration).OnComplete(
            () =>
            {
                entity.isDestroyed = true;
                base.destroy();
                entity.Destroy();
            });
    }

    public void OnFalling(GameEntity entity)
    {
        rigidbody2D.isKinematic = false;
        rigidbody2D.AddForce(new Vector2(Random.Range(-2f, 2f), 0), ForceMode2D.Impulse);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (rigidbody2D.isKinematic == false && other.gameObject.tag == "Bottom")
        {
            base.destroy();
        }
    }
}

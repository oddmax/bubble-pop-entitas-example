using DG.Tweening;
using Entitas;
using Entitas.Unity;
using Features.Pieces;
using TMPro;
using UnityEngine;

public class View : MonoBehaviour, IView, IDestroyedListener, IBubbleNumberListener, ITransformPositionListener
{
    protected BubbleSprite[] bubbleSprites;
    public Sprite[] spriteArray;
    public float size;
    public SpriteRenderer sprite;

    public virtual void Link(IEntity entity)
    {
        gameObject.Link(entity);
        var e = (GameEntity)entity;
        
        e.AddDestroyedListener(this);
        e.AddBubbleNumberListener(this);
        e.AddTransformPositionListener(this);
        
        if(e.hasTransformPosition)
            transform.position = e.transformPosition.value;
       
        sprite.sprite = GetBubbleSprite(e.bubbleNumber.Number);
    }

    

    public void SetSpritesInfo(BubbleSprite[] bubbleSprites)
    {
        this.bubbleSprites = bubbleSprites;
    }

    public virtual void OnDestroyed(GameEntity entity)
    {
        destroy();
    }
    
    public void OnBubbleNumber(GameEntity entity, int number)
    {
        sprite.sprite = GetBubbleSprite(number);
    }

    protected virtual void destroy()
    {
        gameObject.Unlink();
        Destroy(gameObject);
    }

    private Sprite GetBubbleSprite(int number)
    {
        foreach (var bubbleSpriteInfo in bubbleSprites)
        {
            if (bubbleSpriteInfo.number == number)
                return bubbleSpriteInfo.sprite;
        }

        return null;
    }


    public void OnTransformPosition(GameEntity entity, Vector2 value)
    {
        DOTween.Kill(this);
        transform.position  = value;
    }
}

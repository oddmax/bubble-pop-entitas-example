using Entitas;

public interface IView
{
    void Link(IEntity entity);
    void SetSpritesInfo(BubbleSprite[] bubbleSprites);
}

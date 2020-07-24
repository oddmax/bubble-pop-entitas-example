using UnityEngine;

namespace Features.Pieces.View
{
    public class ParticleEffectView : MonoBehaviour
    {
        public ParticleSystem particleSystem;
        public float time;
        public int bubbleNumber;
        private BubbleSprite[] bubbleSprites;
        
        public void SetSpritesInfo(BubbleSprite[] bubbleSprites)
        {
            this.bubbleSprites = bubbleSprites;
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

        public void Show()
        {
            var textureSheetAnimation = particleSystem.textureSheetAnimation;
            var bubbleSprite = GetBubbleSprite(bubbleNumber);
            if(bubbleSprite != null)
                textureSheetAnimation.SetSprite(0, bubbleSprite);
            particleSystem.Play();
            Destroy(gameObject, time);
        }
    }
}
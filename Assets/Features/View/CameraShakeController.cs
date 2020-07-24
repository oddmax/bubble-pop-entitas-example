using UnityEngine;

namespace Features.View
{
    public class CameraShakeController  : MonoBehaviour, IAnyCameraShakeListener
    {
        [SerializeField]
        private StressReceiver stressReceiver;

        void Start()
        {
            var listener = Contexts.sharedInstance.game.CreateEntity();
            listener.AddAnyCameraShakeListener(this);
        }

        public void OnAnyCameraShake(GameEntity entity, int amountOfShakes)
        {
            stressReceiver.InduceStress(0.1f);
        }
    }
}
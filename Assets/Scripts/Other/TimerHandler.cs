using UnityEngine;

namespace Other
{
    public class TimerHandler : MonoBehaviour
    {
        public float RemainingTime { get; private set; }
        public bool IsActive { get; private set; }
        
        public void Setup(float time)
        {
            RemainingTime = time;
            IsActive = true;
        }

        public void Tick()
        {
            if (!IsActive) return;
            if (RemainingTime > 0)
            {
                RemainingTime -= Time.deltaTime;
            }
            else
            {
                RemainingTime = 0;
                IsActive = false;
            }
        }
    }
}
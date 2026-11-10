using Game.ImpulseComponent;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.GameObjects.Content.Monkey
{
    public class EmemyImpulseInput : MonoBehaviour
    {
        [SerializeField] private float _impulseInterval = 1f;
        private float _nextImpulseTime;
        [SerializeField] private ImpulseRequestComponent impulseRequest;

        private void Update()
        {
            if (Time.time >= _nextImpulseTime)
            {
                impulseRequest.Impulse();
                _nextImpulseTime = Time.time + _impulseInterval;
            }
        }
    }
}
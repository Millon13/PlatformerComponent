using Game.ImpulseComponent;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Monkey
{
    public class EmemyPushInput: MonoBehaviour
    {
      
        [SerializeField] private float _pushInterval = 1f;
        private float _nextPushTime;
        [SerializeField] private PushRequestComponent _pushRequest;
        private void Update()
        {
            if (Time.time >= _nextPushTime)
            {
                _pushRequest.Push();
                _nextPushTime = Time.time + _pushInterval;
            }
        }
    }
}
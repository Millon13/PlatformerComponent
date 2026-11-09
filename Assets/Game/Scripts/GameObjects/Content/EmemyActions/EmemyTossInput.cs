using Game.ImpulseComponent;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Monkey
{
    public class EmemyTossInput: MonoBehaviour
    {
        [SerializeField] private float _tossInterval = 1f;
        private float _nextTossTime;
        [SerializeField] private TossRequestComponent _tossRequest;
        private void Update()
        {
            if (Time.time >= _nextTossTime)
            {
                _tossRequest.Toss();
                _nextTossTime = Time.time + _tossInterval;
            }
        }
    }
}
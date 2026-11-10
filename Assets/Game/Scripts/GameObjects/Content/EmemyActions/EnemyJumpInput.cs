using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Monkey
{
    public class EnemyJumpInput : MonoBehaviour
    {
        [SerializeField] private float _jumpInterval = 2f;
        private float _nextJumpTime;
        private float _nextPushTime;
        [SerializeField] private JumpRequestComponent _jumpRequest;

        private void Update()
        {
            if (Time.time >= _nextJumpTime)
            {
                _jumpRequest.Jump();
                _nextJumpTime = Time.time + _jumpInterval;
            }
        }
    }
}
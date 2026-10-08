using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class PushComponent: MonoBehaviour
    {
        [SerializeField] private KeyCode PushButton = KeyCode.Mouse1;
        [SerializeField] Rigidbody2D _rigidbody2D;
        public bool CanPush;
        private float _nextPushTime = 0f;
        private bool _isPreparingPush = false;
        [SerializeField] float cooldown = 0.35f;
        [SerializeField] float delay = 0.15f;
        [SerializeField] float _forceX=1f;
        [SerializeField] float _forceY=10f;
        public Action<bool> OnPush;
        public void Push(float forceX,float forceY)
        {
            if (_forceX != 0)
                _rigidbody2D.AddForceY(_forceX, ForceMode2D.Impulse);
            if (_forceY != 0)
                _rigidbody2D.AddForceY(_forceY, ForceMode2D.Impulse);
        }
        private void Update()
        {
            CanPush = Time.time >= _nextPushTime && !_isPreparingPush;
    
            if (Input.GetKey(PushButton) && CanPush)
            {
                StartCoroutine(PushWithDelay());
            }
            else if (!Input.GetKey(PushButton))
            {
                OnPush?.Invoke(false);
            }
        }

        private IEnumerator PushWithDelay()
        {
            _isPreparingPush = true;
            yield return new WaitForSeconds(delay);
            Push(_forceX,_forceX);
            OnPush?.Invoke(true);
            _nextPushTime = Time.time + cooldown;
            _isPreparingPush = false;
        }
    }
}
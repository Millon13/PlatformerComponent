using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class PushComponent: MonoBehaviour
    {
        [SerializeField] private KeyCode PushButton = KeyCode.Mouse1;
        private Rigidbody2D _rigidbody2D;
        public bool CanPush;
        private float _nextPushTime = 0f;
        private bool _isPreparingPush = false;
        [SerializeField] float cooldown = 0.35f;
        [SerializeField] float delay = 0.15f;
        [SerializeField] float _forceX=1f;
        [SerializeField] float _forceY=10f;
        public Action<bool> OnPush;
        public void Push(float forceX,float forceY,Rigidbody2D rigidbody2D)
        {
            if (_forceX != 0&&_rigidbody2D!=null)
                rigidbody2D.AddForceY(_forceX, ForceMode2D.Impulse);
            if (_forceY != 0&&_rigidbody2D!=null)
                rigidbody2D.AddForceY(_forceY, ForceMode2D.Impulse);
            return;
        }
        private void Update()
        {
            CanPush = Time.time >= _nextPushTime && !_isPreparingPush;
            if (Input.GetKey(PushButton) && CanPush)
            {
                StartCoroutine(PushWithDelay(_rigidbody2D));
            }
            else if (!Input.GetKey(PushButton))
            {
                OnPush?.Invoke(false);
            }
        }

        private void OnEntered(Collision2D other)
        {
            if (CanPush && Input.GetKey(PushButton))
            {
                _rigidbody2D=other.gameObject.GetComponent<Rigidbody2D>();
                Push(_forceX, _forceY, _rigidbody2D);
            }
        }

        private IEnumerator PushWithDelay(Rigidbody2D rigidbody2D)
        {
            _isPreparingPush = true;
            yield return new WaitForSeconds(delay);
            Push(_forceX,_forceX,rigidbody2D);
            OnPush?.Invoke(true);
            _nextPushTime = Time.time + cooldown;
            _isPreparingPush = false;
        }
    }
}
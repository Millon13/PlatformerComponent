using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class PushComponent : MonoBehaviour
    {
        [SerializeField] private TriggerComponent _pushTrigger;
        
        [SerializeField] private KeyCode PushButton = KeyCode.Mouse0;
        
        private Rigidbody2D _rigidbody2D;
        
        public bool CanPush;
        
        private float _nextPushTime = 0f;
        
        private bool _isPreparingPush = false;
        
        [SerializeField] float cooldown = 0.35f;
        
        [SerializeField] float delay = 0.15f;
        
        [SerializeField] float _forceX;
        
        [SerializeField] float _forceY;
        
        public Action<bool> OnPush;
        
        private Rigidbody2D _targetBody;
        
        private bool _wasButtonHeld = false;
        
        [SerializeField] private bool isPlayerCharecter;

        public void Push(float forceX, float forceY, Rigidbody2D rb)
        {
            if (rb == null) return;
            if (forceX != 0) rb.AddForceX(forceX, ForceMode2D.Impulse);
            if (forceY != 0) rb.AddForceY(forceY, ForceMode2D.Impulse);
        }

        private void OnEnable()
        {
            _pushTrigger.OnEntered += OnTriggerEntered;
            _pushTrigger.OnExited += OnTriggerExited;
        }

        private void OnDisable()
        {
            _pushTrigger.OnEntered -= OnTriggerEntered;
            _pushTrigger.OnExited -= OnTriggerExited;
        }

        private void OnTriggerEntered(Collider2D other)
        {
            if (other.attachedRigidbody != null)
                _targetBody = other.attachedRigidbody;
        }

        private void OnTriggerExited(Collider2D other)
        {
            if (_targetBody != null && other.attachedRigidbody == _targetBody)
                _targetBody = null;
        }

        private void Update()
        {
            CanPush = Time.time >= _nextPushTime && !_isPreparingPush;

            if (PushButton != KeyCode.None && isPlayerCharecter)
            {
                bool held = Input.GetKey(PushButton);

                if (held && !_wasButtonHeld && CanPush && _targetBody != null)
                {
                    StartCoroutine(PushWithDelay(_targetBody));
                }

                if (held != _wasButtonHeld)
                    OnPush?.Invoke(held);

                _wasButtonHeld = held;
            }
            else
            {
                StartCoroutine(PushWithDelay(_targetBody));
            }
        }

        private IEnumerator PushWithDelay(Rigidbody2D rb)
        {
            _isPreparingPush = true;
            yield return new WaitForSeconds(delay);

            if (rb != null)
            {
                float dir = GetPushDirectionX(rb);
                Push(Mathf.Abs(_forceX) * dir, _forceY, rb);
            }

            _nextPushTime = Time.time + cooldown;
            _isPreparingPush = false;
        }

        private float GetPushDirectionX(Rigidbody2D rb)
        {
            if (rb == null) return 0f;
            float diff = rb.position.x - transform.position.x;
            return Mathf.Sign(diff);
        }
    }
}
using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class TossComponent: MonoBehaviour
    {
        [SerializeField] private TriggerComponent _tossTrigger;
        [SerializeField] private KeyCode TossButton = KeyCode.Mouse0;
        private Rigidbody2D _rigidbody2D;
        public bool CanToss;
        private float _nextTossTime = 0f;
        private bool _isPreparingToss = false;
        [SerializeField] float cooldown = 0.35f;
        
        [SerializeField] float delay = 0.15f;
        
        [SerializeField] float _forceX=10f;
        [SerializeField] float _forceY=2f;
        
        public Action<bool> OnToss;
        private Rigidbody2D _targetBody;
        private bool _wasButtonHeld = false;

        public void Toss(float forceX, float forceY, Rigidbody2D rb)
        {
            if (rb == null) return;
            if (forceX != 0) rb.AddForceX(forceX, ForceMode2D.Impulse);
            if (forceY != 0) rb.AddForceY(forceY, ForceMode2D.Impulse);
        }

        private void OnEnable()
        {
            _tossTrigger.OnEntered += OnTriggerEntered;
            _tossTrigger.OnExited  += OnTriggerExited;
        }

        private void OnDisable()
        {
            _tossTrigger.OnEntered -= OnTriggerEntered;
            _tossTrigger.OnExited  -= OnTriggerExited;
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
            CanToss = Time.time >= _nextTossTime && !_isPreparingToss;

            bool held = Input.GetKey(TossButton);

            if (held && !_wasButtonHeld && CanToss && _targetBody != null)
            {
                StartCoroutine(TossWithDelay(_targetBody));
            }
            if (held != _wasButtonHeld)
                OnToss?.Invoke(held);

            _wasButtonHeld = held;
        }
        private float GetTossDirectionX(Rigidbody2D rb)
        {
            if (rb == null) return 0f;
            float diff = rb.position.x - transform.position.x;
            return Mathf.Sign(diff);
        }
        private IEnumerator TossWithDelay(Rigidbody2D rb)
        {
            _isPreparingToss = true;
            yield return new WaitForSeconds(delay);
           if (rb != null)
           {
               float dir = GetTossDirectionX(rb);
               Toss(Mathf.Abs(_forceX) * dir, _forceY, rb);
           }

            _nextTossTime = Time.time + cooldown;
            _isPreparingToss = false;
        }
        
    }
}
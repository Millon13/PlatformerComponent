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
        public Action<bool> OnCollideOther;
        public void Toss(float forceX,float forceY,Rigidbody2D rigidbody2D)
        {
            if (forceX != 0)
                _rigidbody2D.AddForceY(forceX, ForceMode2D.Impulse);
            if (forceY != 0)
                _rigidbody2D.AddForceY(forceY, ForceMode2D.Impulse);
            return;
        }
        
        private void Update()
        {
            CanToss = Time.time >= _nextTossTime && !_isPreparingToss;
            if (Input.GetKey(TossButton) && CanToss)
            {
                StartCoroutine(TossWithDelay(_rigidbody2D));
            }
            else if (!Input.GetKey(TossButton))
            {
                OnToss?.Invoke(false);
            }
        }
        
        private void OnEnable() => _tossTrigger.OnEntered += this.OnTriggerEntered;

        private void OnDisable() => _tossTrigger.OnEntered -= this.OnTriggerEntered;

        private void OnTriggerEntered(Collider2D other)
        {
            if (CanToss && Input.GetKey(TossButton))
            {
                _rigidbody2D=other.gameObject.GetComponent<Rigidbody2D>();
                Toss(_forceX, _forceY, _rigidbody2D);
            }
        }

        private IEnumerator TossWithDelay(Rigidbody2D rigidbody2D)
        {
            _isPreparingToss = true;
            yield return new WaitForSeconds(delay);
            OnToss?.Invoke(true);  
            _nextTossTime = Time.time + cooldown;
            _isPreparingToss = false;
        }
        
    }
}
using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class TossComponent: MonoBehaviour
    {
        [SerializeField] private KeyCode TossButton = KeyCode.Mouse0;
        [SerializeField] Rigidbody2D _rigidbody2D;
        public bool CanToss;
        private float _nextTossTime = 0f;
        private bool _isPreparingToss = false;
        [SerializeField] float cooldown = 0.35f;
        
        [SerializeField] float delay = 0.15f;
        
        [SerializeField] float _forceX=10f;
        [SerializeField] float _forceY=2f;
        public Action<bool> OnToss;
        public void Toss(float forceX,float forceY)
        {
            if (forceX != 0)
                _rigidbody2D.AddForceY(forceX, ForceMode2D.Impulse);
            if (forceY != 0)
                _rigidbody2D.AddForceY(forceY, ForceMode2D.Impulse);
        }
        
        private void Update()
        {
            CanToss = Time.time >= _nextTossTime && !_isPreparingToss;
    
            if (Input.GetKey(TossButton) && CanToss)
            {
                StartCoroutine(TossWithDelay());
            }
            else if (!Input.GetKey(TossButton))
            {
                OnToss?.Invoke(false);
            }
        }

        private IEnumerator TossWithDelay()
        {
            _isPreparingToss = true;
            yield return new WaitForSeconds(delay);
            Toss(_forceX,_forceX);
            OnToss?.Invoke(true);
            _nextTossTime = Time.time + cooldown;
            _isPreparingToss = false;
        }
        
    }
}
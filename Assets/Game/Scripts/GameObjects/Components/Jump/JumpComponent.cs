using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class JumpComponent : MonoBehaviour
    {
        [SerializeField] private KeyCode JumpButton = KeyCode.Space;
        
        [SerializeField] private float force = 12f;
        
        [SerializeField] Rigidbody2D _rigidbody2D;
        
        [SerializeField] float cooldown = 0.25f;
        
        [SerializeField] float delay = 0.05f;
        
        public Action<bool> OnJump;
        
        private float _nextJumpTime = 0f;
        
        public bool CanJump;
        [SerializeField] private bool isPlayerCharecter;

        private bool _isPreparingJump = false;
        private bool _wasButtonHeld = false;
        private void FixedUpdate()
        {
            if (!CanJump) return;
            if (_isPreparingJump) return;
            bool isJumping = Time.time >= _nextJumpTime
                      && !_isPreparingJump;

            if (isPlayerCharecter&&JumpButton!=KeyCode.None)
            {
                bool held = Input.GetKey(JumpButton);


                if (held && !_wasButtonHeld && isJumping)
                {
                    StartCoroutine(JumpWithDelay());
                }

                _wasButtonHeld = held;
            }
            else
            {
                Jump(force);
                _nextJumpTime = Time.time + cooldown;
            }

        }
        

        private IEnumerator JumpWithDelay()
        {
            
            _isPreparingJump = true;
            yield return new WaitForSeconds(delay);
            if (CanJump)
            {
                Jump(force);
                _nextJumpTime = Time.time + cooldown;
            }

            _isPreparingJump = false;
        }

        public void Jump(float f)
        {
            OnJump?.Invoke(true); 
            if (f == 0 || _rigidbody2D == null) return;
            if (!CanJump) return;  
            _rigidbody2D.AddForceY(f, ForceMode2D.Impulse);
           
        }

    }
}
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
        private void Update()
        {
            CanJump = Time.time >= _nextJumpTime
                      && !_isPreparingJump;

            if (isPlayerCharecter&&JumpButton!=KeyCode.None)
            {
                bool held = Input.GetKey(JumpButton);


                if (held && !_wasButtonHeld && CanJump)
                {
                    StartCoroutine(JumpWithDelay());
                }

                if (held != _wasButtonHeld)
                    OnJump?.Invoke(held);

                _wasButtonHeld = held;
            }
            else
            {
                StartCoroutine(JumpWithDelay());
            }

        }
        

        private IEnumerator JumpWithDelay()
        {
            _isPreparingJump = true;
            yield return new WaitForSeconds(delay);

            Jump(force);
            _nextJumpTime = Time.time + cooldown;
            _isPreparingJump = false;
        }

        public void Jump(float f)
        {
            if (f != 0 && _rigidbody2D != null)
                _rigidbody2D.AddForceY(f, ForceMode2D.Impulse);
        }

    }
}
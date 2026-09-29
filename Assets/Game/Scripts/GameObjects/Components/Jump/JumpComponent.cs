using System;
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
        [SerializeField] private AudioSource _audioSource;

        public void Jump(float force)
        {
            if (force != 0)
                _rigidbody2D.AddForceY(force, ForceMode2D.Impulse);
        }

        private void Update()
        {
           CanJump = Time.time >= _nextJumpTime;
            if (Input.GetKey(KeyCode.Space)&&CanJump)
            {

                Jump(force);
                OnJump?.Invoke(true);
                _nextJumpTime = Time.time + cooldown;
            }
            else
            {
                OnJump?.Invoke(false);
            }
        }

    }
}
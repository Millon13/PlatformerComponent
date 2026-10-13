using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game

{[RequireComponent(typeof(LookComponent))]
    public sealed class MoveTransformComponent : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 4.5f;

        [SerializeField] private KeyCode rightInput;
        [SerializeField] private KeyCode leftInput;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private LookComponent _lookComponent;

        public Action<bool> OnMove;
        private void Awake()
        {
            _lookComponent = GetComponent<LookComponent>();
        }
        public bool CanMove;
        public void Move(Vector2 direction)
        {
            if (direction != Vector2.zero) 
                _rb.linearVelocity = new Vector2(direction.x * _speed, _rb.linearVelocity.y);
            else
                _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
          
        }

        private void FixedUpdate()
        {
            if (CanMove)
            {
                if (Input.GetKey(rightInput) || Input.GetKey(leftInput))
                {
                    OnMove?.Invoke(true);
                    if (Input.GetKey(rightInput))
                    {
                        _lookComponent.Look(1);
                        Move(Vector2.right);
                    }
                    else if (Input.GetKey(leftInput))
                    {
                         _lookComponent.Look(-1);
                         Move(Vector2.left);
                    }
                }
                else
                {
                    OnMove?.Invoke(false);
                }
            }
            else
            {
                return;
            }
        }
    }
}
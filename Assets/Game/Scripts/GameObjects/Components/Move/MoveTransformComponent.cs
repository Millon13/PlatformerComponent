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
        
        private LookComponent _lookComponent;

        public Action<bool> OnMove;
        private void Awake()
        {
            _lookComponent = GetComponent<LookComponent>();
        }
        public bool CanMove;
        public void Move(Vector2 direction)
        {
            if (direction != Vector2.zero) 
                this.transform.Translate((Vector3) direction * _speed * Time.fixedDeltaTime);
        }

        private void Update()
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
                         Move(-Vector2.left);
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
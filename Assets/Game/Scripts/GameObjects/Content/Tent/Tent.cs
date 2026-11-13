using System;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Tent
{
    public class Tent : MonoBehaviour
    {
        public event Action<bool> OnFinished;
        
        [SerializeField] private float _detectionRadius = 5f;
        
        [SerializeField] private LayerMask _targetMask = ~0;
        
        private Collider2D[] _buffer;
        
        private Rigidbody2D _selfRb;
        
        private Transform _currentTarget;

        private void Awake()
        {
            _buffer = new Collider2D[16];
            _selfRb = GetComponent<Rigidbody2D>();
        }

        private void DetectTarget()
        {
            int count = Physics2D.OverlapCircleNonAlloc(transform.position,
                _detectionRadius, _buffer, _targetMask);

            bool found = false;

            for (int i = 0; i < count; i++)
            {
                var col = _buffer[i];
                if (col == null) continue;

                if (col.CompareTag("Character"))
                {
                    _currentTarget = col.transform;
                    found = true;
                    break;
                }
            }

            OnFinished?.Invoke(found);
        }

        private void Update()
        {
            DetectTarget();
        }
    }
}
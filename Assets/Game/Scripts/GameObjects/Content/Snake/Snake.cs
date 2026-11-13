using Game.Scripts.GameObjects.Components.Ground;
using Game.Scripts.GameObjects.Components.Health;
using Game.Scripts.GameObjects.Components.ImpulseComponent;
using Game.Scripts.GameObjects.Components.Look;
using Game.Scripts.GameObjects.Components.Move;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Snake
{
    public class Snake : MonoBehaviour, ImpulseComponent.IAction, ImpulseComponent.ICondition, MoveComponent.IAction,
        MoveComponent.ICondition
    {
        [SerializeField] private HealthComponent _healthComponent;
    
        [SerializeField] private TakeDamageColorComponent _takeDamageColorComponent;
    
        [SerializeField] private GroundedComponent _groundedComponent;
    
        [SerializeField] private ImpulseComponent _impulseComponent;
    
        [SerializeField] private float _damage = 1f;
    
        [SerializeField] private Transform _charectorTransform;
    
        [SerializeField] private MoveComponent _moveComponent;
    
        [SerializeField] private LookComponent _lookComponent;
    
        [SerializeField] private float _detectionRadius = 10f;
    
        [SerializeField] private LayerMask _targetMask = ~0;
    
        private Collider2D[] _buffer;
    
        private Rigidbody2D _selfRb;
    
        private Transform _currentTarget;
    
        private bool isTargetFollowing;
    
        [SerializeField] private float _impulseInterval = 1f;
    
        private float _nextImpulseTime;

        private void Awake()
        {
            _buffer = new Collider2D[16];
            _selfRb = GetComponent<Rigidbody2D>();
            _healthComponent = GetComponent<HealthComponent>();
            _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
            _impulseComponent.SetAction(this);
            _impulseComponent.SetCondition(this);
            _moveComponent.SetAction(this);
            _moveComponent.SetCondition(this);
        }


        bool MoveComponent.ICondition.Evaluate() =>
            isTargetFollowing;

        void MoveComponent.IAction.Invoke(Vector3 direction)
        {
            _moveComponent.MoveStep(direction);
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

            isTargetFollowing = found;

            if (found)
            {
                _lookComponent.LookAt(_currentTarget);
            }
        }

        private void Update()
        {
            DetectTarget();
            HandleMovement();
            if (Time.time >= _nextImpulseTime)
            {
                _impulseComponent.Impulse();
                _nextImpulseTime = Time.time + _impulseInterval;
            }
        }

        private void HandleMovement()
        {
            if (!isTargetFollowing || _currentTarget == null) return;

            Vector3 dir = (_currentTarget.position - transform.position);
            dir.z = 0;
            dir.Normalize();
            _moveComponent.Move(dir);
        }

        private void OnCollisionEnter2D(Collision2D collision) => TryHealthCollide(collision);

        public void TryHealthCollide(Collision2D collision)
        {
            HealthComponent damageable = collision.collider.GetComponentInParent<HealthComponent>();
            if (damageable == null || damageable == this)
                return;

            damageable.TakeDamage(_damage);
        }

        void ImpulseComponent.IAction.Invoke()
        {
            _impulseComponent.ImpulseWithDelay();
        }

        bool ImpulseComponent.ICondition.Evaluate() =>
            _healthComponent.IsAlive() && _groundedComponent.IsGrounded;
    }
}
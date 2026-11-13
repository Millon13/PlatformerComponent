using Game.Scripts.GameObjects.Components.Ground;
using Game.Scripts.GameObjects.Components.Health;
using Game.Scripts.GameObjects.Components.ImpulseComponent;
using Game.Scripts.GameObjects.Components.Jump;
using Game.Scripts.GameObjects.Components.Look;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Monkey
{
    public class Monkey : MonoBehaviour, JumpComponent.ICondition, JumpComponent.IAction,
        ImpulseComponent.IAction, ImpulseComponent.ICondition
    {
        [SerializeField] private Transform _charectorTransform;
    
        [SerializeField] private HealthComponent _healthComponent;
    
        [SerializeField] private TakeDamageColorComponent _takeDamageColorComponent;
    
        [SerializeField] private LookComponent _lookComponent;
    
        [SerializeField] private JumpComponent _jumpComponent;
    
        [SerializeField] private GroundedComponent _groundedComponent;
    
        [SerializeField] private ImpulseComponent _impulseComponent;

        [SerializeField] private float _damage = 1f;
    
        [SerializeField] private float _jumpInterval = 2f;
    
        private float _nextJumpTime;
    
        private float _nextPushTime;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
            _lookComponent = GetComponent<LookComponent>();
            _healthComponent = GetComponent<HealthComponent>();
            _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
            _impulseComponent = GetComponent<ImpulseComponent>();
            _groundedComponent = GetComponent<GroundedComponent>();
            _jumpComponent = GetComponent<JumpComponent>();
            _jumpComponent.SetCondition(this);
            _jumpComponent.SetAction(this);
            _impulseComponent.SetAction(this);
            _impulseComponent.SetCondition(this);
        }


        private void OnCollisionEnter2D(Collision2D collision) => TryHealthCollide(collision);

        public void TryHealthCollide(Collision2D collision)
        {
            HealthComponent damageable = collision.collider.GetComponentInParent<HealthComponent>();
            if (damageable == null || damageable == this)
                return;

            damageable.TakeDamage(_damage);
        }


        private void Update()
        {
            if (Time.time >= _nextJumpTime)
            {
                _jumpComponent.Jump();
                _nextJumpTime = Time.time + _jumpInterval;
            }

            if (_healthComponent.IsAlive() && _groundedComponent.IsGrounded)
            {
                _impulseComponent.Impulse();
            }
        }


        void JumpComponent.IAction.Invoke()
        {
            _jumpComponent.TryJump();
        }

        bool JumpComponent.ICondition.Evaluate() =>
            _healthComponent.IsAlive() && _groundedComponent.IsGrounded;

        void ImpulseComponent.IAction.Invoke()
        {
            _impulseComponent.ImpulseWithDelay();
        }

        bool ImpulseComponent.ICondition.Evaluate() =>
            _healthComponent.IsAlive() && _groundedComponent.IsGrounded;
    }
}
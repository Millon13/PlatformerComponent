using Game.Scripts.GameObjects.Components.ControlComponent;
using Game.Scripts.GameObjects.Components.Ground;
using Game.Scripts.GameObjects.Components.Health;
using Game.Scripts.GameObjects.Components.ImpulseComponent;
using Game.Scripts.GameObjects.Components.Move;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Spider
{
    public class Spider : MonoBehaviour, ImpulseComponent.ICondition, ImpulseComponent.IAction, MoveComponent.ICondition,
        MoveComponent.IAction
    {
        [SerializeField] private HealthComponent _healthComponent;
    
        private TakeDamageColorComponent _takeDamageColorComponent;
    
        [SerializeField] private PatrolComponent _patrolComponent;
    
        [SerializeField] private GroundedComponent _groundedComponent;
    
        [SerializeField] private MoveComponent moveComponent;
    
        [SerializeField] private ImpulseComponent _impulseComponent;
    
        [SerializeField] private float _damage = 1f;
    
        [SerializeField] private float _impulseInterval = 1f;

        private float _nextImpulseTime;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
            _patrolComponent = GetComponent<PatrolComponent>();
            _impulseComponent.SetAction(this);
            _impulseComponent.SetCondition(this);
            moveComponent.SetAction(this);
            moveComponent.SetCondition(this);
        }

        private void OnCollisionEnter2D(Collision2D collision) => TryHealthCollide(collision);

        bool MoveComponent.ICondition.Evaluate() =>
            _healthComponent.IsAlive();

        void ImpulseComponent.IAction.Invoke()
        {
            _impulseComponent.ImpulseWithDelay();
        }

        void MoveComponent.IAction.Invoke(Vector3 direction)
        {
            moveComponent.MoveStep(direction);
        }

        private void Update()
        {
            if (Time.time >= _nextImpulseTime)
            {
                _impulseComponent.Impulse();
                _nextImpulseTime = Time.time + _impulseInterval;
            }

            _patrolComponent.CanPatrol = true;
        }

        public void TryHealthCollide(Collision2D collision)
        {
            HealthComponent damageable = collision.collider.GetComponentInParent<HealthComponent>();
            if (damageable == null || damageable == this)
                return;

            damageable.TakeDamage(_damage);
        }

        bool ImpulseComponent.ICondition.Evaluate() =>
            _healthComponent.IsAlive() && _groundedComponent.IsGrounded;

        public void Invoke(Vector3 direction)
        {
            throw new System.NotImplementedException();
        }
    }
}
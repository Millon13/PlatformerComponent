using Game.Scripts.GameObjects.Components.Ground;
using Game.Scripts.GameObjects.Components.Health;
using Game.Scripts.GameObjects.Components.Jump;
using Game.Scripts.GameObjects.Components.Look;
using Game.Scripts.GameObjects.Components.Move;

using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Character
{
    public interface IPushComponent
    {
        void Push();
    }

    public interface ITossComponent
    {
        void Toss();
    }

    public class Character : MonoBehaviour, MoveComponent.IAction,
        MoveComponent.ICondition, JumpComponent.IAction,
        JumpComponent.ICondition,  Game.Scripts.GameObjects.Components.ImpulseComponent.ImpulseComponent.IAction,
        Game.Scripts.GameObjects.Components.ImpulseComponent.ImpulseComponent.ICondition, IPushComponent, ITossComponent
    {
        [SerializeField] private HealthComponent _healthComponent;

        [SerializeField] private TakeDamageColorComponent _takeDamageColorComponent;

        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private Components.ImpulseComponent.ImpulseComponent _impulsePushComponent;

        [SerializeField] private Components.ImpulseComponent.ImpulseComponent _impulseTossComponent;

        [SerializeField] private MoveComponent _moveTransformComponent;

        [SerializeField] private LookComponent _lookComponent;

        [SerializeField] private GroundedComponent _groundedComponent;

        [SerializeField] private JumpComponent _jumpComponent;

        [SerializeField] private float _damage = 1f;

        private void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
            _moveTransformComponent = GetComponent<MoveComponent>();
            _groundedComponent = GetComponent<GroundedComponent>();
            _jumpComponent = GetComponent<JumpComponent>();
            moveComponent.SetAction(this);
            moveComponent.SetCondition(this);
            _jumpComponent.SetCondition(this);
            _jumpComponent.SetAction(this);
            _impulsePushComponent.SetAction(this);
            _impulsePushComponent.SetCondition(this);
            _impulseTossComponent.SetAction(this);
            _impulseTossComponent.SetCondition(this);
        }

        private void OnCollisionEnter2D(Collision2D collision) => TryHealthCollide(collision);

        public void TryHealthCollide(Collision2D collision)
        {
            HealthComponent damageable = collision.collider.GetComponentInParent<HealthComponent>();
            if (damageable == null || damageable == this)
                return;

            damageable.TakeDamage(_damage);
        }

        void MoveComponent.IAction.Invoke(Vector3 direction)
        {
            if (direction != Vector3.zero)
            {
                _moveTransformComponent.MoveStep(direction);
                _lookComponent.Look(direction);
            }
        }

        void JumpComponent.IAction.Invoke()
        {
            _jumpComponent.TryJump();
        }

        void Game.Scripts.GameObjects.Components.ImpulseComponent.ImpulseComponent.IAction.Invoke()
        {
            _impulsePushComponent.ImpulseWithDelay();
            _impulseTossComponent.ImpulseWithDelay();
        }

        bool Game.Scripts.GameObjects.Components.ImpulseComponent.ImpulseComponent.ICondition.Evaluate() =>
            _healthComponent.IsAlive();


        bool MoveComponent.ICondition.Evaluate() =>
            _healthComponent.IsAlive();

        bool JumpComponent.ICondition.Evaluate() =>
            _healthComponent.IsAlive() && _groundedComponent.IsGrounded;


        public void Push()
        {
            _impulsePushComponent.Impulse();
        }

        public void Toss()
        {
            _impulseTossComponent.Impulse();
        }
    }
}
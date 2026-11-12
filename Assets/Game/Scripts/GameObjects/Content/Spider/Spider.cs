using Game;
using Game.ImpulseComponent;
using UnityEngine;
using UnityEngine.Serialization;

public class Spider : MonoBehaviour, ImpulseComponent.ICondition, ImpulseComponent.IAction,MoveComponent.ICondition
{
    [SerializeField] private HealthComponent _healthComponent;
    private TakeDamageColorComponent _takeDamageColorComponent;
    [SerializeField] private PatrolComponent _patrolComponent;
    [SerializeField] private GroundedComponent _groundedComponent;
    [SerializeField] private MoveComponent moveComponent;
    [SerializeField] private ImpulseComponent _impulseComponent;

    [SerializeField]
    private ImpulseComponent impulseComponent;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _impulseInterval = 1f;
                                                    
    private float _nextImpulseTime;
    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
        _patrolComponent = GetComponent<PatrolComponent>();
        impulseComponent.SetAction(this);
        impulseComponent.SetCondition(this);
    }
    private void OnCollisionEnter2D(Collision2D collision) => TryHealthCollide(collision);

    bool MoveComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive();

    private void Update()
    {
        if (Time.time >= _nextImpulseTime)
        {
            impulseComponent.Impulse();
            _nextImpulseTime = Time.time + _impulseInterval;
        }
        _patrolComponent.CanPatrol=true;
    }
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
using Game;
using Game.ImpulseComponent;
using UnityEngine;

public class Snake : MonoBehaviour, ImpulseComponent.IAction, ImpulseComponent.ICondition
{
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private TakeDamageColorComponent _takeDamageColorComponent;
    [SerializeField] private GroundedComponent _groundedComponent;
    [SerializeField] private ImpulseComponent _impulseComponent;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private Transform _charectorTransform;
    [SerializeField] private PatrolComponent _patrolComponent;
    [SerializeField] private LookComponent _lookComponent;
    private bool isTargetFollowing; 
    [SerializeField] private float _impulseInterval = 1f; 
    private float _nextImpulseTime;
    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
        _impulseComponent.SetAction(this);
        _impulseComponent.SetCondition(this);
        if (_patrolComponent != null)
            _patrolComponent.CanPatrol = true;
    }
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Character")
            TryFollow();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Character")
            TryFollow();
    }
    

    private void TryFollow()
    {
        _patrolComponent.Patrol(_charectorTransform);
        isTargetFollowing = true;
    }
   

    private void Update()
    {
        if (Time.time >= _nextImpulseTime)
        {
            _impulseComponent.Impulse();
            _nextImpulseTime = Time.time + _impulseInterval;
        }
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
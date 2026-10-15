using Game;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private Transform _charectorTransform;
    [SerializeField] private TriggerComponent _agroTrigger;
    [SerializeField] private Collider2D _followingActivateCollider;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private TakeDamageColorComponent _takeDamageColorComponent;
    [SerializeField] private LookComponent  _lookComponent;
    [SerializeField] private GroundedComponent _groundedComponent;
    [SerializeField] private PatrolComponent _patrolComponent;
    [SerializeField] private PushComponent _pushComponent;
    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
        _lookComponent = GetComponent<LookComponent>();
    }

    private void FixedUpdate()
    {
        _patrolComponent.CanPatrol=_healthComponent.IsAlive;
        _pushComponent.CanPush=_healthComponent.IsAlive&&_groundedComponent.IsGrounded;;
        if (_healthComponent.IsAlive&& _charectorTransform != null)
            _lookComponent.Look(_charectorTransform);
    }
    private void OnEnable()
    {
        _agroTrigger.OnEntered += OnTriggerEntered;
    }

    private void OnDisable()
    {
        _agroTrigger.OnEntered -= OnTriggerEntered;
    }

    private void OnTriggerEntered(Collider2D other)
    {
        if (other==_followingActivateCollider)
        {
           _patrolComponent.Patrol(_charectorTransform);
        }
    }
    
    
}

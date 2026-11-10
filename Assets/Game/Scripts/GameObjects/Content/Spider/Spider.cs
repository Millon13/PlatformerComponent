using Game;
using Game.ImpulseComponent;
using UnityEngine;
using UnityEngine.Serialization;

public class Spider : MonoBehaviour, ImpulseRequestComponent.ICondition, ImpulseRequestComponent.IAction
{
    [SerializeField] private HealthComponent _healthComponent;
    private TakeDamageColorComponent _takeDamageColorComponent;
    [SerializeField] private PatrolComponent _patrolComponent;
    [SerializeField] private GroundedComponent _groundedComponent;

    [SerializeField] private ImpulseComponent _impulseComponent;

    [SerializeField]
    private ImpulseRequestComponent impulseComponent;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
        _patrolComponent = GetComponent<PatrolComponent>();
        impulseComponent.SetAction(this);
        impulseComponent.SetCondition(this);
    }

    void ImpulseRequestComponent.IAction.Invoke()
    {
        _impulseComponent.BeginAction();
    }

    bool ImpulseRequestComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive() && _groundedComponent.IsGrounded;

    private void Update()
    {
        _patrolComponent.CanPatrol = _healthComponent.IsAlive() && _groundedComponent.IsGrounded;
    }
}
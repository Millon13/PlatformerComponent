using Game;
using Game.ImpulseComponent;
using UnityEngine;

public class Spider : MonoBehaviour,PushRequestComponent.ICondition,PushRequestComponent.IAction
{
    [SerializeField] private HealthComponent _healthComponent;
    private TakeDamageColorComponent _takeDamageColorComponent;
    [SerializeField] private PatrolComponent _patrolComponent;
    [SerializeField] private GroundedComponent _groundedComponent;

    [SerializeField] private ImpulseComponent _impulseComponent;
    [SerializeField]private PushRequestComponent _pushComponent;
    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
        _patrolComponent = GetComponent<PatrolComponent>();
        _pushComponent.SetAction(this);
        _pushComponent.SetCondition(this);
    }
    void PushRequestComponent.IAction.Invoke()
    {
        _impulseComponent.BeginAction();
    }
    bool PushRequestComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive()&&_groundedComponent.IsGrounded;

    private void Update()
    {
        _patrolComponent.CanPatrol = _healthComponent.IsAlive() && _groundedComponent.IsGrounded;
    }
}
using Game;
using Game.ImpulseComponent;
using UnityEngine;

public class Snake : MonoBehaviour, ImpulseRequestComponent.IAction, ImpulseRequestComponent.ICondition
{
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private TakeDamageColorComponent _takeDamageColorComponent;
    [SerializeField] private GroundedComponent _groundedComponent;
    [SerializeField] private ImpulseRequestComponent _tossComponent;
    [SerializeField] private ImpulseComponent _impulseComponent;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
        _tossComponent.SetAction(this);
        _tossComponent.SetCondition(this);
    }


    void ImpulseRequestComponent.IAction.Invoke()
    {
        _impulseComponent.BeginAction();
    }

    bool ImpulseRequestComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive() && _groundedComponent.IsGrounded;
}
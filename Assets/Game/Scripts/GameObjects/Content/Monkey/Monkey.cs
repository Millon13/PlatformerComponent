using Game;
using Game.ImpulseComponent;
using UnityEngine;

public class Monkey : MonoBehaviour, JumpRequestComponent.ICondition, JumpRequestComponent.IAction,
    ImpulseRequestComponent.IAction, ImpulseRequestComponent.ICondition
{
    [SerializeField] private Transform _charectorTransform;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private TakeDamageColorComponent _takeDamageColorComponent;
    [SerializeField] private LookComponent _lookComponent;
    [SerializeField] private ImpulseRequestComponent impulseComponent;
    [SerializeField] private JumpRequestComponent _jumpRequestComponent;
    [SerializeField] private JumpComponent _jumpComponent;
    [SerializeField] private GroundedComponent _groundedComponent;
    [SerializeField] private ImpulseComponent _impulseComponent;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
        _lookComponent = GetComponent<LookComponent>();
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
        impulseComponent = GetComponent<ImpulseRequestComponent>();
        _groundedComponent = GetComponent<GroundedComponent>();
        _jumpRequestComponent = GetComponent<JumpRequestComponent>();
        _jumpRequestComponent.SetCondition(this);
        _jumpRequestComponent.SetAction(this);
        impulseComponent.SetAction(this);
        impulseComponent.SetCondition(this);
    }

    private void Update()
    {
        if (_healthComponent.IsAlive() && _groundedComponent.IsGrounded)
        {
            impulseComponent.Impulse();
        }
    }

    void JumpRequestComponent.IAction.Invoke()
    {
        _jumpComponent.Jump();
    }

    bool JumpRequestComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive() && _groundedComponent.IsGrounded;

    void ImpulseRequestComponent.IAction.Invoke()
    {
        _impulseComponent.BeginAction();
    }

    bool ImpulseRequestComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive() && _groundedComponent.IsGrounded;
}
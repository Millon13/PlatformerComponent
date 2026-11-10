using Game;
using Game.ImpulseComponent;
using UnityEngine;
using UnityEngine.Serialization;

public class Charector : MonoBehaviour, MoveRequestComponent.IAction,
    MoveRequestComponent.ICondition, JumpRequestComponent.IAction,
    JumpRequestComponent.ICondition, ImpulseRequestComponent.IAction,
    ImpulseRequestComponent.ICondition
{
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private TakeDamageColorComponent _takeDamageColorComponent;
    [SerializeField] private JumpRequestComponent _jumpRequestComponent;
    [SerializeField] private MoveRequestComponent _moveRequestComponent;
    [SerializeField] private ImpulseRequestComponent impulsePushRequestComponent;
    [SerializeField] private ImpulseRequestComponent impulseTossRequestComponent;
    [SerializeField] private MoveTransformComponent _moveTransformComponent;
    [SerializeField] private LookComponent _lookComponent;
    [SerializeField] private GroundedComponent _groundedComponent;
    [SerializeField] private JumpComponent _jumpComponent;
    [SerializeField] private ImpulseComponent _impulsePushComponent;
    [SerializeField] private ImpulseComponent _impulseTossComponent;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
        _moveTransformComponent = GetComponent<MoveTransformComponent>();
        _groundedComponent = GetComponent<GroundedComponent>();
        _jumpComponent = GetComponent<JumpComponent>();
        _moveRequestComponent.SetAction(this);
        _moveRequestComponent.SetCondition(this);
        _jumpRequestComponent.SetCondition(this);
        _jumpRequestComponent.SetAction(this);
        impulsePushRequestComponent.SetAction(this);
        impulsePushRequestComponent.SetCondition(this);
        impulseTossRequestComponent.SetAction(this);
        impulseTossRequestComponent.SetCondition(this);
    }

    void MoveRequestComponent.IAction.Invoke(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            _moveTransformComponent.MoveStep(direction);
            _lookComponent.Look(direction);
        }
    }

    void JumpRequestComponent.IAction.Invoke()
    {
        _jumpComponent.Jump();
    }

    void ImpulseRequestComponent.IAction.Invoke()
    {
        _impulsePushComponent.BeginAction();
    }

    bool MoveRequestComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive();

    bool JumpRequestComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive() && _groundedComponent.IsGrounded;

    bool ImpulseRequestComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive();
}
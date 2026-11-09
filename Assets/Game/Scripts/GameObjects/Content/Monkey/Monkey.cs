using Game;
using Game.ImpulseComponent;
using UnityEngine;

public class Monkey : MonoBehaviour,JumpRequestComponent.ICondition,JumpRequestComponent.IAction,PushRequestComponent.IAction,PushRequestComponent.ICondition
{
    [SerializeField] private Transform _charectorTransform;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private TakeDamageColorComponent _takeDamageColorComponent;
    [SerializeField] private LookComponent _lookComponent;
    [SerializeField] private PushRequestComponent _pushComponent;
    [SerializeField] private JumpRequestComponent _jumpComponent;
    [SerializeField] private GroundedComponent _groundedComponent;
    [SerializeField] private ImpulseComponent _impulseComponent;
    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
        _lookComponent = GetComponent<LookComponent>();
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
 
        _groundedComponent = GetComponent<GroundedComponent>();
        _jumpComponent = GetComponent<JumpRequestComponent>();
        _jumpComponent.SetCondition(this);
        _jumpComponent.SetAction(this);
        _pushComponent.SetAction(this);
        _pushComponent.SetCondition(this);
    }
    void JumpRequestComponent.IAction.Invoke()
    {
        _jumpComponent.Jump();
    }
    bool JumpRequestComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive()&&_groundedComponent.IsGrounded;
    
    void PushRequestComponent.IAction.Invoke()
    {
        _impulseComponent.BeginAction();
    }
    bool PushRequestComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive()&&_groundedComponent.IsGrounded;
    
}
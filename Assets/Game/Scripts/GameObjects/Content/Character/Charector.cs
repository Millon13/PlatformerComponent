using Game;
using Game.ImpulseComponent;
using UnityEngine;

public class Charector : MonoBehaviour,MoveRequestComponent.IAction, 
    MoveRequestComponent.ICondition,JumpRequestComponent.IAction,
    JumpRequestComponent.ICondition,PushRequestComponent.IAction,
    PushRequestComponent.ICondition,TossRequestComponent.IAction,
    TossRequestComponent.ICondition
{
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private TakeDamageColorComponent _takeDamageColorComponent;
    [SerializeField]private JumpRequestComponent _jumpRequestComponent;
    [SerializeField] private MoveRequestComponent _moveRequestComponent;
    [SerializeField] private MoveTransformComponent _moveTransformComponent;
    [SerializeField ]private LookComponent _lookComponent;
    [SerializeField] private GroundedComponent _groundedComponent;
    [SerializeField] private PushComponent _pushComponent;
    [SerializeField] private TossComponent _tossComponent;
    [SerializeField] private JumpComponent _jumpComponent;
    [SerializeField] private ImpulseComponent _impulseComponent;

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
        _moveRequestComponent.SetAction(this);
    }

    void MoveRequestComponent.IAction.Invoke(Vector3 direction)
    {
        _moveTransformComponent.MoveStep(direction);
        _lookComponent.Look(direction);
    }
    void JumpRequestComponent.IAction.Invoke()
    {
        _jumpComponent.Jump();
    }
     void PushRequestComponent.IAction.Invoke(float x, float y,Rigidbody2D rb)
    {
      // _impulseComponent.Impulse();
    }

    void TossRequestComponent.IAction.Invoke()
    {
       // _impulseComponent.Impulse();
    }

  

    bool MoveRequestComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive();

    bool JumpRequestComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive();
    bool PushRequestComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive();
    bool TossRequestComponent.ICondition.Evaluate() =>
        _healthComponent.IsAlive();
    private void OnDead() => this.gameObject.SetActive(false);

    public void Invoke(float fx, float fy, Rigidbody2D rb)
    {
        throw new System.NotImplementedException();
    }
}
using Game;
using UnityEngine;
[RequireComponent(typeof(HealthComponent), typeof(JumpComponent),typeof(GroundedComponent))]
[RequireComponent(typeof(MoveTransformComponent), typeof(PushComponent),typeof(TossComponent))]
public class Charector : MonoBehaviour
{
    
  private HealthComponent _healthComponent;
  private TakeDamageColorComponent _takeDamageColorComponent;
  private MoveTransformComponent _moveTransformComponent;
  private   GroundedComponent _groundedComponent;
  private PushComponent _pushComponent;
  private TossComponent _tossComponent;
  private JumpComponent _jumpComponent;

  private void Awake()
  {
      _healthComponent = GetComponent<HealthComponent>();
      _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
      _moveTransformComponent = GetComponent<MoveTransformComponent>();
      _groundedComponent = GetComponent<GroundedComponent>();
      _jumpComponent = GetComponent<JumpComponent>();
  }

  private void FixedUpdate()
  {
      _moveTransformComponent.CanMove=_healthComponent.IsAlive&&_groundedComponent.IsGrounded;
      _jumpComponent.CanJump=_healthComponent.IsAlive&&_groundedComponent.IsGrounded;
      _tossComponent.CanToss=_healthComponent.IsAlive&&_groundedComponent.IsGrounded&&!_pushComponent.CanPush;;
      _pushComponent.CanPush=_healthComponent.IsAlive&&_groundedComponent.IsGrounded&&!_tossComponent.CanToss;;
  }


}

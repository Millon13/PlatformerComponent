using Game;
using UnityEngine;
public class Charector : MonoBehaviour
{ [SerializeField]private HealthComponent _healthComponent;
  [SerializeField]private TakeDamageColorComponent _takeDamageColorComponent;
  private MoveTransformComponent _moveTransformComponent;
  [SerializeField] private   GroundedComponent _groundedComponent;
  [SerializeField]private PushComponent _pushComponent;
  [SerializeField] private TossComponent _tossComponent;
  [SerializeField]private JumpComponent _jumpComponent;

  private void Awake()
  {
      _healthComponent = GetComponent<HealthComponent>();
      _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
      _moveTransformComponent = GetComponent<MoveTransformComponent>();
      _groundedComponent = GetComponent<GroundedComponent>();
      _jumpComponent = GetComponent<JumpComponent>();
  }

  private void  FixedUpdate()
  {
      _moveTransformComponent.CanMove=_healthComponent.IsAlive;
      _jumpComponent.CanJump=_healthComponent.IsAlive&&_groundedComponent.IsGrounded;
      _tossComponent.CanToss=_healthComponent.IsAlive&&_groundedComponent.IsGrounded&&!_pushComponent.CanPush;;
      _pushComponent.CanPush=_healthComponent.IsAlive&&_groundedComponent.IsGrounded&&!_tossComponent.CanToss;;
  }


}

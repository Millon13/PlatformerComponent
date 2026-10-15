using Game;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    [SerializeField] private Transform _charectorTransform;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private TakeDamageColorComponent _takeDamageColorComponent;
    [SerializeField] private LookComponent _lookComponent;
    [SerializeField] private PushComponent _pushComponent;
    [SerializeField] private JumpComponent _jumpComponent;
    [SerializeField] private GroundedComponent _groundedComponent;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
        _lookComponent = GetComponent<LookComponent>();
    }

    private void FixedUpdate()
    {
        _jumpComponent.CanJump = _healthComponent.IsAlive && _groundedComponent.IsGrounded;
        _pushComponent.CanPush =
            _healthComponent.IsAlive && _groundedComponent.IsGrounded && _jumpComponent.CanEarthQuake;
        if (_healthComponent.IsAlive && _charectorTransform != null)
            _lookComponent.Look(_charectorTransform);
    }
}
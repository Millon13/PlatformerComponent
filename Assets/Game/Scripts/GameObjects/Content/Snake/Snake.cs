using Game;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private Transform _charectorTransform;
    [SerializeField] private Collider2D _followingActivateCollider;
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private TakeDamageColorComponent _takeDamageColorComponent;
    [SerializeField] private LookComponent  _lookComponent;

    [SerializeField] private MoveTransformComponent _moveComponent;
    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
        _lookComponent = GetComponent<LookComponent>();
    }

    private void FixedUpdate()
    {
        _moveComponent.Move(_charectorTransform.position);
        _lookComponent.Look(_charectorTransform);
    }
}

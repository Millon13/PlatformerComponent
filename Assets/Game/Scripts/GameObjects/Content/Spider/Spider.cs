using Game;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] private HealthComponent _healthComponent;
    private TakeDamageColorComponent _takeDamageColorComponent;
    [SerializeField] private PatrolComponent _patrolComponent;
    [SerializeField] private GroundedComponent _groundedComponent;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
        _patrolComponent = GetComponent<PatrolComponent>();
    }


    private void Update()
    {
        _patrolComponent.CanPatrol = _healthComponent.IsAlive && _groundedComponent.IsGrounded;
    }
}
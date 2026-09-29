using Game;
using UnityEngine;

public class Spider : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private TakeDamageColorComponent _takeDamageColorComponent;
    private LookComponent  _lookComponent;

    private void Awake()
    {
        _healthComponent = GetComponent<HealthComponent>();
        _takeDamageColorComponent = GetComponent<TakeDamageColorComponent>();
        _lookComponent = GetComponent<LookComponent>();
    }

    private void FixedUpdate()
    {
        
    }
}

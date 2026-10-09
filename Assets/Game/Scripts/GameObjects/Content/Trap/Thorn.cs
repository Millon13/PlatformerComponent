using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Trap
{
    public class Thorn:MonoBehaviour
    {
        [SerializeField]
        private TriggerComponent _thornTrigger;
        [SerializeField] private HealthComponent  _thornHealthComponent;

        private void Awake()
        {
            _thornHealthComponent = GetComponent<HealthComponent>();
        }
        private void OnEnable() => _thornTrigger.OnEntered += this.OnTriggerEntered;

        private void OnDisable() => _thornTrigger.OnEntered -= this.OnTriggerEntered;

        private void OnTriggerEntered(Collider2D other)
        {
            if (other.TryGetComponent(out HealthComponent health))
            {
                health.TakeDamage(1);
                _thornHealthComponent.TakeDamage(1);
            }

        }
    }
}
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Trap
{
    public class Thorn:MonoBehaviour
    {
        [SerializeField] private HealthComponent  _thornHealthComponent;

        private void Awake()
        {
            _thornHealthComponent = GetComponent<HealthComponent>();
        }
        private void OnEntered(Collider2D other)
        {

            if (other.TryGetComponent(out HealthComponent health))
            {
                health.TakeDamage(1);
                _thornHealthComponent.TakeDamage(1);
            }
        }
    }
}
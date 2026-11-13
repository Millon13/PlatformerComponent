using Game.Scripts.GameObjects.Components.Health;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Trap
{
    public class Thorn : MonoBehaviour
    {
        [SerializeField] private float _damage = 1f;
        private void OnCollisionEnter2D(Collision2D collision) => TryHealthCollide(collision);

        public void TryHealthCollide(Collision2D collision)
        {
            HealthComponent damageable = collision.collider.GetComponentInParent<HealthComponent>();
            if (damageable == null || damageable == this)
                return;

            damageable.TakeDamage(_damage);
        }
    }
}
using UnityEngine;

namespace Game.Scripts.GameObjects.Components.ImpulseComponent
{
    public class ImpulseCollideComponent: MonoBehaviour
    {
        [SerializeField] private Game.ImpulseComponent.ImpulseComponent _impulseComponent;
        private void OnCollisionEnter2D(Collision2D collision)
            => _impulseComponent.TryImpulse(collision.collider, collision.rigidbody);

        private void OnCollisionStay2D(Collision2D collision)
            => _impulseComponent.TryImpulse(collision.collider, collision.rigidbody);

        private void OnTriggerEnter2D(Collider2D other)
            => _impulseComponent.TryImpulse(other, other.attachedRigidbody);

        private void OnTriggerStay2D(Collider2D other)
            => _impulseComponent.TryImpulse(other, other.attachedRigidbody);
    }
}
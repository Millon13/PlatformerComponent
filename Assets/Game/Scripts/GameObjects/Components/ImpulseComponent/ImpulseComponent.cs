using System;
using System.Collections;
using UnityEngine;

namespace Game.ImpulseComponent
{
    public class ImpulseComponent:MonoBehaviour
    {
        [SerializeField] private float _actionTime = 0.15f; // окно действия
        [SerializeField] private float _impulseToOther = 8f;

        private float _actionUntil;

        public void BeginAction() => _actionUntil = Time.time + _actionTime;

        private void OnCollisionEnter2D(Collision2D collision)
            => TryImpulse(collision.collider, collision.rigidbody);

        private void OnCollisionStay2D(Collision2D collision)
            => TryImpulse(collision.collider, collision.rigidbody);

        private void OnTriggerEnter2D(Collider2D other)
            => TryImpulse(other, other.attachedRigidbody);

        private void OnTriggerStay2D(Collider2D other)
            => TryImpulse(other, other.attachedRigidbody);

        private void TryImpulse(Collider2D col, Rigidbody2D otherRb)
        {
            if (Time.time > _actionUntil) return;
            if (otherRb == null || otherRb == GetComponent<Rigidbody2D>()) return;
            
            Vector2 dir = (otherRb.position - (Vector2)transform.position).normalized;
            otherRb.AddForce(dir * _impulseToOther, ForceMode2D.Impulse);
        }
   /* public void Impulse(float fx, float fy, Rigidbody2D rb)
    {
        if (rb == null) return;
        if (fx != 0f) rb.AddForceX(fx, ForceMode2D.Impulse);
        if (fy != 0f) rb.AddForceY(fy, ForceMode2D.Impulse);
    }*/
    }
}
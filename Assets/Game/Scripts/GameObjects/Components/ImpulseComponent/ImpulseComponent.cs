using System;
using System.Collections;
using UnityEngine;

namespace Game.ImpulseComponent
{
    public class ImpulseComponent : MonoBehaviour
    {
        [SerializeField] private float _actionTime = 0.15f; 
        [SerializeField] private float _fx = 1f;
        [SerializeField] private float _fy = 1f;

        private float _actionUntil;

        public void BeginAction() => _actionUntil = Time.time + _actionTime;


        public void TryImpulse(Collider2D col, Rigidbody2D otherRb)
        {
            if (Time.time > _actionUntil) return;
            if (otherRb == null || otherRb == GetComponent<Rigidbody2D>()) return;

            Vector2 dir = (otherRb.position - (Vector2)transform.position).normalized;
            Vector2 force = new Vector2(dir.x * _fx, dir.y * _fy);
            otherRb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.GameObjects.Components.ImpulseComponent
{
    public class ImpulseComponent : MonoBehaviour
    {
        public interface IAction
        {
            void Invoke();
        }

        public interface ICondition
        {
            bool Evaluate();
        }

        private IAction _action;
        
        private ICondition _condition;

        private float cooldown = 0.1f;
        public void SetAction(IAction action) => _action = action;
        public void SetCondition(ICondition condition) => _condition = condition;

        public Action<bool> OnImpulse;
        
        [SerializeField] private float _actionTime = 0.15f;
        
        [SerializeField] private float _fx;
        
        [SerializeField] private float _fy;

        [SerializeField] private float _radius = 2f;
        
        [SerializeField] private LayerMask _mask = ~0;
        
        private Collider2D[] _buffer;
        
        private Rigidbody2D _selfRb;

        private float _actionUntil;

        private void Awake()
        {
            _buffer = new Collider2D[16];
            _selfRb = GetComponent<Rigidbody2D>();
        }

        public void ImpulseWithDelay()
        {
            this.StartCoroutine(TryImpulse());
        }


        public IEnumerator TryImpulse()
        {
            int count = Physics2D.OverlapCircleNonAlloc(
                transform.position, _radius, _buffer, _mask);

            Vector2 origin = transform.position;

            for (int i = 0; i < count; i++)
            {
                var col = _buffer[i];
                if (col == null) continue;

                var otherRb = col.attachedRigidbody;
                if (otherRb == null || otherRb == _selfRb) continue;

                Vector2 dir = (otherRb.position - origin);
                if (dir.sqrMagnitude < 0.0001f) continue;
                dir.Normalize();

                Vector2 force = new Vector2(dir.x * _fx, dir.y) + Vector2.up * _fy;

                otherRb.AddForce(force, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(cooldown);
        }

        public void Impulse()
        {
            if (_condition != null && !_condition.Evaluate()) return;
            _action?.Invoke();
            OnImpulse?.Invoke(true);
        }
    }
}
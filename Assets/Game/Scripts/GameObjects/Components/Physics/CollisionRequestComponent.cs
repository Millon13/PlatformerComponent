using System;
using UnityEngine;

namespace Game
{
    public class CollisionRequestComponent : MonoBehaviour
    {
        public event Action OnCollision;
        
        private Func<bool> _condition;
        private Action _action;
        private bool _required;

        public void SetCondition(Func<bool> condition) => _condition = condition;
        public void SetAction(Action action) => _action = action;
        
        public void CollisionRequest()
        {
            _required = true;
        }

        private void FixedUpdate()
        {
            if (_required && (_condition == null || _condition.Invoke()))
            {
                _action?.Invoke();
                this.OnCollision?.Invoke();
            }

            _required = false;
        }
    
    }
}
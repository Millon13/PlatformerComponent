using System;
using UnityEngine;

namespace Game.ImpulseComponent
{
    public class ImpulseRequestComponent : MonoBehaviour
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

        public void SetAction(IAction action) => _action = action;
        public void SetCondition(ICondition condition) => _condition = condition;

        public Action<bool> OnImpulse;

        public void Impulse()
        {
            if (_condition != null && !_condition.Evaluate()) return;
            _action?.Invoke();
            OnImpulse?.Invoke(true);
        }
    }
}
using System;
using UnityEngine;

namespace Game.ImpulseComponent
{
    public class TossRequestComponent: MonoBehaviour
    {
        public interface IAction
        {
            void Invoke();
        }
        
        public interface ICondition
        {
            bool Evaluate();
        }         
        private PushRequestComponent.IAction _tossAction;
        private PushRequestComponent.ICondition _condition;

        public void SetAction(PushRequestComponent.IAction action) => _tossAction = action;

        public void SetCondition(PushRequestComponent.ICondition condition) => _condition = condition;

        [SerializeField]
        private bool _tossRequired;

        [SerializeField]
        private float _tossDuration = 0.1f;

        [SerializeField]
        private float _tossTime;
        public Action<bool> OnToss;
        public void Toss()
        {
            _tossRequired = true;
        }
        public void Update()
        {
            if (_tossRequired && (_condition == null || _condition.Evaluate()))
            {
               // _tossAction?.Invoke();
                _tossTime = Time.time + _tossDuration;
                this.OnToss?.Invoke(true);
            }
            _tossRequired = false;
        }
    }
}
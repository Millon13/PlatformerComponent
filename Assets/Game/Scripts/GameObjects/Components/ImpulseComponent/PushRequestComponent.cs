using System;
using UnityEngine;

namespace Game.ImpulseComponent
{
    public class PushRequestComponent:MonoBehaviour
    {
        public interface IAction
        {
            void Invoke(float fx, float fy, Rigidbody2D rb);
        }
        
        public interface ICondition
        {
            bool Evaluate();
        }         
        private PushRequestComponent.IAction _pushAction;
        private PushRequestComponent.ICondition _condition;

        public void SetAction(PushRequestComponent.IAction action) => _pushAction = action;

        public void SetCondition(PushRequestComponent.ICondition condition) => _condition = condition;

        [SerializeField]
        private bool _pushRequired;

        [SerializeField]
        private float _pushDuration = 0.1f;

        [SerializeField]
        private float _pushTime;
        [SerializeField]
        private float _fx;
        [SerializeField]
        private float _fy;
        public Action<bool> OnPush;
        public void Push()
        {
            _pushRequired = true;
        }
        public void Update()
        {
            if (_pushRequired && (_condition == null || _condition.Evaluate()))
            {
               // _pushAction?.Invoke(fx: _fx, fy: _fy, );
                _pushTime = Time.time + _pushDuration;
                this.OnPush?.Invoke(true);
            }

            _pushRequired = false;
        }
    }
}
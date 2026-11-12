using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class MoveComponent : MonoBehaviour
    {
        
        public interface IAction
        {
            void Invoke(Vector3 direction);
        }

        public interface ICondition
        {
            bool Evaluate();
        }

        public event Action<Vector3> OnMoved;
        public event Action<bool> OnMoving;

        public bool IsMoving => Time.time <= _moveTime;

        [SerializeField] private Vector3 _moveDirection;

        [SerializeField] private bool _moveRequired;

        [SerializeField] private float _moveDuration = 0.1f;

        [SerializeField] private float _moveTime;

        private IAction _moveAction;
        private ICondition _condition;
        
        [SerializeField] private float _moveSpeed = 4.5f;

        public void MoveStep(Vector3 moveDirection)
        {
            this.transform.position += moveDirection * _moveSpeed * Time.fixedDeltaTime;
        }

        public bool MoveTowards(Vector3 target, float speed, float arriveThreshold = 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                speed * Time.fixedDeltaTime
            );
            return (transform.position - target).sqrMagnitude <= arriveThreshold*arriveThreshold;
        }

        public void SetAction(IAction action) => _moveAction = action;

        public void SetCondition(ICondition condition) => _condition = condition;

        public void Move(Vector3 direction)
        {
            _moveDirection = direction;
            _moveRequired = true;
        }

        private void FixedUpdate()
        {
            if (_moveRequired && _moveDirection != Vector3.zero && (_condition == null || _condition.Evaluate()))
            {
                _moveAction?.Invoke(_moveDirection);
                _moveTime = Time.time + _moveDuration;
                this.OnMoved?.Invoke(_moveDirection);
                this.OnMoving?.Invoke(true);
            }

            if (!_moveRequired)
            {
                this.OnMoving?.Invoke(false);
            }

            _moveRequired = false;
        }
    }
}
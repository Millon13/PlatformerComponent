using System;
using UnityEngine;

namespace Game
{
    public class PatrolComponent:MonoBehaviour
    {
        [SerializeField] private Transform _moveTransform;
        [SerializeField] private Transform _point1;
        [SerializeField] private Transform _point2;
        [SerializeField] private float _speed = 2f; 

        public Action<bool> OnPatrol;
        public bool CanPatrol;
        private Transform _currentTarget;
        
        private void Awake()
        {
            if (_moveTransform == null || _point1 == null || _point2 == null)
                return;

            _moveTransform.position = _point1.position;
            _currentTarget = _point2;
        }
        private void FixedUpdate()
        {
            if (CanPatrol)
            {
                OnPatrol?.Invoke(true);
                Patrol(_currentTarget);
            }
            else
            {
                OnPatrol?.Invoke(false);
            }
        }

        public void Patrol(Transform target)
        {
            OnPatrol?.Invoke(true);
            if (target == null || _moveTransform == null) return;

            _currentTarget = target; 
            Vector3 newPos = Vector3.MoveTowards(
                _moveTransform.position,
                _currentTarget.position,
                _speed * Time.deltaTime
            );
            _moveTransform.position = newPos;

            if (newPos == _currentTarget.position)
            {
                _currentTarget = _currentTarget == _point1 ? _point2 : _point1;
            }
            
        }

    }
}
using System;
using UnityEngine;

namespace Game
{
    public class PatrolComponent : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveTransform;
        [SerializeField] private Transform _point1;
        [SerializeField] private Transform _point2;
        [SerializeField] private float _speed = 2f;

        public event Action<bool> OnPatrolChanged;

        [SerializeField] private bool _canPatrol;

        public bool CanPatrol
        {
            get => _canPatrol;
            set
            {
                if (_canPatrol == value) return;
                _canPatrol = value;
                OnPatrolChanged?.Invoke(_canPatrol);
            }
        }

        private Transform _currentTarget;
        private Vector3 currentTarget;
        private bool _wasPatrolling;

        private void Awake()
        {
            if (_moveTransform == null || _point1 == null || _point2 == null)
            {
                enabled = false;
                return;
            }

            _moveTransform.transform.position = _point1.position;
            _currentTarget = _point2;
        }

        private void FixedUpdate()
        {
            Patrol(_currentTarget);
        }

        public void Patrol(Transform target)
        {
            if (!CanPatrol || target == null) return;

            bool arrived = _moveTransform.MoveTowards(target.position, _speed);
            if (arrived)
                _currentTarget = target == _point1 ? _point2 : _point1;
        }
        /*public void Patrol(Vector3 direction)
        {
            if (!CanPatrol) return;
            
            Vector3 targetPosition = _moveTransform.gameObject.transform.position + direction;

            bool arrived = _moveTransform.MoveTowards(targetPosition, _speed);
            if (arrived)
            {
                currentTarget = targetPosition;
            }
        }*/
    }
}
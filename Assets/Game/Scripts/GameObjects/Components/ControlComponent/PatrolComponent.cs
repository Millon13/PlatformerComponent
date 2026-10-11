using UnityEngine;

namespace Game
{
    public class PatrolComponent:MonoBehaviour
    {
        [SerializeField] private Transform _spiderTransform;
        [SerializeField] private Transform _point1;
        [SerializeField] private Transform _point2;

        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _reachDistance = 0.1f;

        public bool CanPatrol;
        private Transform _currentTarget;
        
        private void Awake()
        {
            if (_spiderTransform == null || _point1 == null || _point2 == null)
                return;

            _spiderTransform.position = _point1.position;
            _currentTarget = _point2;
        }
        private void Update()
        {
            if (CanPatrol)
                Patrol();
        }

        private void Patrol()
        {

            /*if (_currentTarget == null || _spiderTransform == null) return;

            _spiderTransform.position = Vector3.MoveTowards(
                _spiderTransform.position,
                _currentTarget.position,
                _speed * Time.deltaTime
            );

            if (Vector3.Distance(_spiderTransform.position, _currentTarget.position) <= _reachDistance)
            {
                _currentTarget = _currentTarget == _point1 ? _point2 : _point1;
            }
        }*/
            if (_currentTarget == null || _spiderTransform == null) return;

            Vector3 newPos = Vector3.MoveTowards(
                _spiderTransform.position,
                _currentTarget.position,
                _speed * Time.deltaTime
            );

            _spiderTransform.position = newPos;

            // Если позиция совпала с целью — значит дошли
            if (newPos == _currentTarget.position)
            {
                _currentTarget = _currentTarget == _point1 ? _point2 : _point1;
            }
        }

    }
}
using UnityEngine;

namespace Game
{
    public class PatrolView : MonoBehaviour
    {
        [SerializeField] private PatrolComponent _patrolComponent;
        
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            if (_patrolComponent == null)
                _patrolComponent = GetComponent<PatrolComponent>();
        }

        private void OnEnable()
        {
            _patrolComponent.OnPatrol += OnPatrol;
        }

        private void OnDisable()
        {
            _patrolComponent.OnPatrol -= OnPatrol;
        }

        private void OnPatrol(bool IsPatrol)
        {
            _animator.SetBool("IsMoving", IsPatrol);
        }
    }
}
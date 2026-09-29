using UnityEngine;

namespace Game
{
    public class GroundedComponentView: MonoBehaviour
    {
        [SerializeField] private GroundedComponent _groundedComponent;

        [SerializeField] private Animator _animator;
        private void Awake()
        {
            _groundedComponent = GetComponent<GroundedComponent>();
        }

        private void OnEnable()
        {
            _groundedComponent.OnGrounded+=OnGrounded;
        }

        private void OnDisable()
        {
            _groundedComponent.OnGrounded-=OnGrounded;
        }

        private void OnGrounded(bool grounded)
        {
            _animator.SetBool("IsGrounded", grounded);
        }
    }
}
using UnityEngine;

namespace Game
{
    public class JumpComponentView:MonoBehaviour

    {
        [SerializeField] private JumpComponent _jumpComponent;

        [SerializeField] private Animator _animator;
        private void Awake()
        {
            _jumpComponent = GetComponent<JumpComponent>();
        }
        private void OnEnable()
        {
            _jumpComponent.OnJump+=OnJump;
            
        }

        private void OnDisable()
        {
            _jumpComponent.OnJump -= OnJump;

        }

        private void OnJump(bool isJump)
        {
            _animator.SetBool("Jump",isJump);
        }
    
    }
}
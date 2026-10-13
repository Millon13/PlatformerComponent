using UnityEngine;

namespace Game
{
    public class JumpComponentView:MonoBehaviour

    {
        [SerializeField] private JumpComponent _jumpComponent;
        [SerializeField] private Animator _animator;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] private AudioClip _jumpSound;
        private void Awake()
        {
            if (_jumpComponent == null)
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
            if (!isJump) return; 
            _animator.SetTrigger("Jump");
            _audioSource.PlayOneShot(_jumpSound);
        }
    
    }
}
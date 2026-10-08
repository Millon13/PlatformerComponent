using UnityEngine;

namespace Game
{
    public class PushComponentView:MonoBehaviour
    {
        [SerializeField] private PushComponent _pushComponent;
        [SerializeField] private Animator _animator;
        [SerializeField] AudioSource _audioSource;
        [SerializeField] private AudioClip _pushSound;
        private void Awake()
        {
            if (_pushComponent == null)
                _pushComponent = GetComponent<PushComponent>();
        }
        private void OnEnable()
        {
            _pushComponent.OnPush+=OnPush;
            
        }

        private void OnDisable()
        {
            _pushComponent.OnPush -= OnPush;

        }

        private void OnPush(bool isPushed)
        {
            _animator.SetBool("BlowForward",isPushed);
            _audioSource.PlayOneShot(_pushSound);
        }
    }
}
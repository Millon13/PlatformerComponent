using Game.ImpulseComponent;
using UnityEngine;

namespace Game
{
    public class PushComponentView : MonoBehaviour
    {
        [SerializeField] private PushRequestComponent _pushComponent;
        
        [SerializeField] private Animator _animator;
        
        [SerializeField] AudioSource _audioSource;
        
        [SerializeField] private AudioClip _pushSound;
        
        [SerializeField] private ParticleSystem _particleSystem;

        private void Awake()
        {
            if (_pushComponent == null)
                _pushComponent = GetComponent<PushRequestComponent>();
        }

        private void OnEnable()
        {
            _pushComponent.OnPush += OnPush;
        }

        private void OnDisable()
        {
            _pushComponent.OnPush -= OnPush;
        }

        private void OnPush(bool isPushed)
        {
            if (_particleSystem != null)
                _particleSystem.Play();
            _animator.SetBool("BlowForward", isPushed);
            _audioSource.PlayOneShot(_pushSound);
        }
    }
}
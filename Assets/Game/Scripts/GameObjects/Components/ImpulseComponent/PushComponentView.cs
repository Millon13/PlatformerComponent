using Game.ImpulseComponent;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class PushComponentView : MonoBehaviour
    {
        [SerializeField]
        private ImpulseRequestComponent impulseComponent;

        [SerializeField] private Animator _animator;

        [SerializeField] AudioSource _audioSource;

        [SerializeField] private AudioClip _pushSound;

        [SerializeField] private ParticleSystem _particleSystem;

        private void Awake()
        {
            if (impulseComponent == null)
                impulseComponent = GetComponent<ImpulseRequestComponent>();
        }

        private void OnEnable()
        {
            impulseComponent.OnImpulse += OnPush;
        }

        private void OnDisable()
        {
            impulseComponent.OnImpulse -= OnPush;
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
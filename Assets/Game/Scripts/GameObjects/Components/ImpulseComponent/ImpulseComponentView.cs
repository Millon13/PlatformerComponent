using UnityEngine;

namespace Game.Scripts.GameObjects.Components.ImpulseComponent
{
    public class ImpulseComponentView : MonoBehaviour
    {
        [SerializeField] private string _animatorBoolName;
        
        [SerializeField] private Scripts.GameObjects.Components.ImpulseComponent.ImpulseComponent _impulseComponent;

        [SerializeField] private Animator _animator;

        [SerializeField] AudioSource _audioSource;

        [SerializeField] private AudioClip _tossSound;

        [SerializeField] private ParticleSystem _particleSystem;

        private void Awake()
        {
            if (_impulseComponent == null)
                _impulseComponent = GetComponent<Scripts.GameObjects.Components.ImpulseComponent.ImpulseComponent>();
        }

        private void OnEnable()
        {
            _impulseComponent.OnImpulse += OnToss;
        }

        private void OnDisable()
        {
            _impulseComponent.OnImpulse -= OnToss;
        }

        private void OnToss(bool isImpulsed)
        {
            if (_particleSystem != null)
                _particleSystem.Play();
            _animator.SetBool(_animatorBoolName, isImpulsed);
            _audioSource.PlayOneShot(_tossSound);
        }
    }
}